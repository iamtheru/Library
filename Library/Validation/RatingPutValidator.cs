using FluentValidation;
using Shared.DTO.Incoming;

namespace Library.Validation
{
	public class RatingPutValidator : AbstractValidator<RatingPut>
	{
		public RatingPutValidator()
		{
			RuleFor(r => r.Score)
				.InclusiveBetween(1, 5).WithMessage("Score must be between 1 and 5.");
		}
	}
}
