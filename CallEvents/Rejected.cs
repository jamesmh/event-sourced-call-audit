namespace EventSourcedCallAudit.CallEvents
{
    public class Rejected
    {
        public long from { get; set; }
        public long to { get; set; }
        public string uuid { get; set; }
        public string conversation_uuid { get; set; }
        public string status { get; set; }
        public string direction { get; set; }
        public string timestamp { get; set; }
    }
}