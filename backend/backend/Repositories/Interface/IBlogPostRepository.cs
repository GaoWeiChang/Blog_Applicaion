using backend.Models.Domain;

namespace backend.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogpost);

        Task<IEnumerable<BlogPost>> GetAllAsync(); 
    }
}
