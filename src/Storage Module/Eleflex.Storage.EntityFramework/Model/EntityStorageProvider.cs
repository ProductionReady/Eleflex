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
using System.Text;
using System.Configuration;
using System.Data.Entity;
using Eleflex;
using Eleflex.Storage;
using Eleflex.Storage.EntityFramework;

namespace Eleflex.Storage.EntityFramework
{
    /// <summary>
    /// Provides storage provider base class for entity framework and utility functions.
    /// </summary>
    public abstract class EntityStorageProvider : StorageProvider
    {
        protected readonly IStorageProviderUnitOfWork _storageProviderUnitOfWork;
        protected string _providerConnectionString = null;

        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="providerKey"></param>
        /// <param name="connectionStringKey"></param>
        /// <param name="storageProviderUnitOfWork"></param>
        public EntityStorageProvider(string providerKey, string connectionStringKey, IStorageProviderUnitOfWork storageProviderUnitOfWork)       
            : base(providerKey, connectionStringKey)
        {
            ConnectionStringKey = connectionStringKey;
            _storageProviderUnitOfWork = storageProviderUnitOfWork;
            IStorageProvider provider = _storageProviderUnitOfWork.GetStorageProvider(providerKey);
            if (provider == null)
            {
                _storageProviderUnitOfWork.StartStorageProvider(this);
                SessionOwner = true;
            }
            else
            {
                this.Sessions = provider.Sessions; //Get the session list from UOW master provider.
                SessionOwner = false;
            }
        }

        /// <summary>
        /// Get the entity framework model namespace. Ex: Model.LoggingDB (namespace name used in generation for model)
        /// </summary>
        /// <returns></returns>
        public abstract string GetEFModelNamespace { get; }

        /// <summary>
        /// Get the providers connection string.
        /// </summary>
        public override string ProviderConnectionString
        {
            get
            {
                if (!string.IsNullOrEmpty(_providerConnectionString))
                    return _providerConnectionString;

                string genericConnectionString = ConnectionString;
                string efModelNamespace = GetEFModelNamespace;

                string[] split = genericConnectionString.Split(';');
                string server = string.Empty;
                string database = string.Empty;
                string username = string.Empty;
                string password = string.Empty;
                StringBuilder additionalParameters = new StringBuilder();
                foreach (string config in split)
                {
                    string[] paramSplit = config.Split('=');
                    if (paramSplit.Length <= 1)
                        continue;
                    if (string.Compare(paramSplit[0], "data source", true) == 0)
                    {
                        server = paramSplit[1];
                        continue;
                    }
                    if (string.Compare(paramSplit[0], "server", true) == 0)
                    {
                        server = paramSplit[1];
                        continue;
                    }
                    if (string.Compare(paramSplit[0], "initial catalog", true) == 0)
                    {
                        database = paramSplit[1];
                        continue;
                    }
                    if (string.Compare(paramSplit[0], "catalog", true) == 0)
                    {
                        database = paramSplit[1];
                        continue;
                    }
                    if (string.Compare(paramSplit[0], "database", true) == 0)
                    {
                        database = paramSplit[1];
                        continue;
                    }
                    if (string.Compare(paramSplit[0], "user id", true) == 0)
                    {
                        username = paramSplit[1];
                        continue;
                    }
                    if (string.Compare(paramSplit[0], "uid", true) == 0)
                    {
                        username = paramSplit[1];
                        continue;
                    }
                    if (string.Compare(paramSplit[0], "password", true) == 0)
                    {
                        password = paramSplit[1];
                        continue;
                    }
                    if (string.Compare(paramSplit[0], "pass", true) == 0)
                    {
                        password = paramSplit[1];
                        continue;
                    }
                    if (additionalParameters.Length > 0)
                        additionalParameters.Append(";");
                    additionalParameters.Append(paramSplit[0]);
                    additionalParameters.Append("=");
                    additionalParameters.Append(paramSplit[1]);
                }
                
                StringBuilder sb = new StringBuilder();
                sb.Append(@"metadata=res://*/");
                sb.Append(efModelNamespace);
                sb.Append(@".csdl|res://*/" );
                sb.Append(efModelNamespace);
                sb.Append(@".ssdl|res://*/" );
                sb.Append(efModelNamespace);
                sb.Append(@".msl;provider=System.Data.SqlClient;provider connection string='data source=");
                sb.Append(server);
                sb.Append(";initial catalog=");
                sb.Append(database);
                sb.Append(";");
                if (!string.IsNullOrEmpty(username))
                {
                    sb.Append("user id=");
                    sb.Append(username);
                    sb.Append(";password=");
                    sb.Append(password);
                    sb.Append(";");
                }
                sb.Append("MultipleActiveResultSets=True;");
                if (additionalParameters.Length > 0)
                {
                    sb.Append(additionalParameters.ToString());
                    sb.Append(";");
                }
                sb.Append("application name=EntityFramework'");
                _providerConnectionString = sb.ToString();
                return _providerConnectionString;
            }
        }

        /// <summary>
        /// Get a generic connection string from the provider connection string.
        /// </summary>
        public virtual string GetGenericConnectionStringFromEntityConnectionString(string entityConnectionString)
        {
            string entityCon = entityConnectionString;
            string[] split = entityCon.Split(';');
            string server = string.Empty;
            string database = string.Empty;
            string username = string.Empty;
            string password = string.Empty;
            foreach (string config in split)
            {
                string[] paramSplit = config.Split('=');
                if(paramSplit.Length<=1)
                    continue;
                if (string.Compare(paramSplit[0], "provider connection string", true) == 0)
                {
                    if (paramSplit.Length >= 3)
                        server = paramSplit[2];
                    continue;
                }
                if (string.Compare(paramSplit[0], "initial catalog", true) == 0)
                {
                    database = paramSplit[1];
                    continue;
                }
                if (string.Compare(paramSplit[0], "user id", true) == 0)
                {
                    username = paramSplit[1];
                    continue;
                }
                if (string.Compare(paramSplit[0], "password", true) == 0)
                {
                    password = paramSplit[1];
                    continue;
                }                    
            }
            return "server=" + server + ";database=" + database + ";uid=" + username + ";password=" + password + ";";
        }


    }
}
