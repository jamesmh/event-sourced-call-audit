using System;

namespace EventSourcedCallAudit.CallAudit.Events
{
    public class CallCompletedEvent : IEvent
    {
        public string UUID { get; set; }
        public string Conversation_UUID { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}