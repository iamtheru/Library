using Shared.DTO.Outcoming;

namespace Service.Interfaces
{
	public interface IRecomendedService
	{
		public Task<List<BookResponse>> GetRecommendedBooksAsync(string genre , int booksCountq = 10, int reviewrsCount = 10);
	}
}
