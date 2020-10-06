using System.Collections.Generic;
using CallAudit.Projections;

namespace CallAudit.Models
{
    public class IndexModel
    {
        public IEnumerable<Conversation> Conversations { get; set; }
    }
}