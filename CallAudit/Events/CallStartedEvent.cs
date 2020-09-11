using System;

namespace EventSourcedCallAudit.CallAudit.Events
{
    public class CallStartedEvent : IEvent
    {
        public string From { get; set; }
        public string To { get; set; }
        public string UUID { get; set; }
        public string Conversation_UUID { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}