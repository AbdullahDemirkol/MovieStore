using FluentValidation;
using MovieStoreWebAPI.Application.DirectorOperations.Commands.CommandHandler;

namespace MovieStoreWebAPI.Application.DirectorOperations.Commands.Validator
{
    public class CreateDirectorCommandValidator:AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.Surname).NotEmpty().MinimumLength(4);
        }
    }
}
