﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class MessagesFilePackagesConfig : IGenerate
    {

        public string Generate()
        {
            return @"<?xml version=""1.0"" encoding=""utf-8""?>
<packages>
  <package id=""Eleflex"" version=""3.2.1"" targetFramework=""net46"" />
  <package id=""Eleflex.Messages"" version=""3.2.1"" targetFramework=""net46"" />
</packages>";
        }
    }
}
