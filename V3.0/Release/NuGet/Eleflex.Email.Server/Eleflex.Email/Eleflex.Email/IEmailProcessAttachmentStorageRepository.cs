using System;
using System.Collections.Generic;

namespace Eleflex.Email
{
    /// <summary>
    /// Represents a EmailProcessAttachment storage repository.
    /// </summary>
    public partial interface IEmailProcessAttachmentStorageRepository : IStorageRepository<EmailProcessAttachment, long>
    {
    }
}

