﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class ServerFileAppConfig : IGenerate
    {

        public string Generate()
        {
            return @"<?xml version=""1.0"" encoding=""utf-8""?>
<configuration>
  <configSections>
    <!-- For more information on Entity Framework configuration, visit http://go.microsoft.com/fwlink/?LinkID=237468 -->
    <section name=""entityFramework"" type=""System.Data.Entity.Internal.ConfigFile.EntityFrameworkSection, EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"" requirePermission=""false"" />
  </configSections>
  <entityFramework>
    <defaultConnectionFactory type=""System.Data.Entity.Infrastructure.LocalDbConnectionFactory, EntityFramework"">
      <parameters>
        <parameter value=""mssqllocaldb"" />
      </parameters>
    </defaultConnectionFactory>
    <providers>
      <provider invariantName=""System.Data.SqlClient"" type=""System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer"" />
    </providers>
  </entityFramework>
  <connectionStrings>

  </connectionStrings>
</configuration>";
        }
    }
}
