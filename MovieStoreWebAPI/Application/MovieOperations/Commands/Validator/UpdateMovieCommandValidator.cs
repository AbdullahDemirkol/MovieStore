using FluentValidation;
using MovieStoreWebAPI.Application.MovieOperations.Commands.CommandHandler;

namespace MovieStoreWebAPI.Application.MovieOperations.Commands.Validator
{
    public class UpdateMovieCommandValidator:AbstractValidator<UpdateMovieCommand>
    {
        public UpdateMovieCommandValidator()
        {
            RuleFor(command => command.MovieId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.DirectorId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.GenreId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.Price).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.IsActive).NotNull();
        }
    }
}
