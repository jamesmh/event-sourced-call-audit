using Marten;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcedCallAudit.CallAudit
{
    public class PhoneCallWebhooksController : Controller
    {
        private readonly IDocumentStore _store;

        public PhoneCallWebhooksController(IDocumentStore store)
        {
            this._store = store;
        }

        [Route("track-call")]
        [HttpGet]
        public IActionResult Index()
        {
            return this.Json(new
            {
                action = "talk",
                text = "This call will be tracked and stored using event streaming"
            });
        }
        
        [Route("event")]
        [HttpPost]
        public IActionResult Index([FromBody] WebhookEvent @event)
        {
            using var session = this._store.OpenSession();
            
            return this.Json(true);
        }

    }

    // Matches the expected JSON values available from the call tracking events:
    // https://developer.nexmo.com/voice/voice-api/webhook-reference#event-webhook
    public class WebhookEvent
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