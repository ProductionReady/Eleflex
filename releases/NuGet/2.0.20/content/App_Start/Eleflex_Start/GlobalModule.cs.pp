#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2014 Production Ready, LLC. All Rights Reserved.
//Copyright © 2014 Production Ready, LLC. All Rights Reserved.
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
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace $rootnamespace$.App_Start.Eleflex_Start
{
    /// <summary>
    /// HTTP Module for all requests.
    /// </summary>
    public class GlobalModule : IHttpModule
    {
        /// <summary>
        /// Init, setup events.
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            context.EndRequest += context_EndRequest;
            context.Error += context_Error;
        }

        /// <summary>
        /// Error.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void context_Error(object sender, EventArgs e)
        {
            if (sender is System.Web.HttpApplication)
            {
                HttpApplication application = (HttpApplication)sender;
                Exception exception = application.Server.GetLastError();
                Common.Logging.LogManager.GetLogger<GlobalModule>().Error("Web Error", exception);                
            }
            HttpContext.Current.Response.Redirect(@"/Error");
        }

        /// <summary>
        /// End request.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void context_EndRequest(object sender, EventArgs e)
        {
            //Dispose any unit of work (if needed)            
            Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<Eleflex.Storage.IStorageProviderUnitOfWork>().Dispose();

            //Cleanup structuremap
            StructureMap.Web.Pipeline.HttpContextLifecycle.DisposeAndClearAll();            
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose() { }
    }
}