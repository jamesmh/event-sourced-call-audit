using System;

namespace EventSourcedCallAudit.CallAudit.Events
{
    public class CallAnswered
    {
        public Guid Id { get; set; }
        public Guid ConversationId { get; set; }
    }
}