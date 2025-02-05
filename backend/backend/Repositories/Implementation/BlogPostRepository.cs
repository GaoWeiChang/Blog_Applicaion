using backend.Data;
using backend.Models.Domain;
using backend.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<BlogPost> CreateAsync(BlogPost blogpost)
        {
            await dbContext.BlogPosts.AddAsync(blogpost);
            await dbContext.SaveChangesAsync();

            return blogpost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await dbContext.BlogPosts.Include(x => x.Categories).ToListAsync();
        }
    }
}
