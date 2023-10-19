using FluentValidation;
using MovieStoreWebAPI.Application.ActorOperations.Commands.CommandHandler;

namespace MovieStoreWebAPI.Application.ActorOperations.Commands.Validator
{
    public class CreateActorCommandValidator:AbstractValidator<CreateActorCommand>
    {
        public CreateActorCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.Surname).NotEmpty().MinimumLength(4);
        }
    }
}
