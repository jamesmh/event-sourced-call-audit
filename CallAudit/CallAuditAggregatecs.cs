using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Marten;
using Marten.Events;

namespace EventSourcedCallAudit.CallAudit
{
    public class CallAuditAggregate
    {
        private IEventStore _store;

        public CallAuditAggregate(IDocumentSession session)
        {
            this._store = session.Events;
        }

        public async Task Handle(WebhookEvent @event)
        {
            var handlers =
                new Dictionary<string, Func<WebhookEvent, Task>> {
                    { "started", CallStarted },
                    { "ringing", CallRinging },
                    { "answered", CallAnswered },
                    { "cancelled", CallCancelled },
                    { "completed", CallCompleted }
                };
            
            var handler = handlers[@event.status];
            
            if (handler != null)
            {
                await handler(@event);
            }
        }

        private async Task CallStarted(WebhookEvent @event)
        {
            throw new System.NotImplementedException();
        }
        
        private async Task CallRinging(WebhookEvent @event)
        {
            throw new System.NotImplementedException();
        }
        
        private async Task CallAnswered(WebhookEvent @event)
        {
            throw new System.NotImplementedException();
        }
        
        private async Task CallCancelled(WebhookEvent @event)
        {
            throw new System.NotImplementedException();
        }
        
        private async Task CallCompleted(WebhookEvent @event)
        {
            throw new System.NotImplementedException();
        }
    }
}