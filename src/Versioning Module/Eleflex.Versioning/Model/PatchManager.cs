#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2014 Production Ready, LLC. All Rights Reserved.
//Copyright © 2014 Production Ready, LLC. All Rights Reserved.
//For more information, visit http://www.ProductionReady.com
//This file is part of PRODUCTION READY® ELEFLEX®.
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU Affero General Public License as
//published by the Free Software Foundation, either version 3 of the
//License, or (at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Affero General Public License for more details.
//
//You should have received a copy of the GNU Affero General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;

namespace Eleflex.Versioning
{
    /// <summary>
    /// Class used to manage patches for the eleflex system. Will updgrade existing installtions based on loaded code base.
    /// </summary>
    public class PatchManager
    {
        protected static List<Assembly> _allAssemblies = null;
        protected static object _lockAllAssemblies = new object();
        protected bool? _isApplicationUpToDate = false;
        protected const string SOURCE = "Eleflex.Versioning.Model.PatchManager";

        /// <summary>
        /// Get a system summary.
        /// </summary>
        /// <returns></returns>
        public virtual PatchSystemSummary GetSystemSummary()
        {
            PatchSystemSummary summary = new PatchSystemSummary();
            List<Assembly> assemblies = GetAllAssemblies();
            foreach (Assembly assembly in assemblies)
                LoadSummaryAssembly(assembly, summary);
            summary.InstalledModules = GetInstalledModules();
            return summary;
        }

        /// <summary>
        /// Update the system.
        /// </summary>
        /// <returns></returns>
        public virtual bool Update()
        {
            //Since configured logging provider may not work until updated, we will use an internal log
            //and try to write entries at the end
            List<PatchLog> log = new List<PatchLog>();
            log.Add(new PatchLog(Common.Logging.LogLevel.Info, SOURCE, "Patch update begin", null));
            try
            {
                //Get current system summary
                PatchSystemSummary summary = GetSystemSummary();

                //Find modules to update
                List<IModulePatch> modulesToUpdate = new List<IModulePatch>();
                foreach (var embeddedModule in summary.EmbeddedModules)
                {
                    if (summary.InstalledModules == null)
                        modulesToUpdate.Add(embeddedModule);
                    else
                    {
                        var installedModule = summary.InstalledModules.Where(x => x.ModuleKey == embeddedModule.ModuleKey).FirstOrDefault();
                        if (installedModule == null)
                            modulesToUpdate.Add(embeddedModule);
                        else
                        {
                            if (IsCompareVersionHigher(installedModule.Version, embeddedModule.Version))
                                modulesToUpdate.Add(embeddedModule);
                        }
                    }
                }

                //Order modules for update
                List<IModulePatch> orderedModuleUpdateList = new List<IModulePatch>();
                //Get modules with no dependencies            
                for (int i = 0; i < modulesToUpdate.Count; i++)
                {
                    if (modulesToUpdate[i].DependentModules == null || modulesToUpdate[i].DependentModules.Count == 0)
                    {
                        orderedModuleUpdateList.Add(modulesToUpdate[i]);
                        modulesToUpdate.RemoveAt(i);
                        i--;
                        continue;
                    }
                }
                //Remaining modules check by dependency
                while (modulesToUpdate.Count > 0)
                    orderedModuleUpdateList.Add(GetNextModuleToPatch(modulesToUpdate));

                //Update patches
                bool success = true;
                foreach (var patch in orderedModuleUpdateList)
                {

                    success = UpdateModule(patch.ModuleKey, summary, log);
                    if (!success) //Stop processing patches if an error (this should hopefully never happen)
                        break;
                }                

                //Log result
                if (success)
                    log.Add(new PatchLog(Common.Logging.LogLevel.Info, SOURCE, "Patch overall status: success", null));
                else
                    log.Add(new PatchLog(Common.Logging.LogLevel.Info, SOURCE, "Patch overall status: error", null));

                return success;
            }
            catch (Exception ex)
            {
                log.Add(new PatchLog(Common.Logging.LogLevel.Error, SOURCE, "Patch update error", ex));
                return false;
            }
            finally
            {
                log.Add(new PatchLog(Common.Logging.LogLevel.Info, SOURCE, "Patch update end", null));
                //Try adding log entries to provider now
                for (int i = 0; i < log.Count; i++)
                {
                    PatchLog item = log[i];
                    if (item == null)
                        continue;
                    switch (item.Level)
                    {
                        default:
                        case Common.Logging.LogLevel.Info:
                            Common.Logging.LogManager.GetLogger(item.Source).Info(item.Message, item.Exception);
                            break;
                        case Common.Logging.LogLevel.Debug:
                            Common.Logging.LogManager.GetLogger(item.Source).Debug(item.Message, item.Exception);
                            break;
                        case Common.Logging.LogLevel.Error:
                            Common.Logging.LogManager.GetLogger(item.Source).Error(item.Message, item.Exception);
                            break;
                        case Common.Logging.LogLevel.Fatal:
                            Common.Logging.LogManager.GetLogger(item.Source).Fatal(item.Message, item.Exception);
                            break;
                        case Common.Logging.LogLevel.Trace:
                            Common.Logging.LogManager.GetLogger(item.Source).Trace(item.Message, item.Exception);
                            break;
                        case Common.Logging.LogLevel.Warn:
                            Common.Logging.LogManager.GetLogger(item.Source).Warn(item.Message, item.Exception);
                            break;
                    }
                }
                log.Clear();
            }
        }

        /// <summary>
        /// Update an individual module.
        /// </summary>
        /// <param name="updateModuleKey"></param>
        /// <param name="summary"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        protected bool UpdateModule(Guid updateModuleKey, PatchSystemSummary summary, List<PatchLog> log)
        {
            List<IModulePatch> patches = null;
            foreach (var modulePatch in summary.ModulePatches)
            {
                //Found module to update
                if (modulePatch.Key == updateModuleKey)
                {
                    patches = modulePatch.Value;
                    break;
                }
            }

            //No patches found to update
            if (patches == null || patches.Count == 0)
            {
                log.Add(new PatchLog(Common.Logging.LogLevel.Warn, SOURCE, "No patches for module: " + updateModuleKey.ToString(), null));
                return true;
            }

            //Get ordered patch tree
            ModuleVersion installedVersion = null;
            if(summary.InstalledModules != null)
                installedVersion = summary.InstalledModules.Where(x=>x.ModuleKey == updateModuleKey).FirstOrDefault();
            List<IModulePatch> orderedPatches = GetPatchTree(installedVersion, patches);
            if (orderedPatches == null)
            {
                log.Add(new PatchLog(Common.Logging.LogLevel.Warn, SOURCE, "No patch tree for module: " + updateModuleKey.ToString(), null)); 
                return true;
            }

            //Execute patches in order
            foreach (IModulePatch patch in orderedPatches)
            {
                string message = "Patch updating Module: " + patch.ModuleKey + " Module Name: " + patch.ModuleName + " Version: " + patch.Version.ToString() + " Result: ";
                bool success = patch.Patch();                                        
                if (success)
                    log.Add(new PatchLog(Common.Logging.LogLevel.Info, SOURCE, message + "success", null));
                else
                    log.Add(new PatchLog(Common.Logging.LogLevel.Info, SOURCE, message + "error", null));
                if (!success)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Get a list of patches to upgrade the system.
        /// </summary>
        /// <param name="installedVersion"></param>
        /// <param name="patches"></param>
        /// <returns></returns>
        protected virtual List<IModulePatch> GetPatchTree(ModuleVersion installedVersion, List<IModulePatch> patches)
        {
            //Create patch tree            
            List<IModulePatch> orderList = new List<IModulePatch>();
            IModulePatch root = null;
            if (installedVersion != null)
            {
                root = patches.Where(x =>
                    x.PriorVersions != null && x.PriorVersions.Any(a =>
                        a.Version.Major.Equals(installedVersion.Version.Major) &&
                        a.Version.Minor.Equals(installedVersion.Version.Minor) &&
                        a.Version.Build.Equals(installedVersion.Version.Build) &&
                        a.Version.Revision.Equals(installedVersion.Version.Revision))).FirstOrDefault();
            }
            else
                root = patches.Where(x => x.PriorVersions == null).FirstOrDefault();

            if (root == null)
                return null;

            orderList.Add(root);
            IModulePatch previousVersion = root;
            while (true)
            {
                List<IModulePatch> nextVersions = patches.Where(x =>
                    x.PriorVersions != null && x.PriorVersions.Any(a =>
                        a.Version.Major.Equals(previousVersion.Version.Major) &&
                        a.Version.Minor.Equals(previousVersion.Version.Minor) &&
                        a.Version.Build.Equals(previousVersion.Version.Build) &&
                        a.Version.Revision.Equals(previousVersion.Version.Revision))).ToList();

                //Get the patch with the highest version number. This will additionally help if a release has errors
                //so a new one can be released with a higher version number and it will be selected over the previous release with an error.
                IModulePatch bestChoice = null;
                foreach (IModulePatch possibleVersion in nextVersions)
                {
                    if (bestChoice == null)
                    {
                        bestChoice = possibleVersion;
                        continue;
                    }
                    if (IsCompareVersionHigher(bestChoice.Version, possibleVersion.Version))
                        bestChoice = possibleVersion;
                }
                if (bestChoice != null)
                {
                    previousVersion = bestChoice;
                    orderList.Add(previousVersion);
                }
                else
                    break;
            }
            return orderList;
        }

        /// <summary>
        /// Get the next module to patch.
        /// </summary>
        /// <param name="modulesToUpdate"></param>
        /// <returns></returns>
        protected virtual IModulePatch GetNextModuleToPatch(List<IModulePatch> modulesToUpdate)
        {
            //Any dependency not found in the list can be assumed to either be installed or already on the list to be installed
            IModulePatch patch = null;
            for (int i = 0; i < modulesToUpdate.Count; i++)
            {
                bool updateModule = true;
                foreach (Guid dependency in modulesToUpdate[i].DependentModules)
                {
                    if (modulesToUpdate.Where(x => x.ModuleKey == dependency).Count() > 0)
                    {
                        updateModule = false;
                        break;
                    }
                }
                if (!updateModule)
                    continue;
                patch = modulesToUpdate[i];
                modulesToUpdate.RemoveAt(i);
                return patch;
            }
            //Circular references in remaining modules, just return first
            patch = modulesToUpdate[0];
            modulesToUpdate.RemoveAt(0);
            return patch;
        }

        /// <summary>
        /// Get a list of installed modules.
        /// </summary>
        /// <returns></returns>
        protected virtual List<ModuleVersion> GetInstalledModules()
        {
            try
            {
                IModuleVersionRepository repo = ServiceLocator.Current.GetInstance<IModuleVersionRepository>();
                if (repo.IsInstalled())
                    return repo.GetAll();
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Load an individual assembly and check for patches.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="summary"></param>
        protected virtual void LoadSummaryAssembly(Assembly assembly, PatchSystemSummary summary)
        {
            List<IModulePatch> patches = this.GetPatchesInAssembly(assembly);
            if(patches == null || patches.Count == 0)
                return;

            IModulePatch latestPatch = patches[patches.Count - 1]; //Latest patch version
            IModuleVersion curModule = summary.EmbeddedModules.Where(x => x.ModuleKey == latestPatch.ModuleKey).FirstOrDefault();
            if (curModule != null)
                return;
            summary.EmbeddedModules.Add(latestPatch);
            summary.ModulePatches.Add(new KeyValuePair<Guid, List<IModulePatch>>(latestPatch.ModuleKey, patches));
        }
        
        /// <summary>
        /// Get a list of all assemblies.
        /// </summary>
        /// <returns></returns>
        public virtual List<Assembly> GetAllAssemblies()
        {
            if (_allAssemblies == null)
            {
                lock (_lockAllAssemblies)
                {
                    if (_allAssemblies != null)
                        return _allAssemblies;

                    LoadAllAssemblies();
                    _allAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
                    return _allAssemblies;
                }
            }
            return _allAssemblies;
        }

        /// <summary>
        /// Load all assemblies.
        /// </summary>
        protected virtual void LoadAllAssemblies()
        {
            List<Assembly> list = new List<Assembly>();
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            foreach (string dll in Directory.GetFiles(basePath, "*.dll", SearchOption.AllDirectories))
            {
                try
                {
                    Assembly loadedAssembly = Assembly.LoadFile(dll);
                }
                catch { }
            }
        }

        /// <summary>
        /// Get an assembly for a type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected virtual Assembly GetAssemblyForType(Type type)
        {
            return Assembly.GetAssembly(type);
        }

        /// <summary>
        /// Get a list of patches in an assembly.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        protected virtual List<IModulePatch> GetPatchesInAssembly(Assembly assembly)
        {
            List<IModulePatch> list = new List<IModulePatch>();
            Type patchType = typeof(IModulePatch);
            List<Type> types = assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract && patchType.IsAssignableFrom(x)).ToList();
            if (types == null || types.Count == 0)
                return null;
            
            foreach (Type type in types)
            {
                IModulePatch patch = Activator.CreateInstance(type) as IModulePatch;
                if(patch != null)
                    list.Add(patch);
            }

            if (list.Count == 0)
                return null;
            return list;
        }

        /// <summary>
        /// Compare versions.
        /// </summary>
        /// <param name="initial"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        protected virtual bool IsCompareVersionHigher(Version initial, Version compare)
        {
            if (compare.Major > initial.Major)
                return true;
            if (compare.Major < initial.Major)
                return false;
            if (compare.Minor > initial.Minor)
                return true;
            if (compare.Minor < initial.Minor)
                return false;
            if (compare.Build > initial.Build)
                return true;
            if (compare.Build < initial.Build)
                return false;
            if (compare.Revision > initial.Revision)
                return true;
            if (compare.Revision < initial.Revision)
                return false;
            return false;
        }

        /// <summary>
        /// Get the current assembly version.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        protected virtual Eleflex.Versioning.Version GetAssemblyVersion(Assembly assembly)
        {
            System.Version appVersion = assembly.GetName().Version;
            Eleflex.Versioning.Version version = new Version();
            version.ChangeMajor(appVersion.Major);
            version.ChangeMinor(appVersion.Minor);
            version.ChangeBuild(appVersion.Build);
            version.ChangeRevision(appVersion.Revision);
            return version;
        }


    }
}
