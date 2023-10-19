using FluentValidation;
using MovieStoreWebAPI.Application.CustomerOperations.Queries.QueryHandler.GetCustomer;

namespace MovieStoreWebAPI.Application.CustomerOperations.Queries.Validator
{
    public class GetCustomerByIdQueryValidator:AbstractValidator<GetCustomerByIdQuery>
    {
        public GetCustomerByIdQueryValidator()
        {
            RuleFor(query => query.CustomerId).NotEmpty().GreaterThan(0);
        }
    }
}
