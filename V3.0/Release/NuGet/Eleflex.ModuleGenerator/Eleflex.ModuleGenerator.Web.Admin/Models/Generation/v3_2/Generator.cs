using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class Generator
    {


        protected List<Project> _projects = null;
        protected string _moduleName = string.Empty;
        protected string _namespacePrefix = string.Empty;
        protected string _efName = string.Empty;
        protected Eleflex.Version _currentVersion = new Eleflex.Version(3, 2, 1, 0);


        protected List<Project> GetProjects()
        {
            List<Project> list = new List<Project>();
            list.Add(new BusinessProject() { ModuleName = _moduleName, NamespacePrefix = _namespacePrefix, NamespaceSuffix = "" });
            list.Add(new MessagesProject() { ModuleName = _moduleName, NamespacePrefix = _namespacePrefix, NamespaceSuffix = "Messages" });

            list.Add(new ServerProject( _namespacePrefix + "." + _moduleName, list[0].GetProjectNamespace(), list[1].GetProjectNamespace(), list[0].ProjectGuid, list[1].ProjectGuid)
            { ModuleName = _moduleName, NamespacePrefix = _namespacePrefix, NamespaceSuffix = "Server" });

            Guid webAdminProjectGuid = Guid.NewGuid();

            list.Add(new WebClientProject(_namespacePrefix + "." + _moduleName + ".Web.Admin", list[1].GetProjectNamespace(), webAdminProjectGuid, list[1].ProjectGuid) { ModuleName = _moduleName, NamespacePrefix = _namespacePrefix, NamespaceSuffix = "WebClient" });
            list.Add(new WebServerProject(list[0].GetProjectNamespace(), list[2].GetProjectNamespace(), list[0].ProjectGuid, list[2].ProjectGuid) { ModuleName = _moduleName, NamespacePrefix = _namespacePrefix, NamespaceSuffix = "WebServer" });
            list.Add(new WebAdminProject(webAdminProjectGuid, list[1].GetProjectNamespace(), list[1].ProjectGuid) { ModuleName = _moduleName, NamespacePrefix = _namespacePrefix, NamespaceSuffix = "Web.Admin" });            

            return list;
        }

        public byte[] GenerateArchive(string namespacePrefix, string moduleName, string efName)
        {
            _namespacePrefix = namespacePrefix;
            _moduleName = moduleName;
            _efName = efName;

            _projects = GetProjects();            

            List<ArchiveItem> archiveItems = new List<ArchiveItem>();
            archiveItems.AddRange(GetSolutionFiles());
            archiveItems.AddRange(GetBusinessProjectFiles());
            archiveItems.AddRange(GetMessageProjectFiles());
            archiveItems.AddRange(GetServerProjectFiles());
            archiveItems.AddRange(GetWebClientProjectFiles());
            archiveItems.AddRange(GetWebServerProjectFiles());
            archiveItems.AddRange(GetWebAdminProjectFiles());

            return Archive.CreateArchive(archiveItems);
        }

        protected List<ArchiveItem> GetSolutionFiles()
        {
            List<ArchiveItem> files = new List<ArchiveItem>();
            Solution solution = new Solution(_projects);

            ArchiveItem item = new ArchiveItem();
            item.Filename = _projects[0].ModuleName + ".sln";
            item.Data = System.Text.Encoding.UTF8.GetBytes(solution.Generate());
            files.Add(item);


            return files;
        }

        protected List<ArchiveItem> GetBusinessProjectFiles()
        {
            List<ArchiveItem> files = new List<ArchiveItem>();
            BusinessProject proj = (BusinessProject)_projects[0];

            ArchiveItem item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\" + proj.GetProjectNamespace() + ".csproj";
            item.Data = System.Text.Encoding.UTF8.GetBytes(proj.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\Properties\AssemblyInfo.cs";
            FileAssemblyInfoCS assinfo = new FileAssemblyInfoCS();
            assinfo.AssemblyTitle = proj.GetProjectNamespace();
            assinfo.AssemblyGuid = proj.ProjectGuid;
            assinfo.AssemblyFileVersion = _currentVersion.ToString();
            item.Data = System.Text.Encoding.UTF8.GetBytes(assinfo.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\" + proj.GetProjectNamespace().Replace(".","_") + "_CodeGen.tt";
            BusinessFileCodeGenTT bfcgtt = new BusinessFileCodeGenTT(_moduleName, proj.GetProjectNamespace(), _efName, _projects[2].GetProjectNamespace());
            item.Data = System.Text.Encoding.UTF8.GetBytes(bfcgtt.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\I" + _moduleName + "StorageService.cs";
            BusinessFileStorageService bfss = new BusinessFileStorageService(_namespacePrefix + "." + _moduleName, _moduleName);
            item.Data = System.Text.Encoding.UTF8.GetBytes(bfss.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\" + _moduleName + "Constants.cs";
            BusinessFileConstants bfc = new BusinessFileConstants(_namespacePrefix + "." + _moduleName, _moduleName);
            item.Data = System.Text.Encoding.UTF8.GetBytes(bfc.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\packages.config";
            BusinessFilePackagesConfig bfcpc = new BusinessFilePackagesConfig();
            item.Data = System.Text.Encoding.UTF8.GetBytes(bfcpc.Generate());
            files.Add(item);


            return files;
        }

        protected List<ArchiveItem> GetMessageProjectFiles()
        {
            List<ArchiveItem> files = new List<ArchiveItem>();            

            MessagesProject proj = (MessagesProject)_projects[1];

            ArchiveItem item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\" + proj.GetProjectNamespace() + ".csproj";
            item.Data = System.Text.Encoding.UTF8.GetBytes(proj.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\Properties\AssemblyInfo.cs";
            FileAssemblyInfoCS assinfo = new FileAssemblyInfoCS();
            assinfo.AssemblyTitle = proj.GetProjectNamespace();
            assinfo.AssemblyGuid = proj.ProjectGuid;
            assinfo.AssemblyFileVersion = _currentVersion.ToString();
            item.Data = System.Text.Encoding.UTF8.GetBytes(assinfo.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\" + _namespacePrefix.Replace(".", "_") + "_" + _moduleName + "_Services_WCF_Message_CodeGen.tt";
            MessagesFileServicesCodeGenTT mfscg = new MessagesFileServicesCodeGenTT(_namespacePrefix + "." + _moduleName, _moduleName, proj.GetProjectNamespace(), _efName, _projects[2].GetProjectNamespace());
            item.Data = System.Text.Encoding.UTF8.GetBytes(mfscg.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\" + proj.GetProjectNamespace().Replace(".", "") + "Constants.cs";
            MessagesFileConstants wcfc = new MessagesFileConstants(proj.GetProjectNamespace());
            item.Data = System.Text.Encoding.UTF8.GetBytes(wcfc.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\packages.config";
            MessagesFilePackagesConfig mfpc = new MessagesFilePackagesConfig();
            item.Data = System.Text.Encoding.UTF8.GetBytes(mfpc.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\ModulePatch\Version_" + _currentVersion.ToString().Replace(".","_") + ".cs";
            FileModulePatch fmp = new FileModulePatch(proj.GetProjectNamespace(), _currentVersion);
            item.Data = System.Text.Encoding.UTF8.GetBytes(fmp.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\I" + proj.ModuleName + "RequestDispatcher.cs";
            MessagesFileIRequestDispatcher ird = new MessagesFileIRequestDispatcher(proj.NamespacePrefix + "." + proj.ModuleName, proj.ModuleName);
            item.Data = System.Text.Encoding.UTF8.GetBytes(ird.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\" + proj.ModuleName + "RequestDispatcher.cs";
            MessagesFileRequestDispatcher rd = new MessagesFileRequestDispatcher(proj.NamespacePrefix + "." + proj.ModuleName, proj.ModuleName);
            item.Data = System.Text.Encoding.UTF8.GetBytes(rd.Generate());
            files.Add(item);

            return files;
        }

        protected List<ArchiveItem> GetServerProjectFiles()
        {
            List<ArchiveItem> files = new List<ArchiveItem>();

            ServerProject proj = (ServerProject)_projects[2];

            ArchiveItem item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\" + proj.GetProjectNamespace() + ".csproj";
            item.Data = System.Text.Encoding.UTF8.GetBytes(proj.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\Properties\AssemblyInfo.cs";
            FileAssemblyInfoCS assinfo = new FileAssemblyInfoCS();
            assinfo.AssemblyTitle = proj.GetProjectNamespace();
            assinfo.AssemblyGuid = proj.ProjectGuid;
            assinfo.AssemblyFileVersion = _currentVersion.ToString();
            item.Data = System.Text.Encoding.UTF8.GetBytes(assinfo.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\" + _namespacePrefix.Replace(".", "_") + "_" + _moduleName + "_Services_WCF_Server_CodeGen.tt";
            ServerFileServicesServerCodeGenTT mfscg = new ServerFileServicesServerCodeGenTT(_namespacePrefix, _namespacePrefix + "." + _moduleName, _moduleName, proj.GetProjectNamespace(), _efName, _projects[2].GetProjectNamespace());
            item.Data = System.Text.Encoding.UTF8.GetBytes(mfscg.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\" + _namespacePrefix.Replace(".", "_") + "_" + _moduleName + "_Services_WCF_AutoMapper_CodeGen.tt";
            ServerFileServicesAutomapperCodeGenTT mfsacg = new ServerFileServicesAutomapperCodeGenTT(_namespacePrefix, _namespacePrefix + "." + _moduleName, _moduleName, proj.GetProjectNamespace(), _efName, _projects[2].GetProjectNamespace());
            item.Data = System.Text.Encoding.UTF8.GetBytes(mfsacg.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\" + _namespacePrefix.Replace(".", "_") + "_" + _moduleName + "_Storage_EF_CodeGen.tt";
            ServerFileEFCodeGenTT sfef = new ServerFileEFCodeGenTT(_namespacePrefix, _namespacePrefix + "." + _moduleName, _moduleName, proj.GetProjectNamespace(), _efName, _projects[2].GetProjectNamespace());
            item.Data = System.Text.Encoding.UTF8.GetBytes(sfef.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\" + _namespacePrefix.Replace(".", "_") + "_" + _moduleName + "_Storage_EF_AutoMapper_CodeGen.tt";
            ServerFileEFAutoMapperCodeGenTT sfefa = new ServerFileEFAutoMapperCodeGenTT(_namespacePrefix, _namespacePrefix + "." + _moduleName, _moduleName, proj.GetProjectNamespace(), _efName, _projects[2].GetProjectNamespace());
            item.Data = System.Text.Encoding.UTF8.GetBytes(sfefa.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\" + proj.GetProjectNamespace().Replace(".", "") + "Constants.cs";
            ServerFileConstants wcfc = new ServerFileConstants(proj.GetProjectNamespace());
            item.Data = System.Text.Encoding.UTF8.GetBytes(wcfc.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\packages.config";
            ServerFilePackagesConfig mfpc = new ServerFilePackagesConfig();
            item.Data = System.Text.Encoding.UTF8.GetBytes(mfpc.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\App.config";
            ServerFileAppConfig sac = new ServerFileAppConfig();
            item.Data = System.Text.Encoding.UTF8.GetBytes(sac.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\ModulePatch\Version_" + _currentVersion.ToString().Replace(".", "_") + ".cs";
            FileModulePatch fmp = new FileModulePatch(proj.GetProjectNamespace(), _currentVersion);
            item.Data = System.Text.Encoding.UTF8.GetBytes(fmp.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\" + _namespacePrefix + "." + _moduleName + @".Storage.EF.Azure\Version_" + _currentVersion.ToString().Replace(".", "_") + ".cs";
            ServerFileAzurePatch sfap = new ServerFileAzurePatch(_namespacePrefix + "." + _moduleName + @".Storage.EF.Azure", _currentVersion, _moduleName);
            item.Data = System.Text.Encoding.UTF8.GetBytes(sfap.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\" + _namespacePrefix + "." + _moduleName + @".Storage.EF.Azure\" + _moduleName + @"AzureConstants.cs";
            ServerFileAzureConstants sfac = new ServerFileAzureConstants(_namespacePrefix + "." + _moduleName, _moduleName);
            item.Data = System.Text.Encoding.UTF8.GetBytes(sfac.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\" + _namespacePrefix + "." + _moduleName + @".Storage.EF.SQLServer\Version_" + _currentVersion.ToString().Replace(".", "_") + ".cs";
            ServerFileSQLServerPatch fsmp = new ServerFileSQLServerPatch(_namespacePrefix + "." + _moduleName + @".Storage.EF.SQLServer", _currentVersion, _moduleName);
            item.Data = System.Text.Encoding.UTF8.GetBytes(fsmp.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\" + _namespacePrefix + "." + _moduleName + @".Storage.EF.SQLServer\" + _moduleName + @"SQLServerConstants.cs";
            ServerFileSQLServerConstants sfsc = new ServerFileSQLServerConstants(_namespacePrefix + "." + _moduleName, _moduleName);
            item.Data = System.Text.Encoding.UTF8.GetBytes(sfsc.Generate());
            files.Add(item);

            return files;
        }


        protected List<ArchiveItem> GetWebClientProjectFiles()
        {
            List<ArchiveItem> files = new List<ArchiveItem>();
            WebClientProject proj = (WebClientProject)_projects[3];

            ArchiveItem item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\" + proj.GetProjectNamespace() + ".csproj";
            item.Data = System.Text.Encoding.UTF8.GetBytes(proj.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\Properties\AssemblyInfo.cs";
            FileAssemblyInfoCS assinfo = new FileAssemblyInfoCS();
            assinfo.AssemblyTitle = proj.GetProjectNamespace();
            assinfo.AssemblyGuid = proj.ProjectGuid;
            assinfo.AssemblyFileVersion = _currentVersion.ToString();
            item.Data = System.Text.Encoding.UTF8.GetBytes(assinfo.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\" + proj.GetProjectNamespace().Replace(".","") + "Constants.cs";
            WebClientFileConstants wcfc = new WebClientFileConstants(proj.GetProjectNamespace());
            item.Data = System.Text.Encoding.UTF8.GetBytes(wcfc.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\packages.config";
            WebClientFilePackagesConfig bfcpc = new WebClientFilePackagesConfig();
            item.Data = System.Text.Encoding.UTF8.GetBytes(bfcpc.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\ModulePatch\Version_" + _currentVersion.ToString().Replace(".", "_") + ".cs";
            FileModulePatch fmp = new FileModulePatch(proj.GetProjectNamespace(), _currentVersion);
            item.Data = System.Text.Encoding.UTF8.GetBytes(fmp.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\WebClientObjectLocationRegistrationTask.cs";
            WebClientFileObjLocRegTask wcolrs = new WebClientFileObjLocRegTask(proj.NamespacePrefix + "." + proj.ModuleName, proj.ModuleName, proj.GetProjectNamespace());
            item.Data = System.Text.Encoding.UTF8.GetBytes(wcolrs.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\WebClientRoutesStartupTask.cs";
            WebClientFileRouteRegTask wcrrt = new WebClientFileRouteRegTask(proj.NamespacePrefix + "." + proj.ModuleName, proj.ModuleName, proj.GetProjectNamespace(), _projects[5].GetProjectNamespace());
            item.Data = System.Text.Encoding.UTF8.GetBytes(wcrrt.Generate());
            files.Add(item);
            
            return files;
        }


        protected List<ArchiveItem> GetWebServerProjectFiles()
        {
            List<ArchiveItem> files = new List<ArchiveItem>();
            WebServerProject proj = (WebServerProject)_projects[4];

            ArchiveItem item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\" + proj.GetProjectNamespace() + ".csproj";
            item.Data = System.Text.Encoding.UTF8.GetBytes(proj.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\Properties\AssemblyInfo.cs";
            FileAssemblyInfoCS assinfo = new FileAssemblyInfoCS();
            assinfo.AssemblyTitle = proj.GetProjectNamespace();
            assinfo.AssemblyGuid = proj.ProjectGuid;
            assinfo.AssemblyFileVersion = _currentVersion.ToString();
            item.Data = System.Text.Encoding.UTF8.GetBytes(assinfo.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\" + proj.GetProjectNamespace().Replace(".", "") + "Constants.cs";
            WebServerFileConstants wcfc = new WebServerFileConstants(proj.GetProjectNamespace());
            item.Data = System.Text.Encoding.UTF8.GetBytes(wcfc.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\packages.config";
            WebServerFilePackagesConfig bfcpc = new WebServerFilePackagesConfig();
            item.Data = System.Text.Encoding.UTF8.GetBytes(bfcpc.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\ModulePatch\Version_" + _currentVersion.ToString().Replace(".", "_") + ".cs";
            FileModulePatch fmp = new FileModulePatch(proj.GetProjectNamespace(), _currentVersion);
            item.Data = System.Text.Encoding.UTF8.GetBytes(fmp.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\WebServerObjectLocationRegistrationTask.cs";
            WebServerFileObjLocRegTask wsolrs = new WebServerFileObjLocRegTask(proj.NamespacePrefix + "." + proj.ModuleName, proj.ModuleName, proj.GetProjectNamespace());
            item.Data = System.Text.Encoding.UTF8.GetBytes(wsolrs.Generate());
            files.Add(item);

            return files;
        }


        protected List<ArchiveItem> GetWebAdminProjectFiles()
        {
            List<ArchiveItem> files = new List<ArchiveItem>();
            WebAdminProject proj = (WebAdminProject)_projects[5];

            ArchiveItem item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\" + proj.GetProjectNamespace() + ".csproj";
            item.Data = System.Text.Encoding.UTF8.GetBytes(proj.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\Properties\AssemblyInfo.cs";
            FileAssemblyInfoCS assinfo = new FileAssemblyInfoCS();
            assinfo.AssemblyTitle = proj.GetProjectNamespace();
            assinfo.AssemblyGuid = proj.ProjectGuid;
            assinfo.AssemblyFileVersion = _currentVersion.ToString();
            item.Data = System.Text.Encoding.UTF8.GetBytes(assinfo.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\packages.config";
            WebAdminFilePackagesConfig bfcpc = new WebAdminFilePackagesConfig();
            item.Data = System.Text.Encoding.UTF8.GetBytes(bfcpc.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\Web.config";
            WebAdminFileWebConfig wawc = new WebAdminFileWebConfig();
            item.Data = System.Text.Encoding.UTF8.GetBytes(wawc.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\Web.Debug.config";
            WebAdminFileWebConfigDebug wawcd = new WebAdminFileWebConfigDebug();
            item.Data = System.Text.Encoding.UTF8.GetBytes(wawcd.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\Web.Release.config";
            WebAdminFileWebConfigRelease wawcr = new WebAdminFileWebConfigRelease();
            item.Data = System.Text.Encoding.UTF8.GetBytes(wawcr.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\Controllers\AdminController.cs";
            WebAdminFileControllerAdmin waca = new WebAdminFileControllerAdmin(proj.NamespacePrefix + "." + proj.ModuleName);
            item.Data = System.Text.Encoding.UTF8.GetBytes(waca.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\Views\Web.config";
            WebAdminFileWebConfig wavwc = new WebAdminFileWebConfig();
            item.Data = System.Text.Encoding.UTF8.GetBytes(wavwc.Generate());
            files.Add(item);

            item = new ArchiveItem();
            item.Filename = proj.GetProjectNamespace() + @"\Views\Admin\Index.cshtml";
            WebAdminFileViewsAdminIndex wavai = new WebAdminFileViewsAdminIndex(proj.ModuleName);
            item.Data = System.Text.Encoding.UTF8.GetBytes(wavai.Generate());
            files.Add(item);

            return files;
        }
    }
}
