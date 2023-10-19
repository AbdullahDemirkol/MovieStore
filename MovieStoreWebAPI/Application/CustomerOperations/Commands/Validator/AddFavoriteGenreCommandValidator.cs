using FluentValidation;
using MovieStoreWebAPI.Application.CustomerOperations.Commands.CommandHandler;

namespace MovieStoreWebAPI.Application.CustomerOperations.Commands.Validator
{
    public class AddFavoriteGenreCommandValidator:AbstractValidator<AddFavoriteGenreCommand>
    {
        public AddFavoriteGenreCommandValidator()
        {
            RuleFor(command => command.CustomerId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.GenreId).NotEmpty().GreaterThan(0);
        }
    }
}
