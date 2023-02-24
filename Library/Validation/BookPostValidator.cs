using FluentValidation;
using Shared.DTO.Incoming;

namespace Library.Validation
{
	public class BookPostValidator : AbstractValidator<BookPost>
	{
		public BookPostValidator()
		{
			RuleFor(b => b.Title).NotEmpty().WithMessage("Title cannot be empty.")
				.MinimumLength(1).WithMessage("Title cannot be less than 1 characters.")
				.MaximumLength(100).WithMessage("Title cannot be longer than 100 characters.");

			RuleFor(b => b.Cover)
				.NotEmpty().WithMessage("Cover cannot be empty.");

			RuleFor(b => b.Content)
				.NotEmpty().WithMessage("Content cannot be empty.")
				.MaximumLength(500).WithMessage("Content cannot be longer than 500 characters.");

			RuleFor(b => b.Genre)
				.NotEmpty().WithMessage("Genre cannot be empty.")
				.MaximumLength(50).WithMessage("Genre cannot be longer than 50 characters.");

			RuleFor(b => b.Author)
				.NotEmpty().WithMessage("Author cannot be empty.")
				.MinimumLength(4).WithMessage("Message cannot be less than 4 characters.")
				.MaximumLength(100).WithMessage("Author cannot be longer than 100 characters.");
		}
	}
}
