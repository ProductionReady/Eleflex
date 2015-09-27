using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex
{
    public partial interface ISystemStartupShutdown
    {
        void Start(ITaskOptions taskOptions);

        void Stop(ITaskOptions taskOptions);

    }
}
