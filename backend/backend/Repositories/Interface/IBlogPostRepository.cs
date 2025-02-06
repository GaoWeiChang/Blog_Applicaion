using backend.Models.Domain;

namespace backend.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogpost);

        Task<IEnumerable<BlogPost>> GetAllAsync(); 

        Task<BlogPost?> GetByIdAsync(Guid id);

        Task<BlogPost?> GetByUrlHandleAsync(string urlHandle);

        Task<BlogPost?> UpdateAsync(BlogPost blogpost); 

        Task<BlogPost?> DeleteAsync(Guid id);
    }
}
