namespace EventSourcedCallAudit.CallAudit
{
    // Matches the expected JSON values available from the call tracking events:
    // https://developer.nexmo.com/voice/voice-api/webhook-reference#event-webhook
    public class WebhookEvent
    {
        public string end_time { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string uuid { get; set; }
        public string conversation_uuid { get; set; }
        public string status { get; set; }
        public int duration { get; set; }
        public string start_time { get; set; }
    }
}