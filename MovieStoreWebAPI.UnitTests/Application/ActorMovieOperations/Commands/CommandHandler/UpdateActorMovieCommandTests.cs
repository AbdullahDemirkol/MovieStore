using AutoMapper;
using FluentAssertions;
using MovieStoreWebAPI.Application.ActorMovieOperations.Command.CommandHandler;
using MovieStoreWebAPI.Application.ActorMovieOperations.Command.RequestCommandModel;
using MovieStoreWebAPI.Application.ActorOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.ActorOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.Entity.Concrete;
using MovieStoreWebAPI.UnitTests.TestsSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.ActorMovieOperations.Commands.CommandHandler
{
    public class UpdateActorMovieCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateActorMovieCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_ActorMovie_ShouldBeUpdated()
        {
            //Arrange(Hazırlık)
            CreateActorCommand createActorCommand = new CreateActorCommand(_dbContext, _mapper);
            createActorCommand.Model = new CreateActorModel()
            {
                Name = "asdasd",
                Surname = "asdasd"
            };
            createActorCommand.Handle();
            int actorId = _dbContext.Actors.FirstOrDefault(am => am.Name == "asdasd" && am.Surname == "asdasd").Id;

            UpdateActorMovieCommand command = new UpdateActorMovieCommand(_dbContext);
            command.ActorMovieId = 1;
            command.Model = new UpdateActorMovieModel()
            {
                ActorId = actorId,
                MovieId = 1
            };

            //Act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var actorMovie = _dbContext.ActorsMovies.FirstOrDefault(am => am.ActorId == actorId && am.MovieId == 1);
            actorMovie.Should().NotBeNull();
        }
        [Theory]
        [InlineData(999999999, 1, 1)]
        [InlineData(1, 999999999, 1)]
        [InlineData(1, 1, 999999999)]
        public void WhenThereAreNoGivenValues_InvalidOperationException_ShouldBeErrors(int actorMovieId,int actorId, int movieId)
        {
            //Arrange(Hazırlık)
            UpdateActorMovieCommand command = new UpdateActorMovieCommand(_dbContext);
            command.ActorMovieId=actorMovieId;
            command.Model = new UpdateActorMovieModel()
            {
                ActorId = actorId,
                MovieId = movieId
            };

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>();
        }
        [Fact]
        public void WhenAlreadyActorMovieRelationshipIsGiven_InvalidOperationException_ShouldBeError()
        {

            //Arrange(Hazırlık)
            var actorMovie = _dbContext.ActorsMovies.FirstOrDefault();
            UpdateActorMovieCommand command = new UpdateActorMovieCommand(_dbContext);
            command.ActorMovieId = _dbContext.ActorsMovies.FirstOrDefault(am => am.Id != actorMovie.Id).Id;
            command.Model = new UpdateActorMovieModel()
            {
                ActorId = actorMovie.ActorId,
                MovieId = actorMovie.MovieId
            };

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Zaten Aktor-Film Iliskisi Bulunmaktadir.");
        }
    }
}
