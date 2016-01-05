using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class WebClientFileObjLocRegTask : IGenerate
    {

        string _namespaceRoot;
        string _namespaceName;
        string _moduleName;

        public WebClientFileObjLocRegTask(string namespaceRoot, string moduleName, string namespaceName)
        {
            _namespaceRoot = namespaceRoot;
            _moduleName = moduleName;
            _namespaceName = namespaceName;
        }

        public string Generate()
        {
            

            return @"using Eleflex;
using Eleflex.Services.WCF;
using " + _namespaceRoot + @".Services.WCF.Message;

namespace " + _namespaceName + @"
{
    /// <summary>
    /// Represents an object location registration task configuring structuremap for the system.
    /// </summary>
    [ObjectLocationRegistrationTask]
    public partial class WebClientObjectLocationRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public WebClientObjectLocationRegistrationTask()
        {
            Description = ""This tasks registers object location configurations for the " + _namespaceRoot + @" Module."";
        }

        /// <summary>
        /// Start registration logic.
        /// </summary>
        /// <param name=""taskOptions""></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            StructureMap.IContainer container = ObjectLocator.Container as StructureMap.IContainer;
            container.Configure(x =>
            {
                //Service request dispatcher
                x.For<I" + _moduleName + @"RequestDispatcher>().Use<" + _moduleName + @"RequestDispatcher>()
                    .Ctor<string>(WCFConstants.IWCFCOMMANDREQUESTDISPATCHER_CONSTRUCTORPARAM_ENDPOINT).Is(WCFConstants.SERVICE_ENDPOINT_NAME_DEFAULT); //endpoint = ""EleflexDefault"" see web.config system.servicemodel.client

            });

            return base.Register(taskOptions);
        }
    }
}

";
        }
    }
}
