using System;
using System.Threading.Tasks;
using CallAudit.Events;
using Marten;
using Vonage.Voice.EventWebhooks;

namespace CallAudit.Handlers
{
    public class CallAuditHandlers
    {
        private IDocumentSession _session;

        public CallAuditHandlers(IDocumentSession session)
        {
            this._session = session;
        }

        public async Task Handle(CallStatusEvent @event)
        {
            switch (@event)
            {
                case Started started:
                    this.HandleCallStarted(started);
                    break;
                case Answered answered:
                    this.HandleCallAnswered(answered);
                    break;
                case Completed completed:
                    this.HandleCallCompleted(completed);
                    break;
            }

            await this._session.SaveChangesAsync();
        }

        private void HandleCallStarted(Started started)
        {
            var eventToStore = new CallStarted
            {
                ConversationId = Guid.Parse(FormatUuid(started.ConversationUuid)),
                From = started.From,
                To = started.To
            };
        
            // Create an individual stream per phone conversation.
            this._session.Events.Append(eventToStore.ConversationId, eventToStore);
        }
    
        private void HandleCallAnswered(Answered answered)
        {
            var eventToStore = new CallAnswered()
            {
                ConversationId = Guid.Parse(FormatUuid(answered.ConversationUuid))
            };
        
            this._session.Events.Append(eventToStore.ConversationId, eventToStore);
        }

        private void HandleCallCompleted(Completed completed)
        {
            var eventToStore = new CallCompleted()
            {
                ConversationId = Guid.Parse(FormatUuid(completed.ConversationUuid)),
                StartTime = completed.StartTime,
                EndTime = completed.EndTime,
                Duration = int.Parse(completed.Duration)
            };
        
            this._session.Events.Append(eventToStore.ConversationId, eventToStore);
        }

        private static string FormatUuid(string conversationUuid)
        {
            return conversationUuid.Replace("CON-", string.Empty);
        }
    }
}