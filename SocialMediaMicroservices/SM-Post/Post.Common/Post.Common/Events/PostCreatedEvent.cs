using CQRS.Core.Events;
namespace Post.Common.Events
{
    public class PostCreatedEvent : BaseEvent
    {
        public PostCreatedEvent(string type) :base(nameof(PostCreatedEvent))
        {    
        }

        public string Author { get; set; }
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
