using AutoMapper;
using Shared.DTO.Incoming;
using Shared.DTO.Outcoming;
using Shared.RepositoryEntities;

namespace Library
{
	public class AppMappingProfile : Profile
	{
		public AppMappingProfile()
		{
			CreateMap<ReviewPut, Review>();

			CreateMap<RatingPut, Rating>();

			CreateMap<Review, ReviewResponse>();

			CreateMap<BookPost, Book>()
				.ForMember(b => b.Id, opt => opt.Ignore());
			

			CreateMap<Book, BookResponse>()
				.ForMember(br => br.Rating, opt => opt
				.MapFrom(b => b.Ratings.Average(r => r.Score)))
				.ForMember(br => br.ReviewsNumber, opt => opt
				.MapFrom(b => b.Reviews.Count));

			CreateMap<Book, BookDetailsResponse>()
				.ForMember(bd => bd.Rating, opt => opt
				.MapFrom(b => b.Ratings.Any() ? b.Ratings.Average(r => r.Score) : 0))
				.ForMember(bd => bd.Reviews, opt => opt
				.MapFrom(b => b.Reviews));

			CreateMap<Book, BookResponse>()
				.ForMember(br => br.Rating, opt => opt
				.MapFrom(b => b.Ratings.Any() ? b.Ratings.Average(r => r.Score) : 0))
				.ForMember(br => br.ReviewsNumber, opt => opt
					.MapFrom(br => br.Reviews.Count));
		}
	}
}
