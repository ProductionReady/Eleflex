#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2015 Production Ready, LLC. All Rights Reserved.
//Copyright © 2015 Production Ready, LLC. All Rights Reserved.
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
using Eleflex.Storage;

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
        protected const string SOURCE = "Eleflex.Versioning.PatchManager";

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
            try
            {
                //Get current system summary
                PatchSystemSummary summary = GetSystemSummary();

                //Find modules to update
                List<IModulePatch> modulesToUpdate = new List<IModulePatch>();
                foreach (Guid moduleKey in summary.ModulePatches.Keys)
                {
                    List<IModulePatch> patches = summary.ModulePatches[moduleKey];
                    IModulePatch moduleLatest = GetLatestVersionPatch(patches);
                    if (moduleLatest == null)
                        continue;

                    if (summary.InstalledModules == null)
                        modulesToUpdate.Add(moduleLatest);
                    else
                    {
                        var installedModule = summary.InstalledModules.Where(x => x.ModuleKey == moduleLatest.ModuleKey).FirstOrDefault();
                        if (installedModule == null)
                            modulesToUpdate.Add(moduleLatest);
                        else
                        {
                            if (IsCompareVersionHigher(installedModule.Version, moduleLatest.Version))
                                modulesToUpdate.Add(moduleLatest);
                        }
                    }
                }

                //Order modules for update. Get modules with no dependencies first
                List<IModulePatch> orderedModuleUpdateList = new List<IModulePatch>();                
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

                //Update modules
                bool success = true;
                foreach (var modToUpdate in orderedModuleUpdateList)
                {
                    success = UpdateModule(modToUpdate.ModuleKey, summary, log);
                    if (!success) //Stop processing patches if an error as there may be dependencies down the line
                        break;
                }                

                //Log result
                if (success)
                {
                    //Don't log success for now, clearer log...
                    //log.Add(new PatchLog(Common.Logging.LogLevel.Info, SOURCE, "PatchManager update status: success", null));
                }
                else
                    log.Add(new PatchLog(Common.Logging.LogLevel.Info, SOURCE, "PatchManager update status: error", null));

                return success;
            }
            catch (Exception ex)
            {
                log.Add(new PatchLog(Common.Logging.LogLevel.Error, SOURCE, "PatchManager update error", ex));
                return false;
            }
            finally
            {                
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
                    System.Threading.Thread.Sleep(5); //Make sure messages show up in correct order due to precision of underlying storage providers
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
            //Find a list of patches for this module
            List<IModulePatch> patches = null;
            if (summary.ModulePatches.ContainsKey(updateModuleKey))
                patches = summary.ModulePatches[updateModuleKey];

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
            List<IModulePatch> orderedPatches = GetPatchTree(installedVersion, patches, log);
            if (orderedPatches == null)
            {
                log.Add(new PatchLog(Common.Logging.LogLevel.Warn, SOURCE, "No patch tree for module: " + updateModuleKey.ToString(), null)); 
                return true;
            }

            //Execute patches in order
            foreach (IModulePatch patch in orderedPatches)
            {
                string message = "PatchManager updating Module: " + patch.ModuleKey + " Module Name: " + patch.ModuleName + " Version: " + patch.Version.ToString() + " Result: ";
                bool success = Patch(patch, log);                                        
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
        /// Patch the system.
        /// </summary>
        /// <returns></returns>
        protected virtual bool Patch(IModulePatch patch, List<PatchLog> log)
        {
            IStorageProviderUnitOfWork uow = ServiceLocator.Current.GetInstance<IStorageProviderUnitOfWork>();
            try
            {
                //Update the patch
                patch.Update();                

                //Get new version information
                ModuleVersion version = new ModuleVersion();
                version.ChangeModuleKey(patch.ModuleKey);
                version.ChangeModuleName(patch.ModuleName);
                version.ChangeUpdateDate(DateTimeOffset.UtcNow);
                version.ChangeVersion(patch.Version);

                //Update version information
                IModuleVersionRepository versionRepository = ServiceLocator.Current.GetInstance<IModuleVersionRepository>();
                ModuleVersion curVersion = versionRepository.Get(version.ModuleKey);
                if (curVersion == null)
                    versionRepository.Insert(version);
                else
                    versionRepository.Update(version);
                
                //Commit all changes
                uow.Commit();
                return true;
            }
            catch (Exception ex)
            {
                log.Add(new PatchLog(Common.Logging.LogLevel.Error, SOURCE, "Failure updating patch " + patch.ToString(), ex));
                uow.Rollback();
                return false;
            }
        }

        /// <summary>
        /// Get a list of patches to upgrade the system.
        /// </summary>
        /// <param name="installedVersion"></param>
        /// <param name="patches"></param>
        /// <returns></returns>
        protected virtual List<IModulePatch> GetPatchTree(ModuleVersion installedVersion, List<IModulePatch> patches, List<PatchLog> log)
        {
            //Create patch tree            
            List<IModulePatch> orderList = new List<IModulePatch>();
            if (patches == null || patches.Count == 0)
                return orderList;

            Version currentVersionToStartFrom = null;
            if (installedVersion == null)
            {
                //New install, find best root version to start from (should really only be one but maybe rollup patches too)
                List<IModulePatch> availRoots = patches.Where(x => x.PriorVersions == null).ToList();
                if(availRoots.Count == 0)
                {
                    log.Add(new PatchLog(Common.Logging.LogLevel.Warn, SOURCE, "No root version with patches for " + patches[0].ToString(), null));
                    return orderList;
                }
                IModulePatch bestChoice = null;
                foreach (IModulePatch possibleVersion in availRoots)
                {
                    if (bestChoice == null)
                    {
                        bestChoice = possibleVersion;
                        continue;
                    }
                    if (IsCompareVersionHigher(bestChoice.Version, possibleVersion.Version))
                        bestChoice = possibleVersion;
                }
                orderList.Add(bestChoice);
                currentVersionToStartFrom = bestChoice.Version;
            }
            else
                currentVersionToStartFrom = installedVersion.Version;

            
            while (true)
            {
                List<IModulePatch> nextVersions = patches.Where(x =>
                    x.PriorVersions != null && x.PriorVersions.Any(a =>
                        a.Major.Equals(currentVersionToStartFrom.Major) &&
                        a.Minor.Equals(currentVersionToStartFrom.Minor) &&
                        a.Build.Equals(currentVersionToStartFrom.Build) &&
                        a.Revision.Equals(currentVersionToStartFrom.Revision))).ToList();

                //No more versions to upgrade to
                if (nextVersions.Count == 0)
                    break;

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
                    orderList.Add(bestChoice);
                    currentVersionToStartFrom = bestChoice.Version;                    
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
                if (repo.IsInstalled()) //Won't throw error
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
            //Get all patches in assembly
            List<IModulePatch> patches = this.GetPatchesInAssembly(assembly);
            if(patches == null || patches.Count == 0)
                return;

            //Patches may be for different modules
            foreach(IModulePatch patch in patches)
            {
                //ModulePatches hold all available patches for a module
                if (summary.ModulePatches.ContainsKey(patch.ModuleKey))
                    summary.ModulePatches[patch.ModuleKey].Add(patch);
                else
                    summary.ModulePatches.Add(patch.ModuleKey, new List<IModulePatch>() { patch });
            }
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
        /// Get the highest version for this module.
        /// </summary>
        /// <param name="patches"></param>
        /// <returns></returns>
        protected virtual IModulePatch GetLatestVersionPatch(List<IModulePatch> patches)
        {
            if (patches == null || patches.Count == 0)
                return null;

            IModulePatch highest = null;
            foreach (IModulePatch patch in patches)
            {
                if(highest == null)
                {
                    highest = patch;
                    continue;
                }
                if (IsCompareVersionHigher(highest.Version, patch.Version))
                    highest = patch;
            }
            return highest;
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
