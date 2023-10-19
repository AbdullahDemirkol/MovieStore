using FluentValidation;
using MovieStoreWebAPI.Application.ActorOperations.Commands.CommandHandler;

namespace MovieStoreWebAPI.Application.ActorOperations.Commands.Validator
{
    public class UpdateActorCommandValidator:AbstractValidator<UpdateActorCommand>
    {
        public UpdateActorCommandValidator()
        {
            RuleFor(command=>command.ActorId).NotNull().GreaterThan(0);
            RuleFor(command => command.Model.Name).NotNull().MinimumLength(4);
            RuleFor(command => command.Model.Surname).NotNull().MinimumLength(4);
        }
    }
}
