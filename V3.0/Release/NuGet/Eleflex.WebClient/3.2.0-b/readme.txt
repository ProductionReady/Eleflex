*************************
INSTALLATION INSTRUCTIONS
*************************

1) Update your web.config file to change the WCF client address and port number ("localhost:16185") to point to your ELEFLEX WebServer.
---snip---
<system.serviceModel>
  <client>
    <endpoint address="http://localhost:16185/EleflexService.svc" name="EleflexDefault" behaviorConfiguration="EleflexBehavior" binding="basicHttpBinding" bindingConfiguration="EleflexBinding" contract="Eleflex.Services.WCF.IWCFCommand" />
  <client>
</system.serviceModel>
---snip---

2) Ensure the Web.Config AppSetting for "EleflexImpersonateSystemToken" is the same between your ELEFLEX WebServer and WebClient. Change this value to something unique before deploying your application.

After completing the above steps, rebuild and run your web application project. You should now be connected to your ELEFLEX WebServer.


********
OVERVIEW
********

ELEFLEX is a platform for building modular, domain-driven, service-oriented web applications and services. This package contains web application user interfaces for Logging, Security and Versioning as well as standardized ELEFLEX service communication.

Visit our website at http://www.ProductionReady.com for the latest news, documentation, resources and more!	


********************
v3.1.0 RELEASE NOTES
********************

* Fixes for reloading assemblies on web application recycling


Happy Coding!
Production Ready, LLC
http://www.ProductionReady.com
