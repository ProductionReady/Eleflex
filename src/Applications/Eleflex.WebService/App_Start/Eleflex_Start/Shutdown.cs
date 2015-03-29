﻿#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2015 Production Ready, LLC. All Rights Reserved.
//Copyright © 2015 Production Ready, LLC. All Rights Reserved.
//For more information, visit http://www.ProductionReady.com
//This file is part of PRODUCTION READY® ELEFLEX®.
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU Affero General Public License as
//published by the Free Software Foundation, either version 3 of the
//License, or (at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Affero General Public License for more details.
//
//You should have received a copy of the GNU Affero General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
#endregion
using System;

[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(Eleflex.WebService.App_Start.Eleflex_Start.Shutdown), "Stop")]

namespace Eleflex.WebService.App_Start.Eleflex_Start
{
    /// <summary>
    /// Shutdown code needed by ELEFLEX.
    /// </summary>
    public static class Shutdown 
    {
        /// <summary>
        /// Stop.
        /// </summary>
        public static void Stop() 
        {
            //Log shutdown
            Common.Logging.LogManager.GetLogger(typeof(Shutdown)).Info("Application Shutdown");

            System.Threading.Thread.Sleep(1000);

            //Dispose the logger
            ((IDisposable)Common.Logging.LogManager.Adapter).Dispose();

            //Dispose container
            ((IDisposable)Bootstrap.Bootstrapper.Container).Dispose();     
        }
    }
}