using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class WebAdminFileViewWebConfig : IGenerate
    {

        string _rootNamespace;

        public WebAdminFileViewWebConfig(string rootNamespace)
        {
            _rootNamespace = rootNamespace;
        }

        public string Generate()
        {
            return @"<?xml version=""1.0""?>

<configuration>
  <configSections>
    <sectionGroup name=""system.web.webPages.razor"" type=""System.Web.WebPages.Razor.Configuration.RazorWebSectionGroup, System.Web.WebPages.Razor, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35"">
      <section name=""host"" type=""System.Web.WebPages.Razor.Configuration.HostSection, System.Web.WebPages.Razor, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35"" requirePermission=""false"" />
      <section name=""pages"" type=""System.Web.WebPages.Razor.Configuration.RazorPagesSection, System.Web.WebPages.Razor, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35"" requirePermission=""false"" />
    </sectionGroup>
  </configSections>

  <system.web.webPages.razor>
    <host factoryType=""System.Web.Mvc.MvcWebRazorHostFactory, System.Web.Mvc, Version=5.2.3.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35"" />
    <pages pageBaseType=""System.Web.Mvc.WebViewPage"">
      <namespaces>
        <add namespace=""System.Web.Mvc"" />
        <add namespace=""System.Web.Mvc.Ajax"" />
        <add namespace=""System.Web.Mvc.Html"" />
        <add namespace=""System.Web.Optimization""/>
        <add namespace=""System.Web.Routing"" />
        <add namespace="""  + _rootNamespace + @".Web.Admin"" />
      </namespaces>
    </pages>
  </system.web.webPages.razor>

  <appSettings>
    <add key=""webpages:Enabled"" value=""false"" />
  </appSettings>

  <system.webServer>
    <handlers>
      <remove name=""BlockViewHandler""/>
      <add name=""BlockViewHandler"" path=""*"" verb=""*"" preCondition=""integratedMode"" type=""System.Web.HttpNotFoundHandler"" />
    </handlers>
  </system.webServer>

  <system.web>
    <compilation>
      <assemblies>
        <add assembly=""System.Web.Mvc, Version=5.2.3.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35"" />
      </assemblies>
    </compilation>
  </system.web>
</configuration>
";
        }

    }
}
