using System;
using CallAudit.Events;

namespace CallAudit.Projections
{
    public class Conversation
    {
        private Conversation() { }
    
        public Guid Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool Answered { get; set; } = false;
        public DateTime? EndedAt { get; set; }
        public int? Duration { get; set; }

        public void Apply(CallStarted started)
        {
            this.From = started.From;
            this.To = started.To;
        }

        public void Apply(CallAnswered answered)
        {
            this.Answered = true;
        }

        public void Apply(CallCompleted completed)
        {
            this.EndedAt = completed.EndTime;
            this.Duration = completed.Duration;
        }
    }

}