using FluentValidation;
using MovieStoreWebAPI.Application.GenreOperations.Commands.CommandHandler;

namespace MovieStoreWebAPI.Application.GenreOperations.Commands.Validator
{
    public class UpdateGenreCommandValidator:AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command => command.GenreId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.IsActive).NotNull();
        }
    }
}
