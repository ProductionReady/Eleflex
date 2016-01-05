using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class FileAssemblyInfoCS : IGenerate
    {
    
        public FileAssemblyInfoCS()
        {
            AssemblyTitle = "MyProject";
            AssemblyVersion = "3.0.0.0";
            AssemblyFileVersion = "3.2.1.0";
            AssemblyCopyright = "Copyright © 2016";
        }

        public string AssemblyTitle { get; set; }
        public string AssemblyDescription { get; set; }
        public string AssemblyConfiguration { get; set; }
        public string AssemblyCompany { get; set; }
        public string AssemblyProduct { get; set; }
        public string AssemblyCopyright { get; set; }
        public string AssemblyTrademark { get; set; }
        public string AssemblyCulture { get; set; }
        public string AssemblyVersion { get; set; }
        public string AssemblyFileVersion { get; set; }

        public Guid AssemblyGuid { get; set; }


        public string Generate()
        {
            return @"using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle(""" + AssemblyTitle + @""")]
[assembly: AssemblyDescription(""" + AssemblyDescription + @""")]
[assembly: AssemblyConfiguration(""" + AssemblyConfiguration + @""")]
[assembly: AssemblyCompany(""" + AssemblyCompany + @""")]
[assembly: AssemblyProduct(""" + AssemblyProduct + @""")]
[assembly: AssemblyCopyright(""" + AssemblyCopyright + @""")]
[assembly: AssemblyTrademark(""" + AssemblyTrademark + @""")]
[assembly: AssemblyCulture(""" + AssemblyCulture + @""")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid(""" + AssemblyGuid.ToString().ToLower() + @""")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion(""1.0.*"")]
[assembly: AssemblyVersion(""" + AssemblyVersion + @""")]
[assembly: AssemblyFileVersion(""" + AssemblyFileVersion + @""")]
";
        }
    }
}
