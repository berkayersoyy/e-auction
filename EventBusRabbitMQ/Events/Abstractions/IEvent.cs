using System;

namespace EventBusRabbitMQ.Events.Abstractions
{
    public abstract class IEvent
    {
        protected IEvent()
        {
            CreationDate = DateTime.UtcNow;
            RequestId = Guid.NewGuid();
        }

        public Guid RequestId { get; private init; }
        public DateTime CreationDate { get; private init; }
    }
}