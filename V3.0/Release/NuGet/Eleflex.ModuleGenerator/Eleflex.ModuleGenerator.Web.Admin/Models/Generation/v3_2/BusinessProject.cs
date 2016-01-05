using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class BusinessProject : Project, IGenerate
    {

        public BusinessProject()
        {
        }

        public string Generate()
        {
            string data = string.Empty;

            string projectNamespace = GetProjectNamespace();
            data += 
    @"<?xml version=""1.0"" encoding=""utf-8""?>
<Project ToolsVersion=""14.0"" DefaultTargets=""Build"" xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
  <Import Project=""$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props"" Condition=""Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')"" />
  <PropertyGroup>
    <Configuration Condition="" '$(Configuration)' == '' "">Debug</Configuration>
    <Platform Condition="" '$(Platform)' == '' "">AnyCPU</Platform>
    <ProjectGuid>" + ProjectGuid.ToString("B") + @"</ProjectGuid>
    <OutputType>Library</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <RootNamespace>" + projectNamespace  + @"</RootNamespace>
    <AssemblyName>" + projectNamespace + @"</AssemblyName>
    <TargetFrameworkVersion>v4.6</TargetFrameworkVersion>
    <FileAlignment>512</FileAlignment>
  </PropertyGroup>
  <PropertyGroup Condition="" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' "">
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\Debug\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup Condition="" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' "">
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\Release\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include=""Eleflex.Core, Version=3.0.0.0, Culture=neutral, processorArchitecture=MSIL"">
      <HintPath>..\packages\Eleflex.3.2.1\lib\net46\Eleflex.Core.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""System"" />
    <Reference Include=""System.Core"" />
    <Reference Include=""System.Xml.Linq"" />
    <Reference Include=""System.Data.DataSetExtensions"" />
    <Reference Include=""Microsoft.CSharp"" />
    <Reference Include=""System.Data"" />
    <Reference Include=""System.Net.Http"" />
    <Reference Include=""System.Xml"" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include=""" + ModuleName + @"Constants.cs"" />
    <Compile Include=""I" + ModuleName + @"StorageService.cs"" />
    <Compile Include=""Properties\AssemblyInfo.cs"" />
  </ItemGroup>
  <ItemGroup>
    <Content Include=""" + projectNamespace.Replace(".","_") + @"_CodeGen.tt"">
      <Generator>TextTemplatingFileGenerator</Generator>
      <LastGenOutput></LastGenOutput>
    </Content>
  </ItemGroup>
  <ItemGroup>
    <Service Include=""{508349B6-6B84-4DF5-91F0-309BEEBAD82D}"" />
  </ItemGroup>
  <ItemGroup>
    <None Include=""packages.config"" />
  </ItemGroup>
  <Import Project=""$(MSBuildToolsPath)\Microsoft.CSharp.targets"" />
  <!-- To modify your build process, add your task inside one of the targets below and uncomment it. 
       Other similar extension points exist, see Microsoft.Common.targets.
  <Target Name=""BeforeBuild"">
  </Target>
  <Target Name=""AfterBuild"">
  </Target>
  -->
</Project>";

            return data;
        }

    }
}
