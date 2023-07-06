using Post.Query.Domain.Entities;
namespace Post.Query.Domain.Repositories
{
    public interface IPostRepository
    {
        Task CreateAsync(PostEntity post);
        Task UpdateAsync(PostEntity post);
        Task DeleteAsync(Guid postId);
        Task<PostEntity> GetByIdAsync(Guid postId);
        Task<List<PostEntity>> ListAllAsync();
        Task<List<PostEntity>> ListByAuthorAsync();
        Task<List<PostEntity>> ListWithLikesAsync();
        Task<List<PostEntity>> ListWithCommentsAsync();
    }
}
