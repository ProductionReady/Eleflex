Welcome to ELEFLEX!
-------------------
ELEFLEX is a platform for building modular, domain-driven, service-oriented web applications and services.

New Installation Instructions
-----------------------------
It is recommended that you create a new ASP.NET MVC 4 Web Application (Empty Template) and then add the Eleflex NuGet package.
The following steps must be completed to finish setup of a new installation:
1) Add a reference to System.ServiceModel
2) Create a new SQL Server Database (v2005 or greater)
3) Update the web.config file to set the connection string for "EleflexDefault" to reflect the database you created
4) Update the web.config file to change the port number 16185 in system.servicemodel->client->endpoint address to be your web application's port number
5) Update the web.config file to fix MVC 5 upgrade issues by changing the following appSettings key:
	<add key="webpages:Version" value="2.0.0.0" /> 	to  <add key="webpages:Version" value="3.0.0.0" />
	
After completing the above steps, run the application. The included application modules will self-update to create database tables and data as needed.

Existing Installations
----------------------
No changes are required.

v2.0.4 Release Notes
--------------------
This release contains the following changes:
* Language changes in home page
* Add links to download complete source code from GitHub
* Footer AGPL image and link to download
* Added AGPL license headers to security solution
* Added default admin intro page and search pages for roles and permissions in security solution
* Added new NuGet component, Bootstrap.Chosen, that makes chosen select boxes look more bootstrap v3 compatible
* Upgraded NuGet components, Bootstrap, JQueryUI and JQueryValidation

We will be updating the application frequently, check NuGet updates for new information.

Happy Coding!
Production Ready, LLC
http://www.ProductionReady.com
