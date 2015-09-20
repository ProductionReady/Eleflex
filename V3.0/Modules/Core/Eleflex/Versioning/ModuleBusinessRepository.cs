using System;

namespace Eleflex
{
    /// <summary>
    /// Represents a Module business repository.
    /// </summary>
    public partial class ModuleBusinessRepository : BusinessRepository<Module, System.Guid>, IModuleBusinessRepository
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="businessRuleService"></param>
		/// <param name="contextBuilder"></param>
        /// <param name="storageRepository"></param>
        public ModuleBusinessRepository(
            IBusinessRuleService businessRuleService,
			IContextBuilder contextBuilder,
            IStorageRepository<Module, System.Guid> storageRepository)
            : base(
                  businessRuleService,
				  contextBuilder,
                  storageRepository)
        {
        }
    }
}
