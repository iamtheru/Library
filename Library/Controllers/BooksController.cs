using Library.Configuration;
using Library.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service.Interfaces;
using Shared;
using Shared.DTO.Incoming;
using Shared.DTO.Outcoming;
using System.ComponentModel.DataAnnotations;

namespace Library.Controllers
{
	[ApiController]
	[Route("api/books")]
	public class BooksController : ControllerBase
	{
		private readonly IBookService _bookService;
		private readonly Config _configuration;


		public BooksController(IBookService bookService, IOptions<Config> configuration)
		{
			_bookService = bookService;
			_configuration = configuration.Value;
		}

		[HttpGet()]
		[GetAllBooksFilterAttribute(typeof(SortState.BookOrder))]
		public async Task<ActionResult<IEnumerable<BookResponse>>> GetAllBooks([FromQuery] string order)
		{
			var books = await _bookService.GetAllBooksAsync(order);

			return Ok(books);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetBookDetails([Required] int id)
		{
			var bookDetails = await _bookService.GetBookDetailsAsync(id);

			if (bookDetails.Id == -1)
			{
				return NotFound();
			}

			return Ok(bookDetails);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBook([Required] int id, [FromQuery] string secret)
		{
			if (secret != _configuration.SecretKey)
			{
				return BadRequest();
			}

			var result = await _bookService.DeleteBookAsync(id);

			if (result == -1)
			{
				return NotFound();
			}

			return Ok();
		}

		[HttpPost("save")]
		public async Task<IActionResult> SaveBook([FromBody] BookPost book)
		{
			var storedBookId = await _bookService.AddBookAsync(book);

			if (storedBookId == -1)
			{
				return NotFound();
			}

			return Ok(new BookCreated { Id = storedBookId });
		}

		[HttpPut("{id}/review")]
		public async Task<ActionResult<int>> SaveBookReview([Required] int id, [FromBody] ReviewPut review)
		{
			var reviewId = await _bookService.AddReviewAsync(id, review);

			if (reviewId == -1)
			{
				return NotFound();
			}

			return Ok(new ReviewCreated { Id = reviewId });
		}

		[HttpPut("{id}/rate")]
		public async Task<ActionResult<int>> RateBook([Required] int id, [FromBody] RatingPut ratingModel)
		{
			var ratingId = await _bookService.AddRatingAsync(id, ratingModel);

			if (ratingId == -1)
			{
				return NotFound();
			}

			return Ok(new RatingCreated { Id = ratingId });
		}
	}
}
