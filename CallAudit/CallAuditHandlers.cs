using System;
using System.Threading.Tasks;
using EventSourcedCallAudit.CallAudit.Events;
using EventSourcedCallAudit.CallAudit.Projections;
using Marten;
using Marten.Events;

namespace EventSourcedCallAudit.CallAudit
{
    public class CallAuditHandlers
    {
        private IDocumentSession _session;

        public CallAuditHandlers(IDocumentSession session)
        {
            this._session = session;
        }

        public async Task Handle(WebhookEvent @event)
        {
            Guid streamId = Guid.Empty;
            if (@event.status == "started")
            {
                streamId = this.HandleCallStarted(@event);
            }
            
            if (@event.status == "answered")
            {
                streamId = this.HandleCallAnswered(@event);
            }

            if (@event.status == "completed")
            {
                streamId = this.HandleCallCompleted(@event);
            }
            
            await this._session.SaveChangesAsync();
        }

        private Guid HandleCallStarted(WebhookEvent @event)
        {
            var eventToStore = new CallStarted
            {
                Id = Guid.Parse(@event.uuid),
                ConversationId = Guid.Parse(@event.conversation_uuid),
                From = @event.from,
                To = @event.to
            };
            
            // Create an individual stream per phone conversation.
            this._session.Events.StartStream<Conversation>(eventToStore.ConversationId, eventToStore);
            return eventToStore.ConversationId;
        }
        
        private Guid HandleCallAnswered(WebhookEvent @event)
        {
            var eventToStore = new CallAnswered()
            {
                Id = Guid.Parse(@event.uuid),
                ConversationId = Guid.Parse(@event.conversation_uuid)
            };
            
            this._session.Events.Append(eventToStore.ConversationId, eventToStore);
            return eventToStore.ConversationId;
        }

        private Guid HandleCallCompleted(WebhookEvent @event)
        {
            var eventToStore = new CallCompleted()
            {
                Id = Guid.Parse(@event.uuid),
                ConversationId = Guid.Parse(@event.conversation_uuid),
                StartTime = DateTimeOffset.Parse(@event.start_time),
                EndTime = DateTimeOffset.Parse(@event.end_time),
                Duration = @event.duration
            };
            
            this._session.Events.Append(eventToStore.ConversationId, eventToStore);
            return eventToStore.ConversationId;
        }
    }
}