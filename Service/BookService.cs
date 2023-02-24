using AutoMapper;
using Repository.Interfaces;
using Service.Interfaces;
using Shared.DTO.Incoming;
using Shared.DTO.Outcoming;
using Shared.RepositoryEntities;
using Order = Shared.SortState.BookOrder;

namespace Service
{
	public class BookService : IBookService
	{
		private readonly IBookRepository _repository;
		private readonly IMapper _mapper;

		public BookService(IBookRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<List<BookResponse>> GetAllBooksAsync(string orderBy)
		{
			var sortBy = (Order)Enum.Parse(typeof(Order), orderBy);

			var books = await _repository.GetAllBooksAsync(sortBy);

			return _mapper.Map<List<BookResponse>>(books);
		}

		public async Task<BookDetailsResponse> GetBookDetailsAsync(int id)
		{
			var bookDetails = await _repository.GetBookDetailsAsync(id);

			if (bookDetails == null)
			{
				return new BookDetailsResponse();
			}

			return _mapper.Map(bookDetails, new BookDetailsResponse());
		}

		public async Task<int> DeleteBookAsync(int id)
		{
			return await _repository.DeleteBookAsync(id);
		}

		public async Task<int> AddBookAsync(BookPost model)
		{
			Book book;

			if (model.Id == 0)
			{
				book = new Book();
				await _repository.AddBookAsync(book);
			}
			else
			{
				book = await _repository.FindBookAsync(model.Id);

				if (book == null)
				{
					return -1;
				}
			}

			_mapper.Map(model, book);
			await _repository.SaveAsync();
			return book.Id;
		}

		public async Task<int> AddReviewAsync(int id, ReviewPut model)
		{
			var storedBook = await _repository.FindBookAsync(id);

			if (storedBook == null)
			{
				return -1;
			}

			var review = _mapper.Map<Review>(model);
			review.BookId = id;

			await _repository.AddReviewAsync(review);

			return review.Id;
		}

		public async Task<int> AddRatingAsync(int id, RatingPut model)
		{
			var storedBook = _repository.FindBookAsync(id);

			if (storedBook.Result == null)
			{
				return -1;
			}

			var rating = _mapper.Map<Rating>(model);
			rating.BookId = id;

			await _repository.AddRatingAsync(rating);

			return rating.Id;
		}
	}
}