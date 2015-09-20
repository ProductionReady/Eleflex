using System;

namespace Eleflex.Lookups
{
    /// <summary>
    /// Represents a Lookup business repository.
    /// </summary>
    public partial class LookupBusinessRepository : BusinessRepository<Lookup, System.Guid>, ILookupBusinessRepository
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="businessRuleService"></param>
		/// <param name="contextBuilder"></param>
        /// <param name="storageRepository"></param>
        public LookupBusinessRepository(
            IBusinessRuleService businessRuleService,
			IContextBuilder contextBuilder,
            IStorageRepository<Lookup, System.Guid> storageRepository)
            : base(
                  businessRuleService,
				  contextBuilder,
                  storageRepository)
        {
        }
    }
}
