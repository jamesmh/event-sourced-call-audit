namespace EventSourcedCallAudit.CallAudit
{
// Matches the expected JSON values available from the call tracking events:
// https://developer.nexmo.com/voice/voice-api/webhook-reference#event-webhook
    public class WebhookEvent
    {
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string from { get; set; }
        public string to { get; set; }

        private string _conversation_uuid;

        public string conversation_uuid
        {
            // uuids from Vonage have "CON-" prefixed. 
            // We'll remove that prefix so we can parse into a Guid.
            get => this._conversation_uuid.Remove(0, 4);
            set => this._conversation_uuid = value;
        }

        public string status { get; set; }
        public int duration { get; set; }
    }
}