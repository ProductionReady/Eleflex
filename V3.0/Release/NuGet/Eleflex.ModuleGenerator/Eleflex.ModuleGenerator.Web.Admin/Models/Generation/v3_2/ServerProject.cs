using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class ServerProject : Project, IGenerate
    {

        string _rootNamespace;
        string _bizNamespace;
        string _messagesNamespace;
        Guid _bizGuid;
        Guid _messageGuid;

        public ServerProject(string rootNamespace, string bizNamespace, string messagesNamespace, Guid bizGuid, Guid messageGuid)
        {
            _rootNamespace = rootNamespace;
            _bizNamespace = bizNamespace;
            _messagesNamespace = messagesNamespace;
            _bizGuid = bizGuid;
            _messageGuid = messageGuid;
        }

        public string Generate()
        {
            string data = string.Empty;

            string rootNamespace = this.NamespacePrefix + "." + this.ModuleName;
            
            string projectNamespace = GetProjectNamespace();

            data +=
    @"<?xml version=""1.0"" encoding=""utf-8""?>
<Project ToolsVersion=""14.0"" DefaultTargets=""Build"" xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
  <Import Project=""$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props"" Condition=""Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')"" />
  <PropertyGroup>
    <Configuration Condition="" '$(Configuration)' == '' "">Debug</Configuration>
    <Platform Condition="" '$(Platform)' == '' "">AnyCPU</Platform>
    <ProjectGuid>{4D951AB2-ABAA-4685-ACFF-30140AC569C7}</ProjectGuid>
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
    <Reference Include=""AutoMapper, Version=4.1.1.0, Culture=neutral, PublicKeyToken=be96cd2c38ef1005, processorArchitecture=MSIL"">
      <HintPath>..\packages\AutoMapper.4.1.1\lib\net45\AutoMapper.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""Eleflex.Core, Version=3.0.0.0, Culture=neutral, processorArchitecture=MSIL"">
      <HintPath>..\packages\Eleflex.3.2.1\lib\net46\Eleflex.Core.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""Eleflex.Messages, Version=3.0.0.0, Culture=neutral, processorArchitecture=MSIL"">
      <HintPath>..\packages\Eleflex.Messages.3.2.1\lib\net46\Eleflex.Messages.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""Eleflex.Storage.EntityFramework, Version=3.0.0.0, Culture=neutral, processorArchitecture=MSIL"">
      <HintPath>..\packages\Eleflex.Storage.EntityFramework.3.2.1\lib\net46\Eleflex.Storage.EntityFramework.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, processorArchitecture=MSIL"">
      <HintPath>..\packages\EntityFramework.6.1.3\lib\net45\EntityFramework.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""EntityFramework.SqlServer, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, processorArchitecture=MSIL"">
      <HintPath>..\packages\EntityFramework.6.1.3\lib\net45\EntityFramework.SqlServer.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""LinqKit, Version=1.1.3.1, Culture=neutral, PublicKeyToken=bc217f8844052a91, processorArchitecture=MSIL"">
      <HintPath>..\packages\LinqKit.1.1.3.1\lib\net45\LinqKit.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""System"" />
    <Reference Include=""System.ComponentModel.DataAnnotations"" />
    <Reference Include=""System.Core"" />
    <Reference Include=""System.Runtime.Serialization"" />
    <Reference Include=""System.Security"" />
    <Reference Include=""System.ServiceModel"" />
    <Reference Include=""System.Xml.Linq"" />
    <Reference Include=""System.Data.DataSetExtensions"" />
    <Reference Include=""Microsoft.CSharp"" />
    <Reference Include=""System.Data"" />
    <Reference Include=""System.Net.Http"" />
    <Reference Include=""System.Xml"" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include=""" + _rootNamespace + @".Storage.EF.Azure\" + ModuleName + @"AzureConstants.cs"" />
    <Compile Include=""" + _rootNamespace + @".Storage.EF.Azure\Version_3_2_1_0.cs"" />
    <Compile Include=""" + _rootNamespace + @".Storage.EF.SQLServer\" + ModuleName + @"SQLServerConstants.cs"" />
    <Compile Include=""" + _rootNamespace + @".Storage.EF.SQLServer\Version_3_2_1_0.cs"" />
    <Compile Include=""" + GetProjectNamespace().Replace(".","") + @"Constants.cs"" />
    <Compile Include=""ModulePatch\Version_3_2_1_0.cs"" />
    <Compile Include=""Properties\AssemblyInfo.cs"" />
  </ItemGroup>
  <ItemGroup>
    <Content Include=""" + _rootNamespace.Replace(".","_") + @"_Services_WCF_AutoMapper_CodeGen.tt"">
      <Generator>TextTemplatingFileGenerator</Generator>
      <LastGenOutput></LastGenOutput>
    </Content>
    <Content Include=""" + _rootNamespace.Replace(".", "_") + @"_Services_WCF_Server_CodeGen.tt"">
      <Generator>TextTemplatingFileGenerator</Generator>
      <LastGenOutput></LastGenOutput>
    </Content>
    <Content Include=""" + _rootNamespace.Replace(".", "_") + @"_Storage_EF_AutoMapper_CodeGen.tt"">
      <Generator>TextTemplatingFileGenerator</Generator>
      <LastGenOutput></LastGenOutput>
    </Content>
    <Content Include=""" + _rootNamespace.Replace(".", "_") + @"_Storage_EF_CodeGen.tt"">
      <Generator>TextTemplatingFileGenerator</Generator>
      <LastGenOutput></LastGenOutput>
    </Content>
  </ItemGroup>
  <ItemGroup>
    <Service Include=""{508349B6-6B84-4DF5-91F0-309BEEBAD82D}"" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include=""..\" + _messagesNamespace + @"\" + _messagesNamespace + @".csproj"">
      <Project>" + _messageGuid.ToString("B").ToLower() + @"</Project>
      <Name>" + _messagesNamespace + @"</Name>
    </ProjectReference>
    <ProjectReference Include=""..\" + _bizNamespace + @"\" + _bizNamespace + @".csproj"">
      <Project>" + _bizGuid.ToString("B").ToLower() + @"</Project>
      <Name>" + _messagesNamespace + @"</Name>
    </ProjectReference>
  </ItemGroup>
  <ItemGroup>
    <None Include=""App.config"" />
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
