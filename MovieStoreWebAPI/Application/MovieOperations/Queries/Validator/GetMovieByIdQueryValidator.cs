using FluentValidation;
using MovieStoreWebAPI.Application.MovieOperations.Queries.QueryHandler.GetMovie;

namespace MovieStoreWebAPI.Application.MovieOperations.Queries.Validator
{
    public class GetMovieByIdQueryValidator:AbstractValidator<GetMovieByIdQuery>
    {
        public GetMovieByIdQueryValidator()
        {
            RuleFor(query => query.MovieId).NotNull().GreaterThan(0);

        }
    }
}
