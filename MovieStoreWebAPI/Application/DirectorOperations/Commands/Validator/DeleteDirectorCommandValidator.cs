using FluentValidation;
using MovieStoreWebAPI.Application.DirectorOperations.Commands.CommandHandler;

namespace MovieStoreWebAPI.Application.DirectorOperations.Commands.Validator
{
    public class DeleteDirectorCommandValidator:AbstractValidator<DeleteDirectorCommand>
    {
        public DeleteDirectorCommandValidator()
        {
            RuleFor(command=>command.DirectorId).NotEmpty().GreaterThan(0);
        }
    }
}
