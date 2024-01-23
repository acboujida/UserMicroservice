using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using UserMicroservice.Interfaces;
using UserMicroservice.DTO;

namespace UserMicroservice.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
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

/*        [HttpGet("{id}/Livre")]
        [ProducesResponseType(200, Type = typeof(LivreDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetLivreOfReview(int id)
        {
            var livre = _mapper.Map<LivreDTO>(_reviewRepository.GetLivreOfReview(id));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_reviewRepository.ReviewExists(id)) return BadRequest(ModelState);

            return Ok(livre);
        }*/
    }
}
