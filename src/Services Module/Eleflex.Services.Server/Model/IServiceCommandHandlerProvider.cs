using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.Services.Server
{
    public interface IServiceCommandHandlerProvider
    {
        IServiceCommandHandler Get(Type type);
    }
}
