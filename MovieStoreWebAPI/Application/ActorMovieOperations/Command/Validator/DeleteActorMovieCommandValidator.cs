using FluentValidation;
using MovieStoreWebAPI.Application.ActorMovieOperations.Command.CommandHandler;

namespace MovieStoreWebAPI.Application.ActorMovieOperations.Command.Validator
{
    public class DeleteActorMovieCommandValidator:AbstractValidator<DeleteActorMovieCommand>
    {
        public DeleteActorMovieCommandValidator()
        {
            RuleFor(command => command.ActorMovieId).NotEmpty().GreaterThan(0);
        }
    }
}
