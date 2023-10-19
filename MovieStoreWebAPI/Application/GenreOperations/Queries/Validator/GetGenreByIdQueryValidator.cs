using FluentValidation;
using MovieStoreWebAPI.Application.GenreOperations.Queries.QueryHandler.GetGenre;

namespace MovieStoreWebAPI.Application.GenreOperations.Queries.Validator
{
    public class GetGenreByIdQueryValidator:AbstractValidator<GetGenreByIdQuery>
    {
        public GetGenreByIdQueryValidator()
        {
            RuleFor(query => query.GenreId).NotEmpty().GreaterThan(0);
        }
    }
}
