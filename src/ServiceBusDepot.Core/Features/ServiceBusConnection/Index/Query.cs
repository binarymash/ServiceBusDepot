namespace ServiceBusDepot.Core.Features.ServiceBusConnection.Index
{
    using MediatR;
    using System.Collections.Generic;

    public class Query : IAsyncRequest<List<Model>>
    {
    }
}
