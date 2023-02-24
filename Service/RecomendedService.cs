using AutoMapper;
using Repository.Interfaces;
using Service.Interfaces;
using Shared.DTO.Outcoming;

namespace Service
{
	public class RecomendedService : IRecomendedService
	{
		private readonly IBookRepository _repository;
		private readonly IMapper _mapper;

		public RecomendedService(IBookRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<List<BookResponse>> GetRecommendedBooksAsync(string genre, int bookCount = 10, int reviewCount = 10)
		{
			var books = await _repository.GetRecommendedBooksAsync(genre, bookCount, reviewCount);

			return books.Select(b => _mapper.Map(b, new BookResponse())).ToList();
		}
	}
}
