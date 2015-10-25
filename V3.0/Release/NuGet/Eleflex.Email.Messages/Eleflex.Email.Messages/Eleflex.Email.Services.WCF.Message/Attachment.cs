using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.Email.Services.WCF.Message
{
    public partial class Attachment
    {

        public virtual string Filename { get; set; }

        public virtual byte[] Data { get; set; }

    }
}
