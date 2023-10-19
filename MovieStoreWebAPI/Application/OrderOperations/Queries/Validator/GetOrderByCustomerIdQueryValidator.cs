using FluentValidation;
using MovieStoreWebAPI.Application.OrderOperations.Queries.QueryHandler.GetOrders;

namespace MovieStoreWebAPI.Application.OrderOperations.Queries.Validator
{
    public class GetOrderByCustomerIdQueryValidator:AbstractValidator<GetOrdersByCustomerIdQuery>
    {
        public GetOrderByCustomerIdQueryValidator()
        {
            RuleFor(query => query.CustomerId).NotEmpty().GreaterThan(0);
        }
    }
}
