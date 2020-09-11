namespace EventSourcedCallAudit.CallEvents
{
    public class Error
    {
        public string reason { get; set; }
        public string conversation_uuid { get; set; }
        public string timestamp { get; set; }
    }
}