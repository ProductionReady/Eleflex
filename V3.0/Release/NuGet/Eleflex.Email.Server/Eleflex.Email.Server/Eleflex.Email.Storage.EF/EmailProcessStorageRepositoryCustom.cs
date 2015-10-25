using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using Eleflex.Storage.EF;

namespace Eleflex.Email.Storage.EF
{
    public partial class EmailProcessStorageRepository
    {

        public ResponseItems<EmailProcess> GetBatchEmaiForProcessing(int number)
        {            
            try
            {
                //Can't use Entity Framework here because we want to lock the records we read, then update them as being processed because multiple processes may be running in a distributed environment
                //The UPDLOCK locks the rows selected using an update lock so that other processes cannot select the same rows. This will blocking access selecting from the table until this is committed.
                string commandGet = @"
CREATE TABLE #TempEmailProcess(EmailProcessKey BIGINT)

INSERT INTO #TempEmailProcess (EmailProcessKey)
SELECT TOP " + number.ToString() + @" [EmailProcessKey]      
  FROM [EleflexV3].[EmailProcess] WITH (UPDLOCK)
WHERE [SentDate] IS NULL
AND [IsError] = 0
AND [IsProcessing] = 0
AND 
(
    [FutureSendDate] IS NULL OR
    [FutureSendDate] <= {0}
)

UPDATE [EleflexV3].[EmailProcess] SET [IsProcessing] = 1, [ProcessingDate] = {0}
WHERE [EmailProcessKey] in (SELECT [EmailProcessKey] FROM #TempEmailProcess)

SELECT ep.[EmailProcessKey]
      ,ep.[FromAddress]
      ,ep.[ToAddress]
      ,ep.[CcAddress]
      ,ep.[BccAddress]
      ,ep.[Subject]
      ,ep.[Body]
      ,ep.[IsHtml]
      ,ep.[CreateDate]
      ,ep.[FutureSendDate]
      ,ep.[SentDate]
      ,ep.[IsError]
      ,ep.[ErrorDate]
      ,ep.[ErrorMessage]
      ,ep.[IsProcessing]
      ,ep.[ProcessingDate]
  FROM [EleflexV3].[EmailProcess] AS ep
  JOIN #TempEmailProcess AS tep ON ep.[EmailProcessKey] = tep.[EmailProcessKey]

DROP TABLE #TempEmailProcess
";

                List<Eleflex.Email.Server.EmailProcess> list;
                using (var session = _storageService.CreateNonManagedSession())
                {
                    var context = session.Session as Eleflex.Email.Server.EmailDB;
                    list = context.Database.SqlQuery<Eleflex.Email.Server.EmailProcess>(commandGet, DateTime.UtcNow).ToList();
                    session.Commit();
                }
                return new ResponseItems<EmailProcess>(_mappingService.Map< Eleflex.Email.Server.EmailProcess, Eleflex.Email.EmailProcess>(list));
            }
            catch(Exception ex)
            {
                Logger.Current.Error<EmailProcessStorageRepository>(ex);
                var resp = new ResponseItems<EmailProcess>();
                resp.AddMessage(true, MessageConstants.ERROR_STORAGE_CODE, MessageConstants.ERROR_STORAGE_TEXT);
                return resp;
            }
        }


        public virtual Response PurgeHistoricalEmails(DateTimeOffset expirationDate)
        {
            try
            {

                string command = @"
DELETE FROM [EleflexV3].[EmailProcessAttachment]
WHERE [EmailProcessKey] IN
(
SELECT [EmailProcessKey] FROM [EleflexV3].[EmailProcess]
WHERE [CreateDate] < {0}
)

DELETE FROM [EleflexV3].[EmailProcess]
WHERE [CreateDate] < {0}

";
                using (var session = _storageService.CreateNonManagedSession())
                {
                    var context = session.Session as Eleflex.Email.Server.EmailDB;
                    context.Database.ExecuteSqlCommand(command, expirationDate);
                    session.Commit();
                }                
                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Current.Error<EmailProcessStorageRepository>("PurgeHistoricalEmails", ex);
                var resp = new Response();
                resp.AddMessage(true, MessageConstants.ERROR_STORAGE_CODE, MessageConstants.ERROR_STORAGE_TEXT);
                return resp;
            }
        }
    }
}
