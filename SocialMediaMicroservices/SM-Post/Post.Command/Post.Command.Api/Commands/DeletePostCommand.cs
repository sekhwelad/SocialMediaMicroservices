using CQRS.Core.Commands;

namespace Post.Command.Api.Commands
{
    public class DeletePostCommand : BaseCommand
    {
        public string Username { get; set; }
    }
}
