*************************
INSTALLATION INSTRUCTIONS
*************************

1) This module provides a web user interface for administration of lookups. By default, it is located at the URL "Admin/Lookups". Add this link to your Admin page, or type into your URL to access. You must have an ELEFLEX Lookups WebServer to connect to.


********
OVERVIEW
********

ELEFLEX Lookups is an ELEFLEX Module that provides a generic lookup table service for the system rather than creating multiple lookup tables. This is primarily used to categorize known data elements such as gender, race, types, etc. Access the ILookup*Repository objects for interaction.

Visit our website at http://www.ProductionReady.com for the latest news, documentation, resources and more!	


********************
v3.0.0 RELEASE NOTES
********************

* Integration into the ELEFLEX v3.0 platform


**************************
WHAT DOES THIS PACKAGE DO?
**************************

* This package installs the Eleflex.Lookups.WebClient assembly and adds 2 files into the App_Start/Eleflex_Start/EleflexLookups folder
	* WebClientRoutesStartupTask.cs - This class registers MVC page routes for "Admin/Lookups" admin web interface.
	* WebClientObjectLocationRegistrationTask.cs - This class registers structuremap object location configurations for service repository WCF communication.


Happy Coding!
Production Ready, LLC
http://www.ProductionReady.com
