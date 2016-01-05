using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class WebClientProject : Project, IGenerate
    {

        string _namespaceWebAdmin;
        string _namespaceMessages;
        Guid _messagesProjectGuid;
        Guid _webAdminProjectGuid;

        public WebClientProject(string namespaceWebAdmin, string namespaceMessages, Guid webAdminProjectGuid, Guid messagesProjectGuid)
        {
            _namespaceMessages = namespaceMessages;
            _namespaceWebAdmin = namespaceWebAdmin;
            _messagesProjectGuid = messagesProjectGuid;
            _webAdminProjectGuid = webAdminProjectGuid;
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
    <RootNamespace>" + projectNamespace + @"</RootNamespace>
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
    <Reference Include=""Eleflex.Messages, Version=3.0.0.0, Culture=neutral, processorArchitecture=MSIL"">
      <HintPath>..\packages\Eleflex.Messages.3.2.1\lib\net46\Eleflex.Messages.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""MvcCodeRouting, Version=1.3.0.0, Culture=neutral, PublicKeyToken=bf7cf9743009fe42, processorArchitecture=MSIL"">
      <HintPath>..\packages\MvcCodeRouting.1.3.0\lib\net40\MvcCodeRouting.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""StructureMap, Version=4.0.0.315, Culture=neutral, processorArchitecture=MSIL"">
      <HintPath>..\packages\structuremap.4.0.0.315\lib\net40\StructureMap.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""StructureMap.Net4, Version=4.0.0.315, Culture=neutral, processorArchitecture=MSIL"">
      <HintPath>..\packages\structuremap.4.0.0.315\lib\net40\StructureMap.Net4.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""System"" />
    <Reference Include=""System.Core"" />
    <Reference Include=""System.Xml.Linq"" />
    <Reference Include=""System.Data.DataSetExtensions"" />
    <Reference Include=""Microsoft.CSharp"" />
    <Reference Include=""System.Data"" />
    <Reference Include=""System.Net.Http"" />
    <Reference Include=""System.ServiceModel"" />
    <Reference Include=""System.Xml"" />
    <Reference Include=""System.Web"" />
    <Reference Include=""System.Web.Routing"" />
  </ItemGroup>
    <ItemGroup>
    <ProjectReference Include=""..\" + _namespaceMessages  + @"\" + _namespaceMessages + @".csproj"">
      <Project>" + _messagesProjectGuid.ToString("B") + @"</Project>
      <Name>PR.Dev.TimeClock.Messages</Name>
    </ProjectReference>
        <ProjectReference Include=""..\" + _namespaceWebAdmin + @"\" + _namespaceWebAdmin + @".csproj"">
          <Project>" + _webAdminProjectGuid.ToString("B") + @"</Project>
          <Name>PR.Dev.TimeClock.Web.Admin</Name>
        </ProjectReference>
  </ItemGroup>
  <ItemGroup>
    <Compile Include=""" + projectNamespace.Replace(".","") + @"Constants.cs"" />
    <Compile Include=""ModulePatch\Version_3_2_1_0.cs"" />
    <Compile Include=""Properties\AssemblyInfo.cs"" />
    <Compile Include=""WebClientObjectLocationRegistrationTask.cs"" />
    <Compile Include=""WebClientRoutesStartupTask.cs"" />
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
