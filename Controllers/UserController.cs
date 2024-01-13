using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserMicroservice.DTO;
using UserMicroservice.Interfaces;
using UserMicroservice.Models;

namespace UserMicroservice.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type= typeof(IEnumerable<User>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDTO>>(_userRepository.GetUsers());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(users);            
        }
    }
}
