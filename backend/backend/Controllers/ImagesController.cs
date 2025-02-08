using backend.Models.Domain;
using backend.Models.Dto;
using backend.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : Controller
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        // GET : https://localhost:7188/api/images
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            var images = await imageRepository.GetAll();

            // convert to Dto
            var response = new List<BlogImageDto>();
            foreach (var image in images) 
            {
                response.Add(new BlogImageDto
                {
                    Id = image.Id,
                    FileName = image.FileName,
                    FileExtension = image.FileExtension,
                    Title = image.Title,
                    Url = image.Url,
                    DateCreated = image.DateCreated
                });
            }

            return Ok(response);
        }

        // POST : https://localhost:7188/api/images
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file,
                                                     [FromForm] string fileName,
                                                     [FromForm] string title) 
        {
            validateFileUpload(file);

            if (ModelState.IsValid)
            {
                // file upload
                var blogImage = new BlogImage
                {
                    FileName = fileName,
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    Title = title,
                    DateCreated = DateTime.Now
                };
                blogImage = await imageRepository.Upload(file, blogImage);

                // Domain to Dto
                var response = new BlogImageDto
                {
                    Id = blogImage.Id,
                    FileName = blogImage.FileName,
                    FileExtension = blogImage.FileExtension,
                    Title = blogImage.Title,
                    Url = blogImage.Url,
                    DateCreated = blogImage.DateCreated
                };

                return Ok(response);
            }

            return BadRequest(ModelState);
        }

        private void validateFileUpload(IFormFile file)
        {
            var allowExtension = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowExtension.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported file format");
            }

            if (file.Length > 10485760) // if larger than 10 bytes
            {
                ModelState.AddModelError("file", "File size cannot be more than 10 MB");
            }
        }
    }
}
