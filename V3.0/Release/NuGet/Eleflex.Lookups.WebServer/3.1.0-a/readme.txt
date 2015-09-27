*************************
INSTALLATION INSTRUCTIONS
*************************

1) This module provides a database access, service commands and a web user interface for administration of lookups. By default, it is located at the URL "Admin/Lookups". Add this link to your Admin page, or type into your URL to access.

NOTE: This module utilizes the "EleflexDefault" database and WCF service client configurations by default.

********
OVERVIEW
********

ELEFLEX Lookups is an ELEFLEX Module that provides a generic lookup table service for the system rather than creating multiple lookup tables. This is primarily used to categorize known data elements such as gender, race, types, etc. Access the ILookup*Repository objects for interaction.

Visit our website at http://www.ProductionReady.com for the latest news, developer documentation, resources and more!	


********************
v3.0.0 RELEASE NOTES
********************

* Fixes for reloading assemblies on web application recycling


**************************
WHAT DOES THIS PACKAGE DO?
**************************

* This package installs the Eleflex.Lookups.WebServer assembly and adds 1 file into the App_Start/Eleflex_Start/EleflexLookups folder
	* WebServerObjectLocationRegistrationTask.cs - This class registers structuremap object location configurations for the SQL Server storage service by default, change to Azure if needed.


Happy Coding!
Production Ready, LLC
http://www.ProductionReady.com
