using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using UserMicroservice.Interfaces;
using UserMicroservice.DTO;
using UserMicroservice.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using UserMicroservice.Authorizations;
using Microsoft.AspNetCore.Authorization;

namespace UserMicroservice.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IUserRepository userRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDTO>>(_reviewRepository.GetReviews());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ReviewDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetReview(int id)
        {
            var review = _mapper.Map<ReviewDTO>(_reviewRepository.GetReview(id));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_reviewRepository.ReviewExists(id)) return NotFound();

            return Ok(review);
        }

        [HttpGet("{id}/User")]
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetUserOfReview(int id)
        {
            var user = _mapper.Map<UserDTO>(_reviewRepository.GetUserOfReview(id));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_reviewRepository.ReviewExists(id)) return NotFound();

            return Ok(user);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult CreateReview([FromBody] ReviewDTO reviewdto, [FromQuery] string bookId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var reviewmodel = _mapper.Map<Review>(reviewdto);

            reviewmodel.User = _userRepository.GetUser(userId);

            if (!_reviewRepository.CreateReview(reviewmodel))
            {
                ModelState.AddModelError("", "Adding review failed!");
                return StatusCode(500, ModelState);
            }
            return Ok("Added succefully");
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview([FromBody] ReviewDTO reviewDTO, int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            if(!_reviewRepository.ReviewExists(id)) return NotFound();

            var review = _mapper.Map<Review>(reviewDTO);

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!_reviewRepository.IsUserReviewOwner(id, userId)) return Forbid();

            if(!_reviewRepository.UpdateReview(review)) return StatusCode(500, "Error while saving item");

            return Ok("Review updated successfully");
        }
   
    }
}
