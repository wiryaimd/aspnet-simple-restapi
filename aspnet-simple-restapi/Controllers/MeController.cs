using aspnet_simple_restapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aspnet_simple_restapi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MeController : ControllerBase
    {

        private readonly AppDbContext _context;

        public MeController(AppDbContext context)
        {
            _context = context;
        }

        //[HttpPost]
        //public async Task<ActionResult<User>> MePost() { 

        //}

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<User>> Me() {
            string name = User.Identity.Name;

            Console.WriteLine("my name: " + name);

            User? user = _context.Users.Where(u => u.Email.Equals(name)).FirstOrDefault();
            if (user == null) {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
