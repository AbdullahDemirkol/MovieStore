using FluentValidation;
using MovieStoreWebAPI.Application.GenreOperations.Commands.CommandHandler;

namespace MovieStoreWebAPI.Application.GenreOperations.Commands.Validator
{
    public class CreateGenreCommandValidator:AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(command=>command.Model.Name).NotEmpty().MinimumLength(4);
        }
    }
}
