using System;

namespace EventSourcedCallAudit.CallAudit.Events
{
    public class CallStarted
    {
        public Guid Id { get; set; }
        public Guid ConversationId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}