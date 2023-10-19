using FluentValidation;
using MovieStoreWebAPI.Application.CustomerOperations.Commands.CommandHandler;

namespace MovieStoreWebAPI.Application.CustomerOperations.Commands.Validator
{
    public class DeleteCustomerCommandValidator:AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(command=>command.CustomerId).NotEmpty().GreaterThan(0);
        }
    }
}
