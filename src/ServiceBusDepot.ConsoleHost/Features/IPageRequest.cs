using MediatR;
using ServiceBusDepot.ConsoleHost.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusDepot.ConsoleHost.Features
{
    public interface IPageRequest : IRequest<IPageRequest>
    {
    }
}
