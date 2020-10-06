using System;

namespace CallAudit.Events
{
    public class CallAnswered
    {
        public Guid Id { get; set; }
        public Guid ConversationId { get; set; }
    }

    public class CallStarted
    {
        public Guid Id { get; set; }
        public Guid ConversationId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }

    public class CallCompleted
    {
        public Guid Id { get; set; }
        public Guid ConversationId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
    }

}