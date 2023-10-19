using FluentValidation;
using MovieStoreWebAPI.Application.ActorMovieOperations.Query.QueryHandler.GetActorMovie;

namespace MovieStoreWebAPI.Application.ActorMovieOperations.Query.Validator
{
    public class GetActorMovieByIdQueryValidator:AbstractValidator<GetActorMovieByIdQuery>
    {
        public GetActorMovieByIdQueryValidator()
        {
            RuleFor(query => query.ActorMovieId).NotEmpty().GreaterThan(0);
        }
    }
}
