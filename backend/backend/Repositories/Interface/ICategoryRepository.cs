using backend.Models.Domain;

namespace backend.Repositories.Interface
{
    public interface ICategoryRepository
    {
        // create data
        Task<Category> CreateAsync(Category category);

        // get all data from the database
        Task<IEnumerable<Category>> GetAllAsync(string? query=null);

        // get data by Guid
        Task<Category> GetByIdAsync(Guid id);

        // update data
        Task<Category?> UpdateAsync(Category category);

        // delete data
        Task<Category?> DeleteAsync(Guid id);
    }
}
