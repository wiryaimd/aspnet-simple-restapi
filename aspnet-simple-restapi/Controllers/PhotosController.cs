using aspnet_simple_restapi.Dtos;
using aspnet_simple_restapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aspnet_simple_restapi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PhotosController : ControllerBase
    {

        private readonly AppDbContext _context;
        private string path = Path.GetFullPath(Environment.CurrentDirectory);

        public PhotosController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Photo>>> GetPhotos() {
            return await _context.Photos.ToListAsync();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPhoto(Guid id) {
            Photo? photo = await _context.Photos.FindAsync(id);
            if (photo == null) {
                return NotFound();
            }
            byte[] img = System.IO.File.ReadAllBytes(path + "/Photos/" + photo.Path);
            return File(img, "image/jpg");
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> PostPhoto([FromForm] PhotoDto photoDto) {
            string? email = User.Identity.Name;

            string uid = Guid.NewGuid().ToString().Substring(0, 8);
            string ex = Path.GetExtension(photoDto.Img.FileName);
            using (FileStream file = new FileStream(path + "/Photos/" + uid + ex, FileMode.Create)) {
                photoDto.Img.CopyTo(file);
                file.Flush();
            }

            User? user = _context.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
            if (user == null) {
                Console.WriteLine("user null");
                return NotFound();
            }
            Console.WriteLine("user: " + user.Id + " " + user.Email);

            string albumName = photoDto.Album;
            Album? album = _context.Albums.Where(delegate (Album album1) {
                Console.WriteLine("id alb: " + album1.UserId);
                return album1.Name.Equals(albumName) && album1.UserId.Equals(user.Id);
            }).FirstOrDefault();

            if (album == null) {
                Console.WriteLine("album null");
                return NotFound();
            }

            Console.WriteLine("album: " + album.Id);

            Photo photo = new Photo() { 
                Id = Guid.NewGuid(),
                AlbumId = album.Id,
                FileName = uid + ex,
                Path = uid
            };
            _context.Photos.Add(photo);
            await _context.SaveChangesAsync();
            
            return Ok();
        }
    }
}
