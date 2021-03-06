*************************
INSTALLATION INSTRUCTIONS
*************************

1) The package is the core assembly for the ELEFLEX platform. Install the ELEFLEX.WebServer package to host an ELEFLEX web site.


********
OVERVIEW
********

ELEFLEX is a platform for building modular, domain-driven, service-oriented web applications and services. This package contains the core assembly used for the ELEFLEX platform.

Visit our website at http://www.ProductionReady.com for the latest news, developer documentation, resources and more!	


********************
v3.0.0 RELEASE NOTES
********************

We are happy to release ELEFLEX v3.0! This is the next generation of the ELEFLEX platform and provides a wealth of new features such as:

* 3 NEW NuGet deployment projects available
	* Eleflex - This package provides the core library for the infrastructure.
	* Eleflex.WebClient - This package installs all the required components to host a web application that communicates with an existing ELEFLEX WebServer utilizing the service communication bus.
	* Eleflex.WebServer - This package installs all required business and service components utilizing Microsoft SQL Server or Azure as the database storage mechanism.
	* Portable and univeral apps coming soon!

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
