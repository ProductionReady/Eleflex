-------------------
Welcome to ELEFLEX!
-------------------
ELEFLEX is a platform for building modular, domain-driven, service-oriented web applications and services.

---------------------
v2.0.36 Release Notes
---------------------
Our next release should be the official v2.1.0 release!
	
New Features:
* Enabled password reset link on login page, logic and added smtp client to use default web.config mailsettings for sending email	
* Creating new user in security admin sends confirmation email along with temporary password
* Added web.config entries for system.net mailsettings to configure default SMTP email configurations used in code

Changes:
* First user to register gets admin role and is automatically signed in. All others are granted user role and require email validation to login
* Existing users in the system will require email validation before they can log in since their email is not confirmed
* Change "Inactive" labels to "Status" throughout security web admin and added "Active"/"Inactive" for grid views
* Removed NuGet package for Microsoft.AspNet.Providers.LocalDb, not in use
* Removed NuGet package for Microsoft.AspNet.Providers.Core, not in use
* Removed NuGet package for Microsoft.AspNet.WebApi.HelpPage, not in use
* Updated NuGet package for EntityFramework to 6.1.3
* Updated NuGet package for jQuery.UI.Combined to 1.11.4	

Issues Fixed:
* Enabled configuring common logging severity levels for repository adapter and service adapter
* Changed Eleflex.WebServer, Azure Storage Providers and Eleflex.AzureWebService to use .NET Framework 4.5 instead of 4.5.1
* Updated web.configs for module web modules to point to MVC5 components and reduce runtime binding errors
* Security account screens now use h1 headers
* Added web.config entries for authentication mode="None" under system.web to disable auto sql server role provider
* Added web.config entries for add key="enableSimpleMembership" value="false" under appSettings to disable auto sql server role provider

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
