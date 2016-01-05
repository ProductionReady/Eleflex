using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class ServerFilePackagesConfig : IGenerate
    {

        public string Generate()
        {
            return @"<?xml version=""1.0"" encoding=""utf-8""?>
<packages>
  <package id=""AutoMapper"" version=""4.1.1"" targetFramework=""net46"" />
  <package id=""Eleflex"" version=""3.2.1"" targetFramework=""net46"" />
  <package id=""Eleflex.Messages"" version=""3.2.1"" targetFramework=""net46"" />
  <package id=""Eleflex.Storage.EntityFramework"" version=""3.2.1"" targetFramework=""net46"" />
  <package id=""EntityFramework"" version=""6.1.3"" targetFramework=""net46"" />
  <package id=""LinqKit"" version=""1.1.3.1"" targetFramework=""net46"" />
</packages>";
        }
    }
}
