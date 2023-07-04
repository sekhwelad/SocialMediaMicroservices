using CQRS.Core.Commands;

namespace Post.Command.Api.Commands
{
    public class EditMessageCommand : BaseCommand
    {
        public string Message { get; set; }
    }
}
