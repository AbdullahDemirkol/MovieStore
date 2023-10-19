using FluentValidation;
using MovieStoreWebAPI.Application.ActorOperations.Commands.CommandHandler;

namespace MovieStoreWebAPI.Application.ActorOperations.Commands.Validator
{
    public class DeleteActorCommandValidator:AbstractValidator<DeleteActorCommand>
    {
        public DeleteActorCommandValidator()
        {
            RuleFor(command=>command.ActorId).NotEmpty().GreaterThan(0);
        }
    }
}
