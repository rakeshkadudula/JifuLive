using DirectScale.Disco.Extension.Services;
using JifuLive.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace JifuLive.Controllers
{


    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAssociateService _associateService;

        public UsersController(IAssociateService associateService)
        {
            _associateService = associateService;
        }

        // 1. Get all users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            // For now, return a mock list of users
            var users = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "johndoe@example.com", Role = "admin" },
                new User { Id = 2, Name = "Jane Smith", Email = "janesmith@example.com", Role = "user" }
            };

            return Ok(users);
        }

        // 2. Get user by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var associate = await _associateService.GetAssociate(id);

            if (associate == null)
            {
                return NotFound();
            }

            return Ok(associate);
        }


    }
}
