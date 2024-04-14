using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using UserMicroservice.Interfaces;
using UserMicroservice.DTO;
using UserMicroservice.Models;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace UserMicroservice.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class OwnedBookController : ControllerBase
    {
        private readonly IOwnedBookRepository _ownedBookRepository;
        private readonly IMapper _mapper;

        public OwnedBookController(IOwnedBookRepository ownedBookRepository, IMapper mapper)
        {
            _ownedBookRepository = ownedBookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OwnedBookDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetOwnedBooks()
        {
            var ownedBooks = _mapper.Map<List<OwnedBookDTO>>(_ownedBookRepository.GetOwnedBooks());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(ownedBooks);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(OwnedBookDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetOwnedBook(int id)
        {
            var ownedBook = _mapper.Map<OwnedBookDTO>(_ownedBookRepository.GetOwnedBook(id));

            if (ownedBook == null) return NotFound();

            return Ok(ownedBook);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateOwnedBook([FromBody] OwnedBookDTO ownedBookDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ownedBookModel = _mapper.Map<OwnedBook>(ownedBookDTO);

            // Set the OwnerId
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ownedBookModel.OwnerId = userId;

            if (!_ownedBookRepository.CreateOwnedBook(ownedBookModel))
            {
                ModelState.AddModelError("", "Creating owned book failed!");
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetOwnedBook", new { id = ownedBookModel.Id }, ownedBookDTO);
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOwnedBook(int id, [FromBody] OwnedBookDTO ownedBookDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var ownedBookModel = _mapper.Map<OwnedBook>(ownedBookDTO);

            // Ensure that the updated owned book has the correct id
            ownedBookModel.Id = id;

            // Check if the user is the owner of the book
            if (!_ownedBookRepository.IsUserOwner(id, userId))
            {
                return Forbid();
            }

            if (!_ownedBookRepository.UpdateOwnedBook(ownedBookModel))
            {
                ModelState.AddModelError("", "Updating owned book failed!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOwnedBook(int id)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Check if the user is the owner of the book
            if (!_ownedBookRepository.IsUserOwner(id, userId))
            {
                return Forbid();
            }

            if (!_ownedBookRepository.OwnedBookExists(id))
            {
                return NotFound();
            }

            if (!_ownedBookRepository.DeleteOwnedBook(id))
            {
                ModelState.AddModelError("", "Deleting owned book failed!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
