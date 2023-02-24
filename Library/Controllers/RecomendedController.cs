using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Library.Controllers
{
	[ApiController]
	[Route("api/")]
	public class RecomendedController : Controller
	{
		private readonly IRecomendedService _service;

		public RecomendedController(IRecomendedService service )
		{
			_service = service;
		}
		
		[HttpGet("recommended")]
		public async Task<IActionResult> GetRecommendedBooks(string genre)
		{
			var recommendedBooks = await _service.GetRecommendedBooksAsync(genre);

			return Ok(recommendedBooks);
		}
	}
}
