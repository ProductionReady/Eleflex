﻿using System.Text;

namespace Eleflex.Storage.EF
{
    /// <summary>
    /// Provides storage service base class for entity framework and utility functions.
    /// </summary>
    public abstract class EntityStorageService : StorageServiceDatabase
    {

        /// <summary>
        /// The unit of work.
        /// </summary>
        protected readonly IStorageContextUnitOfWork _storageContextUnitOfWork;
        /// <summary>
        /// The provider connection string.
        /// </summary>
        protected string _providerConnectionString = null;

        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="serviceKey"></param>
        /// <param name="connectionStringKey"></param>
        /// <param name="versioningStorageConfig"></param>
        /// <param name="storageContextUnitOfWork"></param>
        public EntityStorageService(string serviceKey, string connectionStringKey, string versioningStorageConfig, IStorageContextUnitOfWork storageContextUnitOfWork)
            : base()
        {
            ConnectionStringKey = connectionStringKey;
            SimpleConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringKey].ConnectionString;
            _storageContextUnitOfWork = storageContextUnitOfWork;
            IStorageService service = _storageContextUnitOfWork.GetStorageService(serviceKey);
            if (service == null)
            {
                _storageContextUnitOfWork.RegisterStorageService(this);
                SessionOwner = true;
            }
            else
            {
                this.Sessions = service.Sessions; //Get the session list from UOW master provider.
                SessionOwner = false;
            }
            VersioningStorageConfig = versioningStorageConfig;
        }

        public virtual string ConnectionStringKey
        {
            get; set;
        }

        /// <summary>
        /// Get the entity framework database context namespace. Ex: LoggingDB
        /// </summary>
        /// <returns></returns>
        public abstract string GetEFDatabaseName { get; }

        /// <summary>
        /// Get the providers connection string.
        /// </summary>
        public override string ProviderConnectionString
        {
            get
            {
                if (!string.IsNullOrEmpty(_providerConnectionString))
                    return _providerConnectionString;

                string genericConnectionString = SimpleConnectionString;
                string efModelNamespace = GetEFDatabaseName;

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
                sb.Append(@".csdl|res://*/");
                sb.Append(efModelNamespace);
                sb.Append(@".ssdl|res://*/");
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
        /// Get a generic connection string from the service connection string.
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
                if (paramSplit.Length <= 1)
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
