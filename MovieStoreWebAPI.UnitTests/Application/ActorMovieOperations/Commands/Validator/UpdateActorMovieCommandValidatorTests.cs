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
    public class UpdateActorMovieCommandValidatorTests
    {
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 0, 0)]
        [InlineData(0, 1, 0)]
        [InlineData(0, 0, 1)]
        [InlineData(1, 1, 0)]
        [InlineData(1, 0, 1)]
        [InlineData(0, 1, 1)]
        [InlineData(-1, 1, 1)]
        [InlineData(1, -1, 1)]
        [InlineData(1, 1, -1)]
        [InlineData(-1, -1, 1)]
        [InlineData(-1, 1, -1)]
        [InlineData(1, -1, -1)]
        [InlineData(-1, -1, -1)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int actorMovieId, int actorId, int movieId)
        {
            //Arrange(Hazırlık)
            UpdateActorMovieCommand command = new UpdateActorMovieCommand(null);
            command.ActorMovieId = actorMovieId;
            command.Model = new UpdateActorMovieModel()
            {
                ActorId = actorId,
                MovieId = movieId
            };

            //Act (Çalıştırma)
            UpdateActorMovieCommandValidator validator = new UpdateActorMovieCommandValidator();
            var result = validator.Validate(command);


            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
