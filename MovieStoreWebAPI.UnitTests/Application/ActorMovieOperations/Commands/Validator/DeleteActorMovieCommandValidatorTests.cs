using FluentAssertions;
using MovieStoreWebAPI.Application.ActorMovieOperations.Command.CommandHandler;
using MovieStoreWebAPI.Application.ActorMovieOperations.Command.RequestCommandModel;
using MovieStoreWebAPI.Application.ActorMovieOperations.Command.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.ActorMovieOperations.Commands.Validator
{
    public class DeleteActorMovieCommandValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int actorMovieId)
        {
            //Arrange(Hazırlık)
            DeleteActorMovieCommand command = new DeleteActorMovieCommand(null);
            command.ActorMovieId = actorMovieId;

            //Act (Çalıştırma)
            DeleteActorMovieCommandValidator validator = new DeleteActorMovieCommandValidator();
            var result = validator.Validate(command);


            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
