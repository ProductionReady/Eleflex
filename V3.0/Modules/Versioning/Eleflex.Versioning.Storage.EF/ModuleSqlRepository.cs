//#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2015 Production Ready, LLC. All Rights Reserved.
////Copyright © 2015 Production Ready, LLC. All Rights Reserved.
////For more information, visit http://www.ProductionReady.com
////This file is part of PRODUCTION READY® ELEFLEX®.
////
////This program is free software: you can redistribute it and/or modify
////it under the terms of the GNU Affero General Public License as
////published by the Free Software Foundation, either version 3 of the
////License, or (at your option) any later version.
////
////This program is distributed in the hope that it will be useful,
////but WITHOUT ANY WARRANTY; without even the implied warranty of
////MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
////GNU Affero General Public License for more details.
////
////You should have received a copy of the GNU Affero General Public License
////along with this program.  If not, see <http://www.gnu.org/licenses/>.
//#endregion
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Data;
//using System.Data.SqlClient;
//using Eleflex.Versioning;
//using Eleflex.Storage;
//using Eleflex.Storage.EF;
//using DomainModel = Eleflex;
//using StorageModel = Eleflex.Versioning.Storage.EF;

//namespace Eleflex.Versioning.Storage.EF
//{
//    /// <summary>
//    /// Generic Sql Repository for a Version. We don't use entity framework for this module because we need to be able to manipulate table
//    /// in older version formats that may not be compatible.
//    /// </summary>
//    public class ModuleSqlRepository : SqlStorageRepository<IVersioningStorageService, DomainModel.Module, Guid>, IModuleStorageRepository
//    {
//        /// <summary>
//        /// Constructor.
//        /// </summary>
//        /// <param name="storageService"></param>
//        public ModuleSqlRepository(IVersioningStorageService storageService) : base(storageService) { }


//        /// <summary>
//        /// Insert
//        /// </summary>
//        /// <param name="domain"></param>
//        /// <returns></returns>
//        public override IResponseItem<DomainModel.Module> Insert(IRequestItem<DomainModel.Module> request)
//        {
//            SqlCommand command = null;
//            try
//            {
//                IStorageServiceSession session = _storageService.GetSession();
//                command = new SqlCommand();
//                command.Connection = session.Session as SqlConnection;
//                command.Transaction = session.Transaction as SqlTransaction;
//                command.CommandType = CommandType.Text;
//                command.CommandText = "insert into Eleflex.Module(ModuleKey, Name, UpdateDate, Major, Minor, Build, Revision, ExtraData ) values (@ModuleKey, @ModuleName, @UpdateDate, @Major, @Minor, @Build, @Revision, @ExtraData)";                
//                command.Parameters.AddWithValue("@ModuleKey", request.Item.ModuleKey);
//                command.Parameters.AddWithValue("@ModuleName", request.Item.Name);
//                command.Parameters.AddWithValue("@UpdateDate", request.Item.UpdateDate);
//                command.Parameters.AddWithValue("@Major", request.Item.Version.Major);
//                command.Parameters.AddWithValue("@Minor", request.Item.Version.Minor);
//                command.Parameters.AddWithValue("@Build", request.Item.Version.Build);
//                command.Parameters.AddWithValue("@Revision", request.Item.Version.Revision);
//                command.Parameters.AddWithValue("@ExtraData", request.Item.ExtraData == null ? (object)DBNull.Value : request.Item.ExtraData);
//                int rowCount = command.ExecuteNonQuery();
//                return new ResponseItem<DomainModel.Module>() { Item = request.Item };
//            }
//            catch (Exception ex)
//            {
//                Logger.Current.Error<ModuleSqlRepository>(ex);
//                ResponseItem<DomainModel.Module> resp = new ResponseItem<Eleflex.Module>();
//                resp.AddMessage(true, MessageConstants.ERROR_STORAGE_CODE, MessageConstants.ERROR_STORAGE_TEXT);
//                return resp;
//            }
//            finally
//            {
//                if (command != null)
//                    command.Dispose();
//            }
//        }

//        /// <summary>
//        /// Get.
//        /// </summary>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public override IResponseItem<DomainModel.Module> Get(IRequestItem<Guid> request)
//        {
//            SqlCommand command = null;
//            SqlDataReader reader = null;
//            try
//            {
//                IStorageServiceSession session = _storageService.GetSession();
//                command = new SqlCommand();
//                command.Connection = session.Session as SqlConnection;
//                command.Transaction = session.Transaction as SqlTransaction;
//                command.CommandType = CommandType.Text;
//                command.CommandText = "select ModuleKey, ModuleName,UpdateDate,Major,Minor,Build,Revision,ExtraData from Eleflex.Module where ModuleKey= @ModuleKey";
//                command.Parameters.Add(new SqlParameter("@ModuleKey", request.Item));
//                reader = command.ExecuteReader();
//                DomainModel.Module item = null;
//                if (reader.Read())
//                {
//                    item = new DomainModel.Module();
//                    item.ChangeModuleKey(reader.GetGuid(0));
//                    item.ChangeModuleName(Convert.ToString(reader.GetValue(1)));
//                    item.ChangeUpdateDate(reader.GetDateTimeOffset(2));
//                    DomainModel.Version version = new DomainModel.Version();                    
//                    version.ChangeMajor(Convert.ToInt32(reader.GetValue(3)));
//                    version.ChangeMinor(Convert.ToInt32(reader.GetValue(4)));
//                    version.ChangeBuild(Convert.ToInt32(reader.GetValue(5)));
//                    version.ChangeRevision(Convert.ToInt32(reader.GetValue(6)));
//                    item.ChangeVersion(version);
//                    item.ChangeExtraData(Convert.ToString(reader.GetValue(7)));
//                }
//                return new ResponseItem<DomainModel.Module>(){ Item = item};
//            }
//            catch (Exception ex)
//            {
//                Logger.Current.Error<ModuleSqlRepository>(ex);
//                ResponseItem<DomainModel.Module> resp = new ResponseItem<Eleflex.Module>();
//                resp.AddMessage(true, MessageConstants.ERROR_STORAGE_CODE, MessageConstants.ERROR_STORAGE_TEXT);
//                return resp;
//            }
//            finally
//            {
//                if (reader != null)
//                    reader.Dispose();
//                if (command != null)
//                    command.Dispose();
//            }
//        }

//        /// <summary>
//        /// Update.
//        /// </summary>
//        /// <param name="domain"></param>
//        /// <returns></returns>
//        public override IResponseItem<DomainModel.Module> Update(IRequestItem<DomainModel.Module> request)
//        {
//            SqlCommand command = null;
//            try
//            {
//                IStorageServiceSession session = _storageService.GetSession();
//                command = new SqlCommand();
//                command.Connection = session.Session as SqlConnection;
//                command.Transaction = session.Transaction as SqlTransaction;
//                command.CommandType = CommandType.Text;
//                command.CommandText = "update Eleflex.Module set ModuleName=@ModuleName,UpdateDate=@UpdateDate,Major=@Major,Minor=@Minor,Build=@Build,Revision=@Revision,ExtraData=@ExtraData where ModuleKey=@ModuleKey";
//                command.Parameters.Add(new SqlParameter("@ModuleKey", request.Item.ModuleKey));
//                command.Parameters.Add(new SqlParameter("@ModuleName", request.Item.ModuleName));
//                command.Parameters.Add(new SqlParameter("@UpdateDate", request.Item.UpdateDate));                
//                command.Parameters.Add(new SqlParameter("@Major", request.Item.Version.Major));
//                command.Parameters.Add(new SqlParameter("@Minor", request.Item.Version.Minor));
//                command.Parameters.Add(new SqlParameter("@Build", request.Item.Version.Build));
//                command.Parameters.Add(new SqlParameter("@Revision", request.Item.Version.Revision));
//                command.Parameters.Add(new SqlParameter("@ExtraData", request.Item.ExtraData == null ? (object)DBNull.Value : request.Item.ExtraData));
//                int updated = command.ExecuteNonQuery();
//                return new ResponseItem<DomainModel.Module>() { Item = request.Item };
//            }
//            catch (Exception ex)
//            {
//                Logger.Current.Error<ModuleSqlRepository>(ex);
//                ResponseItem<DomainModel.Module> resp = new ResponseItem<Eleflex.Module>();
//                resp.AddMessage(true, MessageConstants.ERROR_STORAGE_CODE, MessageConstants.ERROR_STORAGE_TEXT);
//                return resp;
//            }
//            finally
//            {
//                if (command != null)
//                    command.Dispose();
//            }
//        }

//        /// <summary>
//        /// Delete.
//        /// </summary>
//        /// <param name="key"></param>
//        public override IResponse Delete(IRequestItem<Guid> request)
//        {
//            SqlCommand command = null;
//            try
//            {
//                IStorageServiceSession session = _storageService.GetSession();
//                command = new SqlCommand();
//                command.Connection = session.Session as SqlConnection;
//                command.Transaction = session.Transaction as SqlTransaction;
//                command.CommandType = CommandType.Text;
//                command.CommandText = "delete from Eleflex.Module where ModuleKey=@ModuleKey";
//                command.Parameters.Add(new SqlParameter("@ModuleKey", request.Item));
//                int updated = command.ExecuteNonQuery();
//                return new Response();
//            }
//            catch (Exception ex)
//            {
//                Logger.Current.Error<ModuleSqlRepository>(ex);
//                Response resp = new Response();
//                resp.AddMessage(true, MessageConstants.ERROR_STORAGE_CODE, MessageConstants.ERROR_STORAGE_TEXT);
//                return resp;
//            }
//            finally
//            {
//                if (command != null)
//                    command.Dispose();
//            }
//        }

//        /// <summary>
//        /// Query.
//        /// </summary>
//        /// <param name="storageQuery"></param>
//        /// <returns></returns>
//        public override IStorageQueryResponseItems<DomainModel.Module> Query(IRequestItem<IStorageQuery> request)
//        {
//            return new StorageQueryResponseItems<DomainModel.Module>() { Items = GetAll() };
//        }

//        /// <summary>
//        /// Query entities for an aggregate.
//        /// </summary>
//        /// <param name="storageQuery"></param>
//        /// <returns></returns>
//        public override IResponseItem<double> QueryAggregate(IRequestItem<IStorageQuery> request)
//        {
//            return new ResponseItem<double>() { Item = GetAll().Count };
//        }

//        /// <summary>
//        /// Determine if repository is installed or not.
//        /// </summary>
//        public virtual bool IsInstalled()
//        {
//            SqlCommand command = null;
//            SqlDataReader reader = null;
//            try
//            {
//                IStorageServiceSession session = _storageService.GetSession();
//                command = new SqlCommand();
//                command.Connection = session.Session as SqlConnection;
//                command.Transaction = session.Transaction as SqlTransaction;
//                command.CommandType = CommandType.Text;
//                command.CommandText = SCRIPT_TABLE_EXISTS;
//                reader = command.ExecuteReader();
//                if (reader.Read())
//                    return Convert.ToBoolean(reader.GetValue(0));
//                return false;
//            }
//            catch
//            {                
//                return false;
//            }
//            finally
//            {
//                if (reader != null)
//                    reader.Dispose();
//                if (command != null)
//                    command.Dispose();
//            }
//        }

//        protected const string SCRIPT_TABLE_EXISTS = @"
//IF OBJECT_ID(N'Eleflex.Module', N'U') IS NOT NULL
//BEGIN
//  select 1
//END
//ELSE
//BEGIN
//select 0
//END
//";

//        /// <summary>
//        /// Get all.
//        /// </summary>
//        /// <returns></returns>
//        public virtual IList<DomainModel.Module> GetAll()
//        {
//            SqlCommand command = null;
//            SqlDataReader reader = null;
//            try
//            {
//                IStorageServiceSession session = _storageService.GetSession();
//                command = new SqlCommand();
//                command.Connection = session.Session as SqlConnection;
//                command.Transaction = session.Transaction as SqlTransaction;
//                command.CommandType = CommandType.Text;
//                command.CommandText = "select ModuleKey, ModuleName,UpdateDate,Major,Minor,Build,Revision,ExtraData from Eleflex.Module";
//                reader = command.ExecuteReader();
//                DomainModel.Module item = null;
//                List<DomainModel.Module> list = new List<DomainModel.Module>();
//                while (reader.Read())
//                {
//                    item = new DomainModel.Module();
//                    list.Add(item);                    
//                    item.ChangeModuleKey(reader.GetGuid(0));
//                    item.ChangeModuleName(Convert.ToString(reader.GetValue(1)));
//                    item.ChangeUpdateDate(reader.GetDateTimeOffset(2));
//                    DomainModel.Version version = new DomainModel.Version();
//                    version.ChangeMajor(Convert.ToInt32(reader.GetValue(3)));
//                    version.ChangeMinor(Convert.ToInt32(reader.GetValue(4)));
//                    version.ChangeBuild(Convert.ToInt32(reader.GetValue(5)));
//                    version.ChangeRevision(Convert.ToInt32(reader.GetValue(6)));
//                    item.ChangeVersion(version);
//                    item.ChangeExtraData(Convert.ToString(reader.GetValue(7)));
//                }
//                return list;
//            }
//            catch (Exception ex)
//            {
//                Logger.Current.Error<ModuleSqlRepository>(ex);
//                return null;
//            }
//            finally
//            {
//                if (reader != null)
//                    reader.Dispose();
//                if (command != null)
//                    command.Dispose();
//            }
//        }

//    }
//}
