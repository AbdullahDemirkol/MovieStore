using FluentValidation;
using MovieStoreWebAPI.Application.ActorMovieOperations.Command.CommandHandler;

namespace MovieStoreWebAPI.Application.ActorMovieOperations.Command.Validator
{
    public class UpdateActorMovieCommandValidator:AbstractValidator<UpdateActorMovieCommand>
    {
        public UpdateActorMovieCommandValidator()
        {
            RuleFor(command => command.ActorMovieId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.ActorId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.MovieId).NotEmpty().GreaterThan(0);
        }
    }
}
