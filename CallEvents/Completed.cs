namespace EventSourcedCallAudit.CallEvents
{
    public class Completed
    {
        public string end_time { get; set; }
        public long from { get; set; }
        public long to { get; set; }
        public string uuid { get; set; }
        public string conversation_uuid { get; set; }
        public string status { get; set; }
        public string direction { get; set; }
        public string timestamp { get; set; }
        
        public int duration { get; set; }
        public string start_time { get; set; }
        public float rate { get; set; }
        public float price { get; set; }
    }
}