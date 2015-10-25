*************************
INSTALLATION INSTRUCTIONS
*************************

1) This module provides database access, service commands and a web user interface for administration of the Email Module. By default, it is located at the URL "Admin/Email". Add this link to your Admin page, or type into your URL to access.

NOTE: This module utilizes the "EleflexDefault" database and WCF service client configurations by default.

********
OVERVIEW
********

ELEFLEX Email is an ELEFLEX Module that provides distributed storage and sending emails.

Visit our website at https://www.ProductionReady.com for the latest news, documentation, resources and more!	


********************
v3.2.0 RELEASE NOTES
********************

* Initial release


**************************
WHAT DOES THIS PACKAGE DO?
**************************

* This package installs the Eleflex.Email.WebServer assembly and adds 2 files into the App_Start/Eleflex_Start/EleflexEmail folder
	* WebServerObjectLocationRegistrationTask.cs - This class registers structuremap object location configurations for the SQL Server storage service by default, change to Azure if needed.
	* WebServerProcessStartupTask.cs - This class registers background processes (sending emails, purging old emails, etc) needed for the Email Module.


Happy Coding!
Production Ready, LLC
http://www.ProductionReady.com
