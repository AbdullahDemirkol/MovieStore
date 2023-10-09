using FluentValidation;
using MovieStoreWebAPI.Application.CustomerOperations.Commands.CommandHandler;

namespace MovieStoreWebAPI.Application.CustomerOperations.Commands.Validator
{
    public class CreateTokenCommandValidator:AbstractValidator<CreateTokenCommand>
    {
        public CreateTokenCommandValidator()
        {
            RuleFor(command => command.Model.Email).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.Password).NotEmpty().MinimumLength(4);
        }
    }
}
