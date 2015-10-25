using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.Email
{
    public partial interface IEmailProcessStorageRepository
    {

        ResponseItems<EmailProcess> GetBatchEmaiForProcessing(int number);

        Response PurgeHistoricalEmails(DateTimeOffset expirationDate);
    }
}
