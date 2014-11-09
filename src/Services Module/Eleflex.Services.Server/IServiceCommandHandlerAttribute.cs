using System;
using System.Collections.Generic;


namespace Eleflex.Services.Server
{
    public interface IServiceCommandHandlerRequestType
    {
        Type RequestType { get; }
    }
}
