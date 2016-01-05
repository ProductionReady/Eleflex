using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class Solution : IGenerate
    {


        List<Project> _projects = null;

        public Solution(List<Project> projects)
        {
            _projects = projects;
        }

        public string Generate()
        {
            string data = string.Empty;
            data +=  @"
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio 14
VisualStudioVersion = 14.0.23107.0
MinimumVisualStudioVersion = 10.0.40219.1";

            foreach (Project proj in _projects)
            {
                string projectNamespace = proj.GetProjectNamespace();

                data += @"
Project("""  + GenerationConstants.VS2015_SOLUTIONITEM_PROJECT.ToString("B") + @""") = """ + projectNamespace + @""", """ + projectNamespace + @"\" + projectNamespace + @".csproj"", """ + proj.ProjectGuid.ToString("B") + @"""
EndProject";                
                    
            }
            data += @"
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution";

            foreach (Project proj in _projects)
            {

                data += @"
		" + proj.ProjectGuid.ToString("B") + @".Debug|Any CPU.ActiveCfg = Debug|Any CPU
		" + proj.ProjectGuid.ToString("B") + @".Debug|Any CPU.Build.0 = Debug|Any CPU
		" + proj.ProjectGuid.ToString("B") + @".Release|Any CPU.ActiveCfg = Release|Any CPU
		" + proj.ProjectGuid.ToString("B") + @".Release|Any CPU.Build.0 = Release|Any CPU";

            }

                data += @"
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
EndGlobal
";

            return data;
        }
    }
}
