using AutoMapper;
using FluentAssertions;
using MovieStoreWebAPI.Application.ActorMovieOperations.Command.CommandHandler;
using MovieStoreWebAPI.Application.ActorMovieOperations.Command.RequestCommandModel;
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
    public class DeleteActorMovieCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;

        public DeleteActorMovieCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
        }
        [Fact]
        public void WhenValidInputIsGiven_ActorMovie_ShouldBeDeleted()
        {
            //Arrange(Hazırlık)
            var actorMovieId = _dbContext.ActorsMovies.Last().Id;
            DeleteActorMovieCommand command = new DeleteActorMovieCommand(_dbContext);
            command.ActorMovieId = actorMovieId;

            //Act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var actorMovie = _dbContext.ActorsMovies.FirstOrDefault(am => am.Id == actorMovieId);
            actorMovie.Should().BeNull();
        }
        [Fact]
        public void WhenActorMovieIdValueIsNotFound_InvalidOperationException_ShouldBeError()
        {

            //Arrange(Hazırlık)
            DeleteActorMovieCommand command = new DeleteActorMovieCommand(_dbContext);
            command.ActorMovieId = 999999999;

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aktor-Film Iliskisi Bulunamadi.");
        }
    }
}
