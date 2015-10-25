*************************
INSTALLATION INSTRUCTIONS
*************************

1) This module provides a web user interface for administration of the Email Module. By default, it is located at the URL "Admin/Email". Add this link to your Admin page, or type into your URL to access. You must have an ELEFLEX Email WebServer to connect to.


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

* This package installs the Eleflex.Email.WebClient assembly and adds 2 files into the App_Start/Eleflex_Start/EleflexEmail folder
	* WebClientRoutesStartupTask.cs - This class registers MVC page routes for "Admin/Email" admin web interface.
	* WebClientObjectLocationRegistrationTask.cs - This class registers structuremap object location configurations for service repository WCF communication.


Happy Coding!
Production Ready, LLC
http://www.ProductionReady.com
