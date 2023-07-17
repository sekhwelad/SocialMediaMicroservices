using CQRS.Core.Queries;

namespace Post.Query.Api.Queries
{
    public class FindPostsByIdQuery : BaseQuery
    {
        public Guid Id { get; set; }
    }
}
