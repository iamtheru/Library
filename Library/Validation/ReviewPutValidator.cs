using FluentValidation;
using Shared.DTO.Incoming;

namespace Library.Validation
{
	public class ReviewPutValidator : AbstractValidator<ReviewPut>
	{
		public ReviewPutValidator()
		{
			RuleFor(r => r.Message)
				.NotEmpty().WithMessage("Message cannot be empty.")
				.MinimumLength(5).WithMessage("Message cannot be less than 5 characters.")
				.MaximumLength(500).WithMessage("Message cannot be longer than 500 characters.");

			RuleFor(r => r.Reviewer)
				.NotEmpty().WithMessage("Reviewer cannot be empty.")
				.MinimumLength(4).WithMessage("Message cannot be less than 4 characters.")
				.MaximumLength(100).WithMessage("Reviewer cannot be longer than 100 characters.");
		}
	}
}
