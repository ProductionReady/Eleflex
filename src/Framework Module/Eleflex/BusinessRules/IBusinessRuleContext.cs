using System;
using System.Collections.Generic;
using System.Linq;

namespace Eleflex
{
    public interface IBusinessRuleContext
    {        

        Dictionary<string, object> State { get; set; }

    }
}
