using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Data.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto registration)
        {
            //validate the request
            registration.Username = registration.Username.ToLower();
            if (await _authRepository.UserExists(registration.Username))
            {
                return BadRequest("User already exists");
            }

            var userToCreate = new User 
            {
                Username = registration.Username
            };

            var createdUser = await _authRepository.Register(userToCreate, registration.Password);

            return StatusCode(201); //CreatedAtRoute() at later time
        }        
    }
}