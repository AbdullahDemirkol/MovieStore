using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebAPI.Application.ActorMovieOperations.Command.CommandHandler;
using MovieStoreWebAPI.Application.ActorMovieOperations.Command.RequestCommandModel;
using MovieStoreWebAPI.Application.ActorOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.ActorOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.UnitTests.TestsSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieStoreWebAPI.UnitTests.Application.ActorMovieOperations.Commands.CommandHandler
{
    public class CreateActorMovieCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateActorMovieCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_ActorMovie_ShouldBeCreated()
        {
            //Arrange(Hazırlık)
            CreateActorCommand createActorCommand = new CreateActorCommand(_dbContext, _mapper);
            createActorCommand.Model = new CreateActorModel()
            {
                Name = "actor",
                Surname = "actor"
            };
            createActorCommand.Handle();
            var actorId = _dbContext.Actors.FirstOrDefault(a => a.Name == "actor" & a.Surname == "actor").Id;
            CreateActorMovieCommand command = new CreateActorMovieCommand(_dbContext, _mapper);
            command.Model = new CreateActorMovieModel()
            {
                ActorId = actorId,
                MovieId = 5
            };

            //Act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var actorMovie = _dbContext.ActorsMovies.FirstOrDefault(am => am.ActorId == actorId && am.MovieId == 5);
            actorMovie.Should().NotBeNull();
        }
        [Fact]
        public void WhenAlreadyActorMovieRelationshipIsGiven_InvalidOperationException_ShouldBeError()
        {

            //Arrange(Hazırlık)
            var actorMovie = _dbContext.ActorsMovies.FirstOrDefault();
            CreateActorMovieCommand command = new CreateActorMovieCommand(_dbContext, _mapper);
            command.Model = new CreateActorMovieModel()
            {
                ActorId = actorMovie.ActorId,
                MovieId = actorMovie.MovieId
            };
            
            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aktor-Film Iliskisi Bulunmaktadır.");
        }
    }
}
