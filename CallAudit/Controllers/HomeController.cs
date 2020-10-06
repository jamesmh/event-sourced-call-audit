using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CallAudit.Models;
using CallAudit.Projections;
using Marten;

namespace CallAudit.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDocumentStore _store;

        public HomeController(IDocumentStore store)
        {
            this._store = store;
        }

        public async Task<IActionResult> Index()
        {
            using var session = this._store.OpenSession();
            
            var model = new IndexModel();
            model.Conversations = await session.Query<Conversation>()
                .ToListAsync();

            return this.View(model);
        }
    }
}
