using FluentValidation;
using MovieStoreWebAPI.Application.DirectorOperations.Queries.QueryHandler.GetDirector;

namespace MovieStoreWebAPI.Application.DirectorOperations.Queries.Validator
{
    public class GetDirectorByIdQueryValidator:AbstractValidator<GetDirectorByIdQuery>
    {
        public GetDirectorByIdQueryValidator()
        {
            RuleFor(query => query.DirectorId).NotEmpty().GreaterThan(0);
        }
    }
}
