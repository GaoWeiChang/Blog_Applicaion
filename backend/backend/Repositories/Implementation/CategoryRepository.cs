using backend.Data;
using backend.Models.Domain;
using backend.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            // Query 
            var categories = dbContext.Categories.AsQueryable(); // allow to chain LINQ queries

            return await categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var ctgr = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            if (ctgr == null) { return null; }

            dbContext.Entry(ctgr).CurrentValues.SetValues(category); // dbContext.Entry(ctgr) - เข้าถึง entity entry ที่ EF Core ติดตามอยู่
            await dbContext.SaveChangesAsync();
            
            return category;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var ctgr = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (ctgr == null) { return null; }

            dbContext.Categories.Remove(ctgr);
            await dbContext.SaveChangesAsync();

            return ctgr;
        }
    }
}
