﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class WebAdminProject : Project, IGenerate
    {


        string _messagesNamespace;
        Guid _messageGuid;

        public WebAdminProject(Guid projectGuid, string messagesNamespace, Guid messageGuid)
        {
            _messagesNamespace = messagesNamespace;
            _messageGuid = messageGuid;
            ProjectGuid = projectGuid;
        }

        public string Generate()
        {
            string data = string.Empty;

            string projectNamespace = GetProjectNamespace();

            data +=
    @"<?xml version=""1.0"" encoding=""utf-8""?>
<Project ToolsVersion=""12.0"" DefaultTargets=""Build"" xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
  <Import Project=""..\packages\Microsoft.Net.Compilers.1.1.0\build\Microsoft.Net.Compilers.props"" Condition=""Exists('..\packages\Microsoft.Net.Compilers.1.1.0\build\Microsoft.Net.Compilers.props')"" />
  <Import Project=""..\packages\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.1.0.0\build\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.props"" Condition=""Exists('..\packages\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.1.0.0\build\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.props')"" />
  <Import Project=""$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props"" Condition=""Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')"" />
  <PropertyGroup>
    <Configuration Condition="" '$(Configuration)' == '' "">Debug</Configuration>
    <Platform Condition="" '$(Platform)' == '' "">AnyCPU</Platform>
    <ProductVersion>
    </ProductVersion>
    <SchemaVersion>2.0</SchemaVersion>
    <ProjectGuid>" + ProjectGuid.ToString("B") + @"</ProjectGuid>
    <ProjectTypeGuids>{349c5851-65df-11da-9384-00065b846f21};{fae04ec0-301f-11d3-bf4b-00c04f79efbc}</ProjectTypeGuids>
    <OutputType>Library</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <RootNamespace>" + projectNamespace + @"</RootNamespace>
    <AssemblyName>" + projectNamespace + @"</AssemblyName>
    <TargetFrameworkVersion>v4.6</TargetFrameworkVersion>
    <UseIISExpress>true</UseIISExpress>
    <IISExpressSSLPort />
    <IISExpressAnonymousAuthentication />
    <IISExpressWindowsAuthentication />
    <IISExpressUseClassicPipelineMode />
    <UseGlobalApplicationHostFile />
    <NuGetPackageImportStamp>
    </NuGetPackageImportStamp>
  </PropertyGroup>
  <PropertyGroup Condition="" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' "">
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup Condition="" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' "">
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include=""AutoMapper, Version=4.1.1.0, Culture=neutral, PublicKeyToken=be96cd2c38ef1005, processorArchitecture=MSIL"">
      <HintPath>..\packages\AutoMapper.4.1.1\lib\net45\AutoMapper.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""Common.Logging, Version=3.3.1.0, Culture=neutral, PublicKeyToken=af08829b84f0328e, processorArchitecture=MSIL"">
      <HintPath>..\packages\Common.Logging.3.3.1\lib\net40\Common.Logging.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""Common.Logging.Core, Version=3.3.1.0, Culture=neutral, PublicKeyToken=af08829b84f0328e, processorArchitecture=MSIL"">
      <HintPath>..\packages\Common.Logging.Core.3.3.1\lib\net40\Common.Logging.Core.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""CommonServiceLocator.StructureMapAdapter.Unofficial, Version=3.0.4.125, Culture=neutral, processorArchitecture=MSIL"">
      <HintPath>..\packages\CommonServiceLocator.StructureMapAdapter.Unofficial.3.0.4.125\lib\Net40\CommonServiceLocator.StructureMapAdapter.Unofficial.dll</HintPath>
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
    <Reference Include=""Eleflex.Web, Version=3.0.0.0, Culture=neutral, processorArchitecture=MSIL"">
      <HintPath>..\packages\Eleflex.Web.3.2.1\lib\net46\Eleflex.Web.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <HintPath>..\packages\Microsoft.AspNet.Identity.Core.2.2.1\lib\net45\Microsoft.AspNet.Identity.Core.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""Microsoft.AspNet.Identity.Owin, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <HintPath>..\packages\Microsoft.AspNet.Identity.Owin.2.2.1\lib\net45\Microsoft.AspNet.Identity.Owin.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <HintPath>..\packages\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.1.0.0\lib\net45\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""Microsoft.CSharp"" />
    <Reference Include=""Microsoft.Owin, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <HintPath>..\packages\Microsoft.Owin.3.0.1\lib\net45\Microsoft.Owin.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""Microsoft.Owin.Host.SystemWeb, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <HintPath>..\packages\Microsoft.Owin.Host.SystemWeb.3.0.1\lib\net45\Microsoft.Owin.Host.SystemWeb.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""Microsoft.Owin.Security, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <HintPath>..\packages\Microsoft.Owin.Security.3.0.1\lib\net45\Microsoft.Owin.Security.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""Microsoft.Owin.Security.Cookies, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <HintPath>..\packages\Microsoft.Owin.Security.Cookies.3.0.1\lib\net45\Microsoft.Owin.Security.Cookies.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""Microsoft.Owin.Security.OAuth, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <HintPath>..\packages\Microsoft.Owin.Security.OAuth.3.0.1\lib\net45\Microsoft.Owin.Security.OAuth.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""Microsoft.Practices.ServiceLocation, Version=1.3.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <HintPath>..\packages\CommonServiceLocator.1.3\lib\portable-net4+sl5+netcore45+wpa81+wp8\Microsoft.Practices.ServiceLocation.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""Microsoft.Web.Infrastructure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <HintPath>..\packages\Microsoft.Web.Infrastructure.1.0.0.0\lib\net40\Microsoft.Web.Infrastructure.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""Newtonsoft.Json, Version=7.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed, processorArchitecture=MSIL"">
      <HintPath>..\packages\Newtonsoft.Json.7.0.1\lib\net45\Newtonsoft.Json.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""Owin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=f0ebd12fd5e55cc5, processorArchitecture=MSIL"">
      <HintPath>..\packages\Owin.1.0\lib\net40\Owin.dll</HintPath>
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
    <Reference Include=""StructureMap.Web, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL"">
      <HintPath>..\packages\structuremap.web.4.0.0.315\lib\net40\StructureMap.Web.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""System.ServiceModel"" />
    <Reference Include=""System.Web.DynamicData"" />
    <Reference Include=""System.Web.Entity"" />
    <Reference Include=""System.Web.ApplicationServices"" />
    <Reference Include=""System.ComponentModel.DataAnnotations"" />
    <Reference Include=""System"" />
    <Reference Include=""System.Data"" />
    <Reference Include=""System.Core"" />
    <Reference Include=""System.Data.DataSetExtensions"" />
    <Reference Include=""System.Web.Extensions"" />
    <Reference Include=""System.Web.Helpers, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <HintPath>..\packages\Microsoft.AspNet.WebPages.3.2.3\lib\net45\System.Web.Helpers.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""System.Web.Mvc, Version=5.2.3.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <HintPath>..\packages\Microsoft.AspNet.Mvc.5.2.3\lib\net45\System.Web.Mvc.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""System.Web.Razor, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <HintPath>..\packages\Microsoft.AspNet.Razor.3.2.3\lib\net45\System.Web.Razor.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""System.Web.WebPages, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <HintPath>..\packages\Microsoft.AspNet.WebPages.3.2.3\lib\net45\System.Web.WebPages.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""System.Web.WebPages.Deployment, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <HintPath>..\packages\Microsoft.AspNet.WebPages.3.2.3\lib\net45\System.Web.WebPages.Deployment.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""System.Web.WebPages.Razor, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <HintPath>..\packages\Microsoft.AspNet.WebPages.3.2.3\lib\net45\System.Web.WebPages.Razor.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include=""System.Xml.Linq"" />
    <Reference Include=""System.Drawing"" />
    <Reference Include=""System.Web"" />
    <Reference Include=""System.Xml"" />
    <Reference Include=""System.Configuration"" />
    <Reference Include=""System.Web.Services"" />
    <Reference Include=""System.EnterpriseServices"" />
  </ItemGroup>
  <ItemGroup>
    <Content Include=""packages.config"" />
    <Content Include=""Views\Web.config"" />
    <EmbeddedResource Include=""Views\Admin\Index.cshtml"" />
    <None Include=""Web.Debug.config"">
      <DependentUpon>Web.config</DependentUpon>
    </None>
    <None Include=""Web.Release.config"">
      <DependentUpon>Web.config</DependentUpon>
    </None>
  </ItemGroup>
  <ItemGroup>
    <Content Include=""Web.config"" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include=""Controllers\AdminController.cs"" />
    <Compile Include=""Properties\AssemblyInfo.cs"" />
  </ItemGroup>
  <ItemGroup>
    <Folder Include=""Models\"" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include=""..\" + _messagesNamespace + @"\" + _messagesNamespace + @".csproj"">
      <Project>" + _messageGuid.ToString("B").ToLower() + @"</Project>
      <Name>" + _messagesNamespace + @"</Name>
    </ProjectReference>
  </ItemGroup>
  <PropertyGroup>
    <VisualStudioVersion Condition=""'$(VisualStudioVersion)' == ''"">10.0</VisualStudioVersion>
    <VSToolsPath Condition=""'$(VSToolsPath)' == ''"">$(MSBuildExtensionsPath32)\Microsoft\VisualStudio\v$(VisualStudioVersion)</VSToolsPath>
  </PropertyGroup>
  <Import Project=""$(MSBuildBinPath)\Microsoft.CSharp.targets"" />
  <Import Project=""$(VSToolsPath)\WebApplications\Microsoft.WebApplication.targets"" Condition=""'$(VSToolsPath)' != ''"" />
  <Import Project=""$(MSBuildExtensionsPath32)\Microsoft\VisualStudio\v10.0\WebApplications\Microsoft.WebApplication.targets"" Condition=""false"" />
  <ProjectExtensions>
    <VisualStudio>
      <FlavorProperties GUID=""{349c5851-65df-11da-9384-00065b846f21}"">
        <WebProjectProperties>
          <UseIIS>True</UseIIS>
          <AutoAssignPort>True</AutoAssignPort>
          <DevelopmentServerPort>32948</DevelopmentServerPort>
          <DevelopmentServerVPath>/</DevelopmentServerVPath>
          <IISUrl>http://localhost:32948/</IISUrl>
          <NTLMAuthentication>False</NTLMAuthentication>
          <UseCustomServer>False</UseCustomServer>
          <CustomServerUrl>
          </CustomServerUrl>
          <SaveServerSettingsInUserFile>False</SaveServerSettingsInUserFile>
        </WebProjectProperties>
      </FlavorProperties>
    </VisualStudio>
  </ProjectExtensions>
  <Target Name=""EnsureNuGetPackageBuildImports"" BeforeTargets=""PrepareForBuild"">
    <PropertyGroup>
      <ErrorText>This project references NuGet package(s) that are missing on this computer. Use NuGet Package Restore to download them.  For more information, see http://go.microsoft.com/fwlink/?LinkID=322105. The missing file is {0}.</ErrorText>
    </PropertyGroup>
    <Error Condition=""!Exists('..\packages\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.1.0.0\build\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.props')"" Text=""$([System.String]::Format('$(ErrorText)', '..\packages\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.1.0.0\build\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.props'))"" />
    <Error Condition=""!Exists('..\packages\Microsoft.Net.Compilers.1.1.0\build\Microsoft.Net.Compilers.props')"" Text=""$([System.String]::Format('$(ErrorText)', '..\packages\Microsoft.Net.Compilers.1.1.0\build\Microsoft.Net.Compilers.props'))"" />
  </Target>
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
