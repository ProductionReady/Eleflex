using System;
using System.Collections.Generic;

namespace Eleflex.Email
{
    public partial interface IEmailProcessorService
    {

        void SendEmailProcess();

        void DeleteEmails(DateTimeOffset expirationDate);

    }
}
