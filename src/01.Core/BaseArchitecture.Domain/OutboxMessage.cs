using Framework.Domain.Events;

namespace BaseArchitecture.Domain
{
    public class OutboxMessage
    {
        public string Id { get; private set; }
        public string Type { get; private set; }
        public string Content { get; private set; }
        public DateTime OccurredTime { get; private set; }
        public DateTime? PublishedTime { get; private set; }
        public string? Error { get; private set; }

        private OutboxMessage()
        {

        }

        public OutboxMessage(string type, string content)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new NullReferenceException(
                    $"domain event type for save in {nameof(OutboxMessage)} is required");

            if (string.IsNullOrWhiteSpace(content))
                throw new NullReferenceException(
                    $"domain event content for save in {nameof(OutboxMessage)} is required");

            Id = Guid.NewGuid().ToString();
            Type = type;
            Content = content;
            OccurredTime = DateTime.UtcNow;
        }

        public void Publish()
        {
            PublishedTime = DateTime.UtcNow;
            
        }
    }
}
