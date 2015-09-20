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
