using FluentValidation;
using MovieStoreWebAPI.Application.ActorOperations.Queries.QueryHandler.GetActor;

namespace MovieStoreWebAPI.Application.ActorOperations.Queries.Validator
{
    public class GetActorByIdQueryValidator:AbstractValidator<GetActorByIdQuery>
    {
        public GetActorByIdQueryValidator()
        {
            RuleFor(query=>query.ActorId).NotEmpty().GreaterThan(0);
        }
    }
}
