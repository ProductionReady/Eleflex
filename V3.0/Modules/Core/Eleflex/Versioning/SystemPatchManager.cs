using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Eleflex
{
    /// <summary>
    /// Represents an object used to manage patches for the Eleflex system. Will upgrade existing installations based on the currently loaded code base.
    /// </summary>
    public partial class SystemPatchManager : ISystemPatchManager
    {

        /// <summary>
        /// Determine if application is up to date.
        /// </summary>
        protected bool? _isApplicationUpToDate = false;

        /// <summary>
        /// Get a system summary.
        /// </summary>
        /// <returns></returns>
        public virtual PatchSystemSummary GetSystemSummary()
        {
            PatchSystemSummary summary = new PatchSystemSummary();
            List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().Distinct().ToList();

            foreach (Assembly assembly in assemblies)
            {
                try
                {
                    LoadSummaryAssembly(assembly, summary);
                }//This may sometimes encounter ReflectionLoader errors for system references but these can be safely ignored
                catch { }
            }
            summary.InstalledModules = GetInstalledModules();
            return summary;
        }

        /// <summary>
        /// Update the system.
        /// </summary>
        /// <returns></returns>
        public virtual bool Update()
        {
            try
            {
                //Debug logging
                Logger.Current.Debug<SystemPatchManager>("[PATCHMANAGER UPDATE BEGIN]");

                //Get current system summary
                PatchSystemSummary summary = GetSystemSummary();

                //Debug log current module versions                
                string loggingModulesInstalled = "[PATCHMANAGER INSTALLED MODULES SUMMARY] ";
                if (summary.InstalledModules != null)
                {
                    for (int i = 0; i < summary.InstalledModules.Count; i++)
                    {
                        if (i > 0)
                            loggingModulesInstalled += " [,] ";
                        var summartItem = summary.InstalledModules[i];
                        loggingModulesInstalled += summartItem.ModuleKey.ToString() + " [:] " + summartItem.Name + " [:] " + summartItem.Version.ToString();
                    }
                    Logger.Current.Debug<SystemPatchManager>(loggingModulesInstalled);
                }                

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

                //Versioning module is required for all modules, it always goes first
                for (int i = 0; i < modulesToUpdate.Count; i++)
                {
                    if(modulesToUpdate[i].ModuleKey == VersioningConstants.STORAGE_SERVICE_MODULE_KEY)
                    {
                        orderedModuleUpdateList.Add(modulesToUpdate[i]);
                        modulesToUpdate.RemoveAt(i);
                        break;
                    }
                }

                //Iterate through rest of modules
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
                    success = UpdateModule(modToUpdate.ModuleKey, summary);
                    if (!success) //Stop processing patches if an error as there may be dependencies down the line
                        break;
                }                

                //Log result
                if (success)
                {                    
                    Logger.Current.Debug<SystemPatchManager>("[PATCHMANAGER UPDATE END SUCCESS]");
                }
                else
                    Logger.Current.Info<SystemPatchManager>("[PATCHMANAGER UPDATE END ERROR]");

                return success;
            }
            catch (Exception ex)
            {

                Logger.Current.Error<SystemPatchManager>("[PATCHMANAGER UPDATE END ERROR]", ex);                
                return false;
            }
        }

        /// <summary>
        /// Update an individual module.
        /// </summary>
        /// <param name="updateModuleKey"></param>
        /// <param name="summary"></param>
        /// <returns></returns>
        protected virtual bool UpdateModule(Guid updateModuleKey, PatchSystemSummary summary)
        {
            //Find a list of patches for this module
            List<IModulePatch> patches = null;
            if (summary.ModulePatches.ContainsKey(updateModuleKey))
                patches = summary.ModulePatches[updateModuleKey];

            //No patches found to update
            if (patches == null || patches.Count == 0)
            {
                Logger.Current.Warn<SystemPatchManager>("No patches for module: " + updateModuleKey.ToString());                
                return true;
            }

            //Get ordered patch tree
            Module installedVersion = null;
            if(summary.InstalledModules != null)
                installedVersion = summary.InstalledModules.Where(x=>x.ModuleKey == updateModuleKey).FirstOrDefault();
            List<IModulePatch> orderedPatches = GetPatchTree(installedVersion, patches);
            if (orderedPatches == null)
            {
                Logger.Current.Warn<SystemPatchManager>("No patch tree for module: " + updateModuleKey.ToString());
                return true;
            }

            //Execute patches in order
            foreach (IModulePatch patch in orderedPatches)
            {
                string message = "PatchManager updating Module Key: " + patch.ModuleKey + " Name: " + patch.Name + " Version: " + patch.Version.ToString() + " Patch Info: " + patch.PatchInfo;
                bool success = Patch(patch);
                if (success)
                    Logger.Current.Info<SystemPatchManager>(message);
                else
                    Logger.Current.Error<SystemPatchManager>(message);
                if (!success)
                    return false;
            }
            return true;
        }

        
        /// <summary>
        /// Patch the system.
        /// </summary>
        /// <param name="patch"></param>
        /// <returns></returns>
        protected virtual bool Patch(IModulePatch patch)
        {
            IStorageContextUnitOfWork uow = ObjectLocator.Current.GetInstance<IStorageContextUnitOfWork>();
            try
            {
                //Update the patch, commit the changes.
                patch.Update();
                uow.Commit(); //Commit needs to be done above because initial Versioning Module table creation is required for repository to work.

                //Get new version information
                Module version = new Module();
                version.ModuleKey = patch.ModuleKey;
                version.Name = patch.Name;
                version.Description = patch.Description;
                version.Version = patch.Version;

                //Update version information.
                IModuleStorageRepository versionRepository = ObjectLocator.Current.GetInstance<IModuleStorageRepository>();
                IResponseItem<Module> curVersion = versionRepository.Get(new RequestItem<Guid>() { Item = version.ModuleKey });
                if (curVersion.Item == null)
                    versionRepository.Insert(new RequestItem<Module>() { Item = version });
                else
                    versionRepository.Update(new RequestItem<Module>() { Item = version });
                
                //Commit module update
                uow.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Current.Error<SystemPatchManager>(ex);
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
        protected virtual List<IModulePatch> GetPatchTree(Module installedVersion, List<IModulePatch> patches)
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
                    Logger.Current.Warn<SystemPatchManager>("No root version with patches for " + patches[0].ToString());
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
        protected virtual IList<Module> GetInstalledModules()
        {
            try
            {
                IModuleStorageRepository repo = ObjectLocator.Current.GetInstance<IModuleStorageRepository>();
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

            //It must be done this way because of system restarts, mismatched types due to multiple app domains being loaded
            List<Type> types = assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract && x.GetInterfaces().Where(z => z.FullName == patchType.FullName).Any()).ToList();
            if (types == null || types.Count == 0)
                return null;
            
            foreach (Type type in types)
            {
                IModulePatch patch = Activator.CreateInstance(type) as IModulePatch;
                if(patch != null && patch.IsAvailable)
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
        protected virtual Version GetAssemblyVersion(Assembly assembly)
        {
            System.Version appVersion = assembly.GetName().Version;
            Version version = new Version();
            version.ChangeMajor(appVersion.Major);
            version.ChangeMinor(appVersion.Minor);
            version.ChangeBuild(appVersion.Build);
            version.ChangeRevision(appVersion.Revision);
            return version;
        }


    }
}
