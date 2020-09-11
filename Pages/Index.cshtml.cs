using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
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

        public IndexModel(IDocumentStore store)
        {
            this._store = store;
        }

        public void OnGet()
        {
            using var session = this._store.OpenSession();
            // Add some querying here...
        }
    }
}