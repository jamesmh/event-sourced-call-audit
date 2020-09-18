using System;
using System.Threading.Tasks;
using EventSourcedCallAudit.CallAudit.Events;
using EventSourcedCallAudit.CallAudit.Projections;
using Marten;

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
            switch (@event.status)
            {
                case "started":
                    this.HandleCallStarted(@event);
                    break;
                case "answered":
                    this.HandleCallAnswered(@event);
                    break;
                case "completed":
                    this.HandleCallCompleted(@event);
                    break;
            }

            await this._session.SaveChangesAsync();
        }

        private void HandleCallStarted(WebhookEvent @event)
        {
            var eventToStore = new CallStarted
            {
                ConversationId = Guid.Parse(@event.conversation_uuid),
                From = @event.from,
                To = @event.to
            };
            
            // Create an individual stream per phone conversation.
            this._session.Events.StartStream<Conversation>(eventToStore.ConversationId, eventToStore);
        }
        
        private void HandleCallAnswered(WebhookEvent @event)
        {
            var eventToStore = new CallAnswered()
            {
                ConversationId = Guid.Parse(@event.conversation_uuid)
            };
            
            this._session.Events.Append(eventToStore.ConversationId, eventToStore);
        }

        private void HandleCallCompleted(WebhookEvent @event)
        {
            var eventToStore = new CallCompleted()
            {
                ConversationId = Guid.Parse(@event.conversation_uuid),
                StartTime = ParseDateTimeOffset(@event.start_time),
                EndTime = ParseDateTimeOffset(@event.end_time),
                Duration = @event.duration
            };
            
            this._session.Events.Append(eventToStore.ConversationId, eventToStore);
        }

        private static DateTimeOffset? ParseDateTimeOffset(string dateString)
        {
            return DateTimeOffset.TryParse(dateString, out var date)
                ? date
                : default;
        }
    }
}