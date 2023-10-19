using FluentValidation;
using MovieStoreWebAPI.Application.OrderOperations.Commands.CommandHandler;

namespace MovieStoreWebAPI.Application.OrderOperations.Commands.Validator
{
    public class CreateOrderCommandValidator:AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(command => command.Model.CustomerId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.MovieId).NotEmpty().GreaterThan(0);
        }
    }
}
