using System;

namespace Eleflex
{
    /// <summary>
    /// Represents a LogMessage business repository.
    /// </summary>
    public partial class LogMessageBusinessRepository : BusinessRepository<LogMessage, long>, ILogMessageBusinessRepository
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="businessRuleService"></param>
		/// <param name="contextBuilder"></param>
        /// <param name="storageRepository"></param>
        public LogMessageBusinessRepository(
            IBusinessRuleService businessRuleService,
			IContextBuilder contextBuilder,
            IStorageRepository<LogMessage, long> storageRepository)
            : base(
                  businessRuleService,
				  contextBuilder,
                  storageRepository)
        {
        }
    }
}
