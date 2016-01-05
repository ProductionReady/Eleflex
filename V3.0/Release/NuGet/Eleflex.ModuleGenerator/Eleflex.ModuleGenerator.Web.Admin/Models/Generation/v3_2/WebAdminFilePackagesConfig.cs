using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class WebAdminFilePackagesConfig : IGenerate
    {

        public string Generate()
        {
            return @"<?xml version=""1.0"" encoding=""utf-8""?>
<packages>
  <package id=""AutoMapper"" version=""4.1.1"" targetFramework=""net46"" />
  <package id=""Common.Logging"" version=""3.3.1"" targetFramework=""net46"" />
  <package id=""Common.Logging.Core"" version=""3.3.1"" targetFramework=""net46"" />
  <package id=""CommonServiceLocator"" version=""1.3"" targetFramework=""net46"" />
  <package id=""CommonServiceLocator.StructureMapAdapter.Unofficial"" version=""3.0.4.125"" targetFramework=""net46"" />
  <package id=""Eleflex"" version=""3.2.1"" targetFramework=""net46"" />
  <package id=""Eleflex.Messages"" version=""3.2.1"" targetFramework=""net46"" />
  <package id=""Eleflex.Web"" version=""3.2.1"" targetFramework=""net46"" />
  <package id=""Microsoft.AspNet.Identity.Core"" version=""2.2.1"" targetFramework=""net46"" />
  <package id=""Microsoft.AspNet.Identity.Owin"" version=""2.2.1"" targetFramework=""net46"" />
  <package id=""Microsoft.AspNet.Mvc"" version=""5.2.3"" targetFramework=""net46"" />
  <package id=""Microsoft.AspNet.Razor"" version=""3.2.3"" targetFramework=""net46"" />
  <package id=""Microsoft.AspNet.WebPages"" version=""3.2.3"" targetFramework=""net46"" />
  <package id=""Microsoft.CodeDom.Providers.DotNetCompilerPlatform"" version=""1.0.0"" targetFramework=""net46"" />
  <package id=""Microsoft.Net.Compilers"" version=""1.1.0"" targetFramework=""net46"" developmentDependency=""true"" />
  <package id=""Microsoft.Owin"" version=""3.0.1"" targetFramework=""net46"" />
  <package id=""Microsoft.Owin.Host.SystemWeb"" version=""3.0.1"" targetFramework=""net46"" />
  <package id=""Microsoft.Owin.Security"" version=""3.0.1"" targetFramework=""net46"" />
  <package id=""Microsoft.Owin.Security.Cookies"" version=""3.0.1"" targetFramework=""net46"" />
  <package id=""Microsoft.Owin.Security.OAuth"" version=""3.0.1"" targetFramework=""net46"" />
  <package id=""Microsoft.Web.Infrastructure"" version=""1.0.0.0"" targetFramework=""net46"" />
  <package id=""Newtonsoft.Json"" version=""7.0.1"" targetFramework=""net46"" />
  <package id=""Owin"" version=""1.0"" targetFramework=""net46"" />
  <package id=""structuremap"" version=""4.0.0.315"" targetFramework=""net46"" />
  <package id=""structuremap.web"" version=""4.0.0.315"" targetFramework=""net46"" />
</packages>";
        }
    }
}
