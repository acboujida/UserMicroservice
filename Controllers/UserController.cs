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
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDTO>>(_userRepository.GetUsers());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int id)
        {
            var user = _mapper.Map<UserDTO>(_userRepository.GetUser(id));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_userRepository.UserExists(id)) return NotFound();

            return Ok(user);
        }

        [HttpGet("id/{name}")]
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetUser(string name)
        {
            var user = _mapper.Map<UserDTO>(_userRepository.GetUser(name));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_userRepository.UserExists(name)) return NotFound();

            return Ok(user);
        }

        [HttpGet("{id}/reviews")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetReviewsOfUser(int id)
        {
            var reviews = _mapper.Map<List<ReviewDTO>>(_userRepository.GetReviewsOfUser(id));

            if (!_userRepository.UserExists(id)) return NotFound();

            if(!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(200)] // success
        [ProducesResponseType(400)] // bad request
        [ProducesResponseType(409)] // model exists
        [ProducesResponseType(500)] // error in saving
        public IActionResult CreateUser([FromBody] UserDTO userDTO)
        {
            var exists = _userRepository.GetUsers().Any(u => u.Email.Trim().ToLower() == userDTO.Email.Trim().ToLower());

            if(exists)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(409, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = _mapper.Map<User>(userDTO);

            if(!_userRepository.CreateUser(user))
            {
                ModelState.AddModelError("", "Error in saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully added!");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteUser(int id)
        {
            if(!_userRepository.UserExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return BadRequest();

            var user = _userRepository.GetUser(id);

            if(!_userRepository.DeleteUser(user))
            {
                ModelState.AddModelError("", "Error in saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
