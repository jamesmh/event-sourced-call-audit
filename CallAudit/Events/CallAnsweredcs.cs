using System;

namespace EventSourcedCallAudit.CallAudit.Events
{
    public class CallAnswered : ICallEvent
    {
        public Guid Id { get; set; }
        public Guid ConversationId { get; set; }
    }
}