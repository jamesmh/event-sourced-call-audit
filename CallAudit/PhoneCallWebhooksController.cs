using System.IO;
using System.Threading.Tasks;
using EventSourcedCallAudit.CallAudit;
using Marten;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace EventSourcedCallAudit.Controllers
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
            return this.Json(new[]
            {
                new
                {
                    action = "talk",
                    text = "This call will be tracked and stored using event sourcing."
                }
            });
        }

        [Route("event")]
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] WebhookEvent @event)
        {
            using var session = this._store.OpenSession();
            await new CallAuditHandlers(session).Handle(@event);
            return this.Ok();
        }
    }
}