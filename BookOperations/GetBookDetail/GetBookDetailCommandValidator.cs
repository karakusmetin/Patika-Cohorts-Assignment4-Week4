using FluentValidation;

namespace WebApi.BookOperations.GetBookDetail
{
	public class GetBookDetailCommandValidator : AbstractValidator<GetBookDetailQuery>
	{
		public GetBookDetailCommandValidator()
		{
			RuleFor(x=>x.BookID).GreaterThan(0);
		}
	}
}
