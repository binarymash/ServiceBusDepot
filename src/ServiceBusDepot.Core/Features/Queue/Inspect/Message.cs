using System;
using System.Collections.Generic;

namespace ServiceBusDepot.Core.Features.Queue.Inspect
{
    public class Message
    {
        public string MessageId { get; set; }

        public string Body{ get; set; }

        public string ContentType { get; set; }

        public string CorrelationId { get; set; }

        public string DeadLetterSource { get; set; }

        public int DeliveryCount { get; set; }

        public long EnqueuedSequenceNumber { get; set; }

        public DateTime EnqueuedTimeUtc { get; set; }

        public DateTime ExpiresAtUtc { get; set; }

        public bool ForcePersistance { get; set; }

        public bool IsBodyConsumed { get; set; }

        public string Label { get; set; }

        //DateTime lockedUntilUtc = brokeredMessage.LockedUntilUtc;

        //Guid lockToken = brokeredMessage.LockToken;

        public string PartitionKey { get; set; }

        public IDictionary<string, object> Properties { get; set; }

        public string ReplyTo { get; set; }

        public string ReplyToSessionId { get; set; }

        public DateTime ScheduledEnqueueTimeUtc { get; set; }

        public long SequenceNumber { get; set; }

        public string SessionId { get; set; }

        public long Size { get; set; }

        //public string State { get; set; }

        public TimeSpan TimeToLive { get; set; }

        public string To { get; set; }

        public string ViaPartitionKey { get; set; }
    }
}
