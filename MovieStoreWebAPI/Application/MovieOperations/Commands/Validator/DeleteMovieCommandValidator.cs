using FluentValidation;
using MovieStoreWebAPI.Application.MovieOperations.Commands.CommandHandler;

namespace MovieStoreWebAPI.Application.MovieOperations.Commands.Validator
{
    public class DeleteMovieCommandValidator:AbstractValidator<DeleteMovieCommand>
    {
        public DeleteMovieCommandValidator()
        {
            RuleFor(command=>command.MovieId).NotEmpty().GreaterThan(0);
        }
    }
}
