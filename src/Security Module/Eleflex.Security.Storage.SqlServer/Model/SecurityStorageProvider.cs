#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2015 Production Ready, LLC. All Rights Reserved.
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
using System.Collections.Generic;
using System.Data.Entity;
using Eleflex.Storage;
using Eleflex.Storage.EntityFramework;

namespace Eleflex.Security.Storage.SqlServer
{
    /// <summary>
    /// Provides abstraction to logging storage mechanism.
    /// </summary>
    public class SecurityStorageProvider : EntityStorageProvider, ISecurityStorageProvider
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connectionStringKey"></param>
        /// <param name="storageProviderUnitOfWork"></param>
        public SecurityStorageProvider(string connectionStringKey, IStorageProviderUnitOfWork storageProviderUnitOfWork)
            : base(Eleflex.Security.SecurityConstants.STORAGE_PROVIDER_NAME, connectionStringKey, storageProviderUnitOfWork)
        {
        }

        /// <summary>
        /// Get the EF module namespace.
        /// </summary>
        public override string GetEFModelNamespace
        {
            get { return "Model.SecurityDB"; }

        }

        /// <summary>
        /// Create a session, base class will take care of management.
        /// </summary>
        /// <returns></returns>
        public override IStorageSession CreateNonManagedSession()
        {
            SecurityDB context = new SecurityDB(ProviderConnectionString);
            DbContextTransaction transaction = context.Database.BeginTransaction();
            EntityStorageSession session = new EntityStorageSession(context, transaction);
            return session;
        }

    }
}
