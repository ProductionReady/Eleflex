using System.Collections.Generic;
using DomainModel = Eleflex;

namespace Eleflex.Versioning.Storage.EF
{
    public partial class ModuleStorageRepository
    {

        /// <summary>
        /// Determine if is installed.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsInstalled()
        {
            try
            {
                System.Data.Entity.DbContext context = _storageService.CreateSession().Session as System.Data.Entity.DbContext;
                int val = context.Database.ExecuteSqlCommand("select count(1) from EleflexV3.Module");
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get all.
        /// </summary>
        /// <returns></returns>
        public virtual IList<DomainModel.Module> GetAll()
        {
            IStorageQueryResponseItems<DomainModel.Module> resp = this.Query(new RequestItem<IStorageQuery>() { Item = new StorageQuery() });
            return resp.Items;
        }
    }
}
