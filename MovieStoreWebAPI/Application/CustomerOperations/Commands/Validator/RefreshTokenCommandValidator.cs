using FluentValidation;
using MovieStoreWebAPI.Application.CustomerOperations.Commands.CommandHandler;

namespace MovieStoreWebAPI.Application.CustomerOperations.Commands.Validator
{
    public class RefreshTokenCommandValidator:AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(command => command.RefreshToken).NotEmpty();
        }
    }
}
