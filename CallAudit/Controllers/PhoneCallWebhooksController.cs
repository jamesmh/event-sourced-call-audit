using System.IO;
using System.Threading.Tasks;
using CallAudit.Handlers;
using Marten;
using Microsoft.AspNetCore.Mvc;
using Vonage.Voice.EventWebhooks;
using Vonage.Voice.Nccos;

namespace CallAudit.Controllers
{
    public class PhoneCallWebhooksController : Controller
    {
        private readonly IDocumentStore _store;

        public PhoneCallWebhooksController(IDocumentStore store)
        {
            this._store = store;
        }
        
        [HttpGet("/track-call")]
        public string TrackCall()
        {
            var talkAction = new TalkAction
            {
                Text = "This call will be tracked and stored using event sourcing."
            };
            var ncco = new Ncco(talkAction);
            return ncco.ToString();
        }
        
        [HttpPost("/event")]
        public async Task<IActionResult> Event()
        {
            // Read the incoming json and load it as the
            // proper C# type it represents ("Started", "Answered", etc.)
            var json = await new StreamReader(this.Request.Body).ReadToEndAsync();
            var @event = (CallStatusEvent) EventBase.ParseEvent(json);
            
            using var session = this._store.OpenSession();
            await new CallAuditHandlers(session).Handle(@event);
            return this.Ok();
        }
    }
}