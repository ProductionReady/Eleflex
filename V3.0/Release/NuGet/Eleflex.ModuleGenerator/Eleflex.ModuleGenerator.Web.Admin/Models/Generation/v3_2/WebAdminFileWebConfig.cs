﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class WebAdminFileWebConfig : IGenerate
    {

        public string Generate()
        {
            return @"<?xml version=""1.0"" encoding=""utf-8""?>
<!--
  For more information on how to configure your ASP.NET application, please visit
  http://go.microsoft.com/fwlink/?LinkId=169433
  -->
<configuration>
  <system.web>
    <compilation debug=""true"" targetFramework=""4.6"" />
    <httpRuntime targetFramework=""4.6"" />
  </system.web>
  <system.codedom>
    <compilers>
      <compiler language=""c#;cs;csharp"" extension="".cs"" type=""Microsoft.CodeDom.Providers.DotNetCompilerPlatform.CSharpCodeProvider, Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"" warningLevel=""4"" compilerOptions=""/langversion:6 /nowarn:1659;1699;1701"" />
      <compiler language=""vb;vbs;visualbasic;vbscript"" extension="".vb"" type=""Microsoft.CodeDom.Providers.DotNetCompilerPlatform.VBCodeProvider, Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"" warningLevel=""4"" compilerOptions=""/langversion:14 /nowarn:41008 /define:_MYTYPE=\&quot;Web\&quot; /optionInfer+"" />
    </compilers>
  </system.codedom>
  <runtime>
    <assemblyBinding xmlns=""urn:schemas-microsoft-com:asm.v1"">
      <dependentAssembly>
        <assemblyIdentity name=""System.Web.Helpers"" publicKeyToken=""31bf3856ad364e35"" />
        <bindingRedirect oldVersion=""1.0.0.0-3.0.0.0"" newVersion=""3.0.0.0"" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name=""System.Web.WebPages"" publicKeyToken=""31bf3856ad364e35"" />
        <bindingRedirect oldVersion=""1.0.0.0-3.0.0.0"" newVersion=""3.0.0.0"" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name=""System.Web.Mvc"" publicKeyToken=""31bf3856ad364e35"" />
        <bindingRedirect oldVersion=""1.0.0.0-5.2.3.0"" newVersion=""5.2.3.0"" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name=""Newtonsoft.Json"" publicKeyToken=""30ad4fe6b2a6aeed"" culture=""neutral"" />
        <bindingRedirect oldVersion=""0.0.0.0-7.0.0.0"" newVersion=""7.0.0.0"" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name=""Microsoft.Owin"" publicKeyToken=""31bf3856ad364e35"" culture=""neutral"" />
        <bindingRedirect oldVersion=""0.0.0.0-3.0.1.0"" newVersion=""3.0.1.0"" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name=""Microsoft.Owin.Security.OAuth"" publicKeyToken=""31bf3856ad364e35"" culture=""neutral"" />
        <bindingRedirect oldVersion=""0.0.0.0-3.0.1.0"" newVersion=""3.0.1.0"" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name=""Microsoft.Owin.Security"" publicKeyToken=""31bf3856ad364e35"" culture=""neutral"" />
        <bindingRedirect oldVersion=""0.0.0.0-3.0.1.0"" newVersion=""3.0.1.0"" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name=""Microsoft.Owin.Security.Cookies"" publicKeyToken=""31bf3856ad364e35"" culture=""neutral"" />
        <bindingRedirect oldVersion=""0.0.0.0-3.0.1.0"" newVersion=""3.0.1.0"" />
      </dependentAssembly>
    </assemblyBinding>
  </runtime>
</configuration>";
        }

    }
}
