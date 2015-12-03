*************************
INSTALLATION INSTRUCTIONS
*************************

1) Create a new SQL Server Database. ELEFLEX Modules will automatically create schemas/tables/procedures/data as needed with this.

1b) If you are creating a Microsoft Azure Database, change the App_Start/Eleflex_Start/SystemObjectLocationRegistrationTask.cs file. For all of the ISTORAGESERVICE_CONSTUCTORPARAM_VERSIONINGSTORAGECONFIG configurations, replace "SQLServer" with "Azure". Ex: "Eleflex.Logging.Storage.EF.SQLServer.LoggingSQLServerConstants.VERSIONING_STORAGE_CONFIG" changes to "Eleflex.Logging.Storage.EF.Azure.LoggingAzureConstants.VERSIONING_STORAGE_CONFIG"

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

4) Update the web.config file to change the AppSetting for "EleflexImpersonateSystemToken". Change this value to something unique before deploying your application.

After completing the above steps, rebuild and run your web application. The first user account to register with the system becomes a system administrator.

********
OVERVIEW
********

ELEFLEX is a platform for building modular, domain-driven, service-oriented applications and services. This package contains the ELEFLEX WebServer web application host that processes business logic, WCF service commands and database access for the Logging, Security and Versioning core modules using Microsoft SQL Server or Azure database platforms.

Visit our website at http://www.ProductionReady.com for the latest news, documentation, resources and more!	



Happy Coding!
Production Ready, LLC
http://www.ProductionReady.com
