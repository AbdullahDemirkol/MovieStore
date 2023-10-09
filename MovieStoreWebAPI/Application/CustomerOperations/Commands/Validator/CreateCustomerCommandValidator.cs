using FluentValidation;
using MovieStoreWebAPI.Application.CustomerOperations.Commands.CommandHandler;

namespace MovieStoreWebAPI.Application.CustomerOperations.Commands.Validator
{
    public class CreateCustomerCommandValidator:AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.Surname).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.Password).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.Email).NotEmpty().MinimumLength(4);
        }
    }
}
