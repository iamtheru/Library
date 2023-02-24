using Shared.DTO.Incoming;
using Shared.DTO.Outcoming;

namespace Service.Interfaces
{
	public interface IBookService
	{
		public Task<List<BookResponse>> GetAllBooksAsync(string orderBy);

		public Task<BookDetailsResponse> GetBookDetailsAsync(int id);

		public Task<int> DeleteBookAsync(int id);

		public Task<int> AddBookAsync(BookPost model);

		public Task<int> AddReviewAsync(int id, ReviewPut model);

		public Task<int> AddRatingAsync(int id, RatingPut rating);
	}
}