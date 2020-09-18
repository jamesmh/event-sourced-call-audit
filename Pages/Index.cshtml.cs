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
                .ToListAsync();

            return this.Page();
        }
    }
}