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
using System.Web.Mvc;
using Eleflex.Storage;
using Eleflex.Security.Message;
using Eleflex.Security.Message.UserCommand;

namespace Eleflex.Security.Web.Security.Users
{
    /// <summary>
    /// Security Users.
    /// </summary>
    public class UsersController : Controller
    {
        /// <summary>
        /// user service client.
        /// </summary>
        IUserServiceClient _userServiceClient;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userServiceClient"></param>
        public UsersController(IUserServiceClient userServiceClient)
        {
            _userServiceClient = userServiceClient;
        }


        /// <summary>
        /// Index.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }


        /// <summary>
        /// List.
        /// </summary>
        /// <returns></returns>        
        public ActionResult List(ListModel model = null)
        {
            StorageQueryBuilder builder = new StorageQueryBuilder();
            if (model != null)
            {
                if (!string.IsNullOrWhiteSpace(model.Inactive))
                    builder.IsEqual("Inactive", model.Inactive);
                if (!string.IsNullOrWhiteSpace(model.Email))
                    builder.Contains("Email", model.Email);
                if (!string.IsNullOrWhiteSpace(model.Username))
                    builder.Contains("Username", model.Username);
            }

            if (model != null && model.MaxRecords.HasValue)
                builder.Paging(1, model.MaxRecords.Value);
            else
                builder.Paging(1, Constants.MAX_RETURNED_RECORDS_DEFAULT);

            builder.Sort("Username", true);
            var response = _userServiceClient.Query(builder.GetStorageQuery());
            model.Items = response.Items;
            return View(model);
        }

    }
}
