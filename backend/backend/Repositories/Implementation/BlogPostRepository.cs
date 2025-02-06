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

        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogpost)
        {
            var existingBlogPost = await dbContext.BlogPosts.Include(x => x.Categories)
                                                            .FirstOrDefaultAsync(x => x.Id ==  blogpost.Id);
            if (existingBlogPost == null) { return null; }

            // update blogPost
            dbContext.Entry(existingBlogPost).CurrentValues.SetValues(blogpost);

            // update category
            existingBlogPost.Categories = blogpost.Categories;

            await dbContext.SaveChangesAsync();

            return existingBlogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlogPost = await dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
            if (existingBlogPost == null) { return null; }

            dbContext.Remove(existingBlogPost);
            await dbContext.SaveChangesAsync();

            return existingBlogPost;
        }
    }
}
