using aspnet_simple_restapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

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

        [Authorize]
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

        [HttpGet("jsonp/{num}")]
        public async Task<ActionResult> FetchImg1(int num = 1) {
            HttpClient client = new HttpClient() {
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com/") // hanya bisa base url saja, kalau diisi path, bakal tetep kedetek base aja
            };

            string res = await client.GetStringAsync("todos/" + num); // kasi path disini

            // http request post
            //HttpContent content = new StringContent("{ \"ini jason\": \"lul aowkawokaw\" }", Encoding.UTF8, "application/json"); 
            //HttpResponseMessage resMsg = await client.PostAsync("posts/1", content); // ini untuk post ngapp
            

            return Ok(res);
        }
    }
}
