using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using UserMicroservice.Interfaces;
using UserMicroservice.DTO;
using UserMicroservice.Models;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using UserMicroservice.Repositories;

namespace UserMicroservice.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class BorrowedBookController : ControllerBase
    {
        private readonly IBorrowedBookRepository _borrowedBookRepository;
        private readonly IMapper _mapper;
        private readonly IOwnedBookRepository _ownedBookRepository;
        private readonly IUserRepository _userRepository;

        public BorrowedBookController(IBorrowedBookRepository borrowedBookRepository, IMapper mapper, IOwnedBookRepository ownedBookRepository, IUserRepository userRepository)
        {
            _borrowedBookRepository = borrowedBookRepository;
            _mapper = mapper;
            _ownedBookRepository = ownedBookRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BorrowedBookDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetBorrowedBooks()
        {
            var borrowedBooks = _mapper.Map<List<BorrowedBookDTO>>(_borrowedBookRepository.GetBorrowedBooks());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(borrowedBooks);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BorrowedBookDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetBorrowedBook(int id)
        {
            var borrowedBook = _mapper.Map<BorrowedBookDTO>(_borrowedBookRepository.GetBorrowedBook(id));

            if (borrowedBook == null) return NotFound();

            return Ok(borrowedBook);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateBorrowedBook([FromBody] BorrowedBookDTO borrowedBookDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ownedBook = _ownedBookRepository.GetOwnedBook(borrowedBookDTO.OwnedBookId);

            if (ownedBook == null)
            {
                ModelState.AddModelError("", "OwnedBook not found!");
                return NotFound(ModelState);
            }
            borrowedBookDTO.BookId = ownedBook.BookId;
            borrowedBookDTO.Title = ownedBook.Title;
            borrowedBookDTO.Description = ownedBook.Description;

            var borrowedBookModel = _mapper.Map<BorrowedBook>(borrowedBookDTO);

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            borrowedBookModel.BorrowerId = userId;
            borrowedBookModel.Borrower = _userRepository.GetUser(userId);
            borrowedBookModel.OwnedBook = ownedBook;

            if (!_borrowedBookRepository.BorrowBook(borrowedBookModel))
            {
                ModelState.AddModelError("", "Borrowing book failed!");
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetBorrowedBook", new { id = borrowedBookModel.Id }, borrowedBookDTO);
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBorrowedBook(int id, [FromBody] BorrowedBookDTO borrowedBookDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var borrowedBookModel = _mapper.Map<BorrowedBook>(borrowedBookDTO);

            borrowedBookModel.Id = id;

            if (!_borrowedBookRepository.IsUserBorrower(id, userId))
            {
                return Forbid();
            }

            if (!_borrowedBookRepository.UpdateBorrowedBook(borrowedBookModel))
            {
                ModelState.AddModelError("", "Updating borrowed book failed!");
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
        public IActionResult DeleteBorrowedBook(int id)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Check if the user is the borrower of the book
            if (!_borrowedBookRepository.IsUserBorrower(id, userId))
            {
                return Forbid();
            }

            if (!_borrowedBookRepository.BorrowedBookExists(id))
            {
                return NotFound();
            }

            if (!_borrowedBookRepository.ReturnBook(id))
            {
                ModelState.AddModelError("", "Returning book failed!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


    }
}
