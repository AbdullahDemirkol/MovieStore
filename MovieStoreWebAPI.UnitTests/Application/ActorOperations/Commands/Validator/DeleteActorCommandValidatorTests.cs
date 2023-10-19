using FluentAssertions;
using MovieStoreWebAPI.Application.ActorMovieOperations.Command.CommandHandler;
using MovieStoreWebAPI.Application.ActorMovieOperations.Command.Validator;
using MovieStoreWebAPI.Application.ActorOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.ActorOperations.Commands.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.ActorOperations.Commands.Validator
{
    public class DeleteActorCommandValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int actorId)
        {
            //Arrange(Hazırlık)
            DeleteActorCommand command = new DeleteActorCommand(null);
            command.ActorId = actorId;

            //Act (Çalıştırma)
            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            var result = validator.Validate(command);


            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
