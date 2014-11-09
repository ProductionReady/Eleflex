using System;
using System.Collections.Generic;

namespace Eleflex.Security
{
    public partial class CryptoKey
    {
        public byte[] Key { get; set; }

        public byte[] IV { get; set; }
    }
}
