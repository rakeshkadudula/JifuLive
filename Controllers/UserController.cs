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
            private readonly HttpClient _httpClient;
        
            public UsersController(HttpClient httpClient, IAssociateService associateService )
            {
                _httpClient = httpClient;
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
            
                var url = $"https://directscale.api.url/api/extension/services/AssociateService/v1/GetAssociate/{id}";
                var response = await _httpClient.GetStringAsync(url);
                var associate = JsonConvert.DeserializeObject<User>(response);

                if (associate == null)
                {
                    return NotFound();
                }

                return Ok(associate);
            }

        
        }
    }
