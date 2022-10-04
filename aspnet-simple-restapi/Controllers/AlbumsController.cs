using aspnet_simple_restapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aspnet_simple_restapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumsController : ControllerBase
    {

        private readonly AppDbContext _context;

        public AlbumsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Album>>> GetAlbums() {
            string? email = User.Identity.Name;
            if (email == null) {
                return NotFound();
            }
            User? user = _context.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();

            return await _context.Albums.Where(a => a.UserId.Equals(user.Id)).ToListAsync();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Album>> PostAlbum(Album album) {
            _context.Albums.Add(album);
            await _context.SaveChangesAsync();
            return Ok(album);
        }
    }
}
