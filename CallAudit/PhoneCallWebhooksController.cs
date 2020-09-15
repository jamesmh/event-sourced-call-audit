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
}