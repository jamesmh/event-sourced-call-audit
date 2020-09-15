using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Baseline;
using EventSourcedCallAudit.CallAudit;
using EventSourcedCallAudit.CallAudit.Projections;
using Marten;
using Marten.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace EventSourcedCallAudit.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IDocumentStore _store;

        public IEnumerable<Conversation> Conversations { get; set; }

        public IndexModel(IDocumentStore store)
        {
            this._store = store;
        }

        public async Task<IActionResult> OnGet()
        {
            using var session = this._store.OpenSession();
            
            // Get all conversations for the last 30 days.
            this.Conversations = await session.Query<Conversation>()
                .Where(c => c.EndedAt > DateTimeOffset.UtcNow.Subtract(TimeSpan.FromDays(30)))
                .ToListAsync();

            return this.Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var convoUUID = Guid.NewGuid();
            var callStarted = new WebhookEvent
            {
                conversation_uuid = convoUUID.ToString(),
                uuid = Guid.NewGuid().ToString(),
                to = "9998887777",
                from = "9998887777",
                status = "started"
            };
            var callAnswered = new WebhookEvent
            {
                conversation_uuid = convoUUID.ToString(),
                uuid = Guid.NewGuid().ToString(),
                to = "9998887777",
                from = "9998887777",
                status = "answered"
            };
            var callCompleted = new WebhookEvent
            {
                conversation_uuid = convoUUID.ToString(),
                uuid = Guid.NewGuid().ToString(),
                to = "9998887777",
                from = "9998887777",
                status = "completed",
                start_time = DateTimeOffset.UtcNow.ToString(),
                end_time = DateTimeOffset.UtcNow.ToString(),
                duration = 5
            };

            using (var session = this._store.OpenSession())
            {
                var handlers = new CallAuditHandlers(session);
                await handlers.Handle(callStarted);
            }

            using (var session = this._store.OpenSession())
            {
                var handlers = new CallAuditHandlers(session);
                await handlers.Handle(callAnswered);
            }

            using (var session = this._store.OpenSession())
            {
                var handlers = new CallAuditHandlers(session);
                await handlers.Handle(callCompleted);
            }

            return await this.OnGet();
        }
    }
}