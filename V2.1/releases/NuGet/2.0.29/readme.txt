-------------------
Welcome to ELEFLEX!
-------------------
ELEFLEX is a platform for building modular, domain-driven, service-oriented web applications and services.

---------------------
v2.0.29 Release Notes
---------------------
New Features:

Changes:
* Updated NuGet package for boostrap to 3.3.2
* Updated NuGet package for jQuery.UI.Combined to 1.11.3
* Updated NuGet package for Microsoft.AspNet.WebApi.OData to 5.4.0
* Updated NuGet package for Microsoft.AspNet.WebPages to 3.2.3
* Updated NuGet package for Microsoft.AspNet.Razor to 3.2.3
* Updated NuGet package for Microsoft.AspNet.Identity.Core to 2.2.0
* Updated NuGet package for Microsoft.AspNet.Identity.Owin to 2.2.0
* Updated NuGet package for Microsoft.AspNet.Mvc to 5.2.3
* Updated NuGet package for Microsoft.AspNet.WebApi.Client to 5.2.3
* Updated NuGet package for Microsoft.AspNet.WebApi.Core to 5.2.3
* Updated NuGet package for Microsoft.AspNet.WebApi.WebHost to 5.2.3
* Updated NuGet package for Microsoft.AspNet.WebApi to 5.2.3	
* Updated NuGet package for Microsoft.AspNet.WebPages.WebData to 3.2.3	
* Updated NuGet package for Microsoft.AspNet.WebPages.Data to 3.2.3
* Updated NuGet package for Microsoft.AspNet.WebApi.HelpPage to 3.2.3
* Updated NuGet package for Microsoft.AspNet.WebApi.Tracing to 3.2.3
* Updated NuGet package for Microsoft.AspNet.WebApi.Owin to 5.2.3
* Updated NuGet package for Microsoft.jQuery.Unobtrusive.Validation to 3.2.3
* Updated NuGet package for Microsoft.jQuery.Unobtrusive.Ajax to 3.2.3
* Updated NuGet package for Microsoft.Owin to 3.0.1
* Updated NuGet package for Microsoft.Owin.Host.SystemWeb to 3.0.1
* Updated NuGet package for Microsoft.Owin.Security to 3.0.1
* Updated NuGet package for Microsoft.Owin.Security.OAuth to 3.0.1
* Updated NuGet package for Microsoft.Owin.Security.Cookies to 3.0.1
* Updated NuGet package for Microsoft.Owin.Security.Google to 3.0.1
* Updated NuGet package for Microsoft.Owin.Security.Facebook to 3.0.1
* Updated NuGet package for Microsoft.Owin.Security.MicrosoftAccount to 3.0.1
* Updated NuGet package for Microsoft.Owin.Security.Twitter to 3.0.1

Issues Fixed:

-----------------------------
New Installation Instructions
-----------------------------
It is recommended that you create a new ASP.NET MVC 4 Web Application (Empty Template) and then add the Eleflex NuGet package. 
The following steps must be completed to finish setup of a new ELEFLEX installation:

1) Create a new SQL Server Database (v2005 or greater). ELEFLEX Modules will automatically create tables/procedures/data as needed with this.

2) Change the web.config file to replace the "EleflexDefault" connection string with the database you just created in step 1.
<connectionStrings>
	<add name="EleflexDefault" connectionString="server=localhost;database=Eleflex;user id=test;password=test;" />
</connectionStrings>

3) Change the web.config file to update the client port number to access ELEFLEX services. 
Replace the port number "16185" with your web application's port number, found by going into your project properties and clicking "Web" from left menu.
<system.serviceModel>
	<client>
		<endpoint address="http://localhost:16185/EleflexService.svc" name="EleflexDefault" behaviorConfiguration="EleflexBehavior" binding="basicHttpBinding" bindingConfiguration="EleflexBinding" contract="Eleflex.Services.IServiceCommandHandler" />
	<client>
</system.serviceModel>

After completing the above steps, run the application. The ELEFLEX modules will self-update to create database tables and data as needed.

We will be updating the application frequently, check NuGet updates for new information.

Happy Coding!
Production Ready, LLC
http://www.ProductionReady.com
