using FluentValidation;

namespace WebApi.BookOperations.UpdateBook
{
	public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
	{
		public UpdateBookCommandValidator()
		{
			RuleFor(x=>x.BookId).GreaterThan(0);
			RuleFor(x=>x.Model.Title).NotEmpty().MinimumLength(3);
			RuleFor(x=>x.Model.GenreId).GreaterThanOrEqualTo(0);
		}
	}
}
