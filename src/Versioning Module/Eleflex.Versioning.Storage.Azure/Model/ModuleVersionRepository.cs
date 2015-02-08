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
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Eleflex.Versioning;
using Eleflex.Storage;
using Eleflex.Storage.EntityFramework;
using DomainModel = Eleflex.Versioning;
using StorageModel = Eleflex.Versioning.Storage.Azure;

namespace Eleflex.Versioning.Storage.Azure
{
    /// <summary>
    /// Generic Sql Repository for a Version. We don't use entity framework for this module because we need to be able to manipulate table
    /// in older version formats that may not be compatible.
    /// </summary>
    public class ModuleVersionRepository : SqlStorageRepository<DomainModel.ModuleVersion, IVersioningStorageProvider, Guid>, IModuleVersionRepository
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="storageProvider"></param>
        public ModuleVersionRepository(IVersioningStorageProvider storageProvider) : base(storageProvider) { }


        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public override DomainModel.ModuleVersion Insert(DomainModel.ModuleVersion domain)
        {
            SqlCommand command = null;
            try
            {
                IStorageSession session = _storageProvider.GetSession();
                command = new SqlCommand();
                command.Connection = session.Session as SqlConnection;
                command.Transaction = session.Transaction as SqlTransaction;
                command.CommandType = CommandType.Text;
                command.CommandText = "insert into Eleflex.ModuleVersion(ModuleKey, ModuleName, UpdateDate, Major, Minor, Build, Revision, ExtraData ) values (@ModuleKey, @ModuleName, @UpdateDate, @Major, @Minor, @Build, @Revision, @ExtraData)";                
                command.Parameters.AddWithValue("@ModuleKey",domain.ModuleKey);
                command.Parameters.AddWithValue("@ModuleName", domain.ModuleName);
                command.Parameters.AddWithValue("@UpdateDate", domain.UpdateDate);
                command.Parameters.AddWithValue("@Major", domain.Version.Major);
                command.Parameters.AddWithValue("@Minor", domain.Version.Minor);
                command.Parameters.AddWithValue("@Build", domain.Version.Build);
                command.Parameters.AddWithValue("@Revision", domain.Version.Revision);
                command.Parameters.AddWithValue("@ExtraData", domain.ExtraData == null ? (object)DBNull.Value : domain.ExtraData);
                int rowCount = command.ExecuteNonQuery();
                return domain;
            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<ModuleVersionRepository>().Error(ex);
                return null;
            }
            finally
            {
                if (command != null)
                    command.Dispose();
            }
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override DomainModel.ModuleVersion Get(Guid key)
        {
            SqlCommand command = null;
            SqlDataReader reader = null;
            try
            {
                IStorageSession session = _storageProvider.GetSession();
                command = new SqlCommand();
                command.Connection = session.Session as SqlConnection;
                command.Transaction = session.Transaction as SqlTransaction;
                command.CommandType = CommandType.Text;
                command.CommandText = "select ModuleKey, ModuleName,UpdateDate,Major,Minor,Build,Revision,ExtraData from Eleflex.ModuleVersion where ModuleKey= @ModuleKey";
                command.Parameters.Add(new SqlParameter("@ModuleKey", key));
                reader = command.ExecuteReader();
                DomainModel.ModuleVersion item = null;
                if (reader.Read())
                {
                    item = new DomainModel.ModuleVersion();
                    item.ChangeModuleKey(reader.GetGuid(0));
                    item.ChangeModuleName(Convert.ToString(reader.GetValue(1)));
                    item.ChangeUpdateDate(reader.GetDateTimeOffset(2));
                    DomainModel.Version version = new DomainModel.Version();                    
                    version.ChangeMajor(Convert.ToInt32(reader.GetValue(3)));
                    version.ChangeMinor(Convert.ToInt32(reader.GetValue(4)));
                    version.ChangeBuild(Convert.ToInt32(reader.GetValue(5)));
                    version.ChangeRevision(Convert.ToInt32(reader.GetValue(6)));
                    item.ChangeVersion(version);
                    item.ChangeExtraData(Convert.ToString(reader.GetValue(7)));
                }
                return item;
            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<ModuleVersionRepository>().Error(ex);
                return null;
            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
                if (command != null)
                    command.Dispose();
            }
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public override DomainModel.ModuleVersion Update(DomainModel.ModuleVersion domain)
        {
            SqlCommand command = null;
            try
            {
                IStorageSession session = _storageProvider.GetSession();
                command = new SqlCommand();
                command.Connection = session.Session as SqlConnection;
                command.Transaction = session.Transaction as SqlTransaction;
                command.CommandType = CommandType.Text;
                command.CommandText = "update Eleflex.ModuleVersion set ModuleName=@ModuleName,UpdateDate=@UpdateDate,Major=@Major,Minor=@Minor,Build=@Build,Revision=@Revision,ExtraData=@ExtraData where ModuleKey=@ModuleKey";
                command.Parameters.Add(new SqlParameter("@ModuleKey", domain.ModuleKey));
                command.Parameters.Add(new SqlParameter("@ModuleName", domain.ModuleName));
                command.Parameters.Add(new SqlParameter("@UpdateDate", domain.UpdateDate));                
                command.Parameters.Add(new SqlParameter("@Major", domain.Version.Major));
                command.Parameters.Add(new SqlParameter("@Minor", domain.Version.Minor));
                command.Parameters.Add(new SqlParameter("@Build", domain.Version.Build));
                command.Parameters.Add(new SqlParameter("@Revision", domain.Version.Revision));
                command.Parameters.Add(new SqlParameter("@ExtraData", domain.ExtraData == null ? (object)DBNull.Value : domain.ExtraData));
                int updated = command.ExecuteNonQuery();
                return domain;
            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<ModuleVersionRepository>().Error(ex);
                return null;
            }
            finally
            {
                if (command != null)
                    command.Dispose();
            }
        }

        /// <summary>
        /// Delete.
        /// </summary>
        /// <param name="key"></param>
        public override void Delete(Guid key)
        {
            SqlCommand command = null;
            try
            {
                IStorageSession session = _storageProvider.GetSession();
                command = new SqlCommand();
                command.Connection = session.Session as SqlConnection;
                command.Transaction = session.Transaction as SqlTransaction;
                command.CommandType = CommandType.Text;
                command.CommandText = "delete from Eleflex.ModuleVersion where ModuleKey=@ModuleKey";
                command.Parameters.Add(new SqlParameter("@ModuleKey", key));
                int updated = command.ExecuteNonQuery();                
            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<ModuleVersionRepository>().Error(ex);
            }
            finally
            {
                if (command != null)
                    command.Dispose();
            }
        }

        /// <summary>
        /// Query.
        /// </summary>
        /// <param name="storageQuery"></param>
        /// <returns></returns>
        public override IList<DomainModel.ModuleVersion> Query(IStorageQuery storageQuery)
        {
            return GetAll();
        }

        /// <summary>
        /// Query entities for an aggregate.
        /// </summary>
        /// <param name="storageQuery"></param>
        /// <returns></returns>
        public override double QueryAggregate(IStorageQuery storageQuery)
        {
            return GetAll().Count;
        }

        /// <summary>
        /// Determine if repository is installed or not.
        /// </summary>
        public virtual bool IsInstalled()
        {
            SqlCommand command = null;
            SqlDataReader reader = null;
            try
            {
                IStorageSession session = _storageProvider.GetSession();
                command = new SqlCommand();
                command.Connection = session.Session as SqlConnection;
                command.Transaction = session.Transaction as SqlTransaction;
                command.CommandType = CommandType.Text;
                command.CommandText = SCRIPT_TABLE_EXISTS;
                reader = command.ExecuteReader();
                if (reader.Read())
                    return Convert.ToBoolean(reader.GetValue(0));
                return false;
            }
            catch
            {                
                return false;
            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
                if (command != null)
                    command.Dispose();
            }
        }

        protected const string SCRIPT_TABLE_EXISTS = @"
IF OBJECT_ID(N'Eleflex.ModuleVersion', N'U') IS NOT NULL
BEGIN
  select 1
END
ELSE
BEGIN
select 0
END
";

        /// <summary>
        /// Get all.
        /// </summary>
        /// <returns></returns>
        public virtual List<DomainModel.ModuleVersion> GetAll()
        {
            SqlCommand command = null;
            SqlDataReader reader = null;
            try
            {
                IStorageSession session = _storageProvider.GetSession();
                command = new SqlCommand();
                command.Connection = session.Session as SqlConnection;
                command.Transaction = session.Transaction as SqlTransaction;
                command.CommandType = CommandType.Text;
                command.CommandText = "select ModuleKey, ModuleName,UpdateDate,Major,Minor,Build,Revision,ExtraData from Eleflex.ModuleVersion";
                reader = command.ExecuteReader();
                DomainModel.ModuleVersion item = null;
                List<DomainModel.ModuleVersion> list = new List<DomainModel.ModuleVersion>();
                while (reader.Read())
                {
                    item = new DomainModel.ModuleVersion();
                    list.Add(item);                    
                    item.ChangeModuleKey(reader.GetGuid(0));
                    item.ChangeModuleName(Convert.ToString(reader.GetValue(1)));
                    item.ChangeUpdateDate(reader.GetDateTimeOffset(2));
                    DomainModel.Version version = new DomainModel.Version();
                    version.ChangeMajor(Convert.ToInt32(reader.GetValue(3)));
                    version.ChangeMinor(Convert.ToInt32(reader.GetValue(4)));
                    version.ChangeBuild(Convert.ToInt32(reader.GetValue(5)));
                    version.ChangeRevision(Convert.ToInt32(reader.GetValue(6)));
                    item.ChangeVersion(version);
                    item.ChangeExtraData(Convert.ToString(reader.GetValue(7)));
                }
                return list;
            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<ModuleVersionRepository>().Error(ex);
                return null;
            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
                if (command != null)
                    command.Dispose();
            }
        }

    }
}
