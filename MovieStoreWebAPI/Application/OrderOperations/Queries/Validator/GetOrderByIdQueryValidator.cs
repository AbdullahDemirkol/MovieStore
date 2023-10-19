using FluentValidation;
using MovieStoreWebAPI.Application.OrderOperations.Queries.QueryHandler.GetOrder;

namespace MovieStoreWebAPI.Application.OrderOperations.Queries.Validator
{
    public class GetOrderByIdQueryValidator:AbstractValidator<GetOrderByIdQuery>
    {
        public GetOrderByIdQueryValidator()
        {
            RuleFor(query => query.OrderId).NotEmpty().GreaterThan(0);
        }
    }
}
