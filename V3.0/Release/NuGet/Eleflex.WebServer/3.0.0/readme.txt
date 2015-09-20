*************************
INSTALLATION INSTRUCTIONS
*************************

1) Create a new SQL Server Database. ELEFLEX Modules will automatically create schemas/tables/procedures/data as needed with this.

1b) If you are creating a Microsoft Azure Database, change the App_Start/Eleflex_Start/SystemObjectLocationRegistrationTask.cs file. For each of the ISTORAGESERVICE_CONSTUCTORPARAM_VERSIONINGSTORAGECONFIG configurations, replace "SQLServer" with "Azure". Ex: "Eleflex.Logging.Storage.EF.SQLServer.LoggingSQLServerConstants.VERSIONING_STORAGE_CONFIG" changes to "Eleflex.Logging.Storage.EF.Azure.LoggingAzureConstants.VERSIONING_STORAGE_CONFIG"

2) Update the web.config file to change the connection string for "EleflexDefault" with the database you just created in step 1.
---snip---
<connectionStrings>
  <add name="EleflexDefault" connectionString="server=localhost;database=Eleflex;user id=test;password=test;" />
</connectionStrings>
---snip---

3) Update the web.config file to change the WCF client port number to access your hosted ELEFLEX WebServer.
Change the port number "16185" to be your web application's port number, found by going into your web project's properties and clicking "Web" from left menu. Alternatively, you could change your web application's port number to 16185 to match the config.
---snip---
<system.serviceModel>
  <client>
    <endpoint address="http://localhost:16185/EleflexService.svc" name="EleflexDefault" behaviorConfiguration="EleflexBehavior" binding="basicHttpBinding" bindingConfiguration="EleflexBinding" contract="Eleflex.Services.WCF.IWCFCommand" />
  <client>
</system.serviceModel>
---snip---

After completing the above steps, rebuild and run your web application. The first user account to register with the system becomes a system administrator.

********
OVERVIEW
********

ELEFLEX is a platform for building modular, domain-driven, service-oriented applications and services. This package contains business logic, WCF service commands and database access for the Logging, Security and Versioning core modules using Microsoft SQL Server and Azure database platforms.

Visit our website at http://www.ProductionReady.com for the latest news, documentation, resources and more!	

********************
v3.0.0 RELEASE NOTES
********************

We are happy to release ELEFLEX v3.0! This is the next generation of the ELEFLEX platform and provides a wealth of new features such as:

* T4 code generation templates creates nearly all code files needed for the infratructure based on a created entity data model. This allows for faster module development and deployment.

* Integrated business rules and business events
	* Business rules are dynamically loaded with class attributes to allow integration from any module or referenced assembly.
	* Fire business events so event consumers can provide real-time integrated logic processing or failure conditions.

* Integrated repository design:
	* Business repositories provide business rule processing and business events.
	* Storage repositories provide data persistence abstraction. 
	* Service repositories provide service-based communication of objects.
	* Mapping repositories provide object mapping between repositories.



Happy Coding!
Production Ready, LLC
http://www.ProductionReady.com
