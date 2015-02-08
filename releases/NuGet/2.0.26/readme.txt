-------------------
Welcome to ELEFLEX!
-------------------
ELEFLEX is a platform for building modular, domain-driven, service-oriented web applications and services.

---------------------
v2.0.26 Release Notes
---------------------
New features include:
* Now supporting Microsoft Azure! Remove Storage.SqlServer assemblies from your project and add Storage.Azure assemblies from GitHub for each module
* Created Applications/Eleflex.AzureWebService project in GitHub configured with Azure storage providers
* Created Applications/Eleflex.WebClient as completely service-based client of Eleflex.WebService project

Changes include:	
* Versioning changes to break modules into business and storage components
* Admin Versioning page now displays current business component version and storage component version for each module

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
