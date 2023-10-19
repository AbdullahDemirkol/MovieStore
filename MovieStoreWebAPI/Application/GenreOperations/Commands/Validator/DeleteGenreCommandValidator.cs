using FluentValidation;
using MovieStoreWebAPI.Application.GenreOperations.Commands.CommandHandler;

namespace MovieStoreWebAPI.Application.GenreOperations.Commands.Validator
{
    public class DeleteGenreCommandValidator:AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(command => command.GenreId).NotEmpty().GreaterThan(0);
        }
    }
}
