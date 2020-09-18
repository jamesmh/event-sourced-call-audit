using System;

namespace EventSourcedCallAudit.CallAudit.Events
{
    public class CallCompleted
    {
        public Guid Id { get; set; }
        public Guid ConversationId { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public int Duration { get; set; }
    }
}