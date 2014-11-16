Welcome to ELEFLEX!
-------------------
ELEFLEX is a platform for building modular, domain-driven, service-oriented web applications and services.

Notes
-----
It is recommended that you create a new ASP.NET MVC 4 Web Application (Empty Template) and then add the Eleflex NuGet package for fastest setup.

Post-Installation
-----------------
The following steps must be done to setup your environment for first use:
1) Add a reference to System.ServiceModel
2) Create an new SQL Server Database (v2005 or greater)
3) Update the web.config file to set the connection string for "EleflexDefault" to reflect the database you created
4) Update the web.config file to change the port number 16185 in system.servicemodel->client->endpoint address to be your web application's port number
5) Update the web.config file to fix MVC 5 upgrade issues by doing the following: ( https://www.asp.net/mvc/overview/releases/how-to-upgrade-an-aspnet-mvc-4-and-web-api-project-to-aspnet-mvc-5-and-web-api-2 )
Change:	<add key="webpages:Version" value="2.0.0.0" /> 
	to
	<add key="webpages:Version" value="3.0.0.0" />

After completing all the above steps, run the application. The included application modules will self-update to create database tables and data as needed.

We are in the process of setting up hosting for source code, defect management and releasing new functionality. All Coming Soon!

Happy Coding!
Production Ready, LLC