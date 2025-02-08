using backend.Data;
using backend.Models.Domain;
using backend.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories.Implementation
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment; // provided application's env information ex. current root path
        private readonly IHttpContextAccessor httpContextAccessor; // access HTTP req/res data in app
        private readonly ApplicationDbContext dbContext;

        public ImageRepository(IWebHostEnvironment webHostEnvironment,
                               IHttpContextAccessor httpContextAccessor,
                               ApplicationDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }

        public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImage)
        {
            // save file
            var localPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{blogImage.FileName}{blogImage.FileExtension}");
            using var stream = new FileStream(localPath, FileMode.Create); // filestream for write file
            await file.CopyToAsync(stream); // copy to local path destination

            // update database
            // ex.https://codepulse.com/images/imgfile.jpg
            var httpRequest = httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{blogImage.FileName}{blogImage.FileExtension}";
            
            blogImage.Url = urlPath;
            await dbContext.BlogImages.AddAsync(blogImage);
            await dbContext.SaveChangesAsync();

            return blogImage;
        }

        public async Task<IEnumerable<BlogImage>> GetAll()
        {
            return await dbContext.BlogImages.ToListAsync();
        }

    }
}
