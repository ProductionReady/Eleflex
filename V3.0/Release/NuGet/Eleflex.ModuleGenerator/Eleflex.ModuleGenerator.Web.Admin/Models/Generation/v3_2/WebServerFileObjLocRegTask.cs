using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class WebServerFileObjLocRegTask : IGenerate
    {

        string _namespaceRoot;
        string _namespaceName;
        string _moduleName;


        public WebServerFileObjLocRegTask(string namespaceRoot, string moduleName, string namespaceName)
        {
            _namespaceRoot = namespaceRoot;
            _moduleName = moduleName;
            _namespaceName = namespaceName;
        }

        public string Generate()
        {


            return @"//using Eleflex;
//using " + _namespaceRoot + @";
//using " + _namespaceRoot + @".Storage.EF;

//namespace " + _namespaceName + @"
//{
//    /// <summary>
//    /// Represents an object location registration task configuring structuremap for the system.
//    /// </summary>
//    [ObjectLocationRegistrationTask]
//    public partial class WebServerObjectLocationRegistrationTask : RegistrationTask
//    {

//        /// <summary>
//        /// Constructor.
//        /// </summary>
//        public WebServerObjectLocationRegistrationTask()
//        {
//            Description = ""This tasks registers object location configurations for the " + _namespaceRoot + @" Module."";
//        }

//        /// <summary>
//        /// Start registration logic.
//        /// </summary>
//        /// <param name=""taskOptions""></param>
//        /// <returns></returns>
//        public override bool Register(ITaskOptions taskOptions)
//        {
//            StructureMap.IContainer container = ObjectLocator.Container as StructureMap.IContainer;
//            container.Configure(x =>
//            {
//                //Storage service
//                x.For<I" + _moduleName + @"StorageService>().Use<" + _moduleName + @"StorageService>()
//                    .Ctor<string>(StorageConstants.ISTORAGESERVICE_CONSTUCTORPARAM_CONNECTIONSTRING).Is(StorageConstants.CONNECTIONSTRING_KEY_DEFAULT) //connectionString = ""EleflexDefault"" see web.config connectionStrings
//                    .Ctor<string>(StorageConstants.ISTORAGESERVICE_CONSTUCTORPARAM_VERSIONINGSTORAGECONFIG).Is(" + _namespaceRoot + @".Storage.EF.SQLServer." + _moduleName + @"SQLServerConstants.VERSIONING_STORAGE_CONFIG);
//            });

//            return base.Register(taskOptions);
//        }
//    }
//}
";
        }
    }
}
