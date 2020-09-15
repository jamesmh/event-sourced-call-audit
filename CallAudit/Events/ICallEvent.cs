using System;
using Marten.Events;

namespace EventSourcedCallAudit.CallAudit.Events
{
    public interface ICallEvent
    {
        public Guid Id { get; set; }
        public Guid ConversationId { get; set; }
    }
}