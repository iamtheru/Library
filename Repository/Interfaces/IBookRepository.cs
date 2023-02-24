using Shared.RepositoryEntities;
using Order = Shared.SortState.BookOrder;

namespace Repository.Interfaces
{
	public interface IBookRepository : IInmemoryDatabase
	{
		public Task<List<Book>> GetAllBooksAsync(Order order);

		public Task<List<Book>> GetRecommendedBooksAsync(string genre, int bookCount, int reviewCount);

		public Task<Book> GetBookDetailsAsync(int id);

		public Task<int> DeleteBookAsync(int id);

		public Task<Book> FindBookAsync(int id);

		public Task<int> AddBookAsync(Book book);

		public Task<int> UpdateBook(Book book);

		public Task<int> AddRatingAsync(Rating rating);

		public Task<int> AddReviewAsync(Review review);
	}
}
