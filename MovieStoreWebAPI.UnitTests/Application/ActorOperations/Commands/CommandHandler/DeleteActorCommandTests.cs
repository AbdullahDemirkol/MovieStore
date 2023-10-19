using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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

namespace MovieStoreWebAPI.UnitTests.Application.ActorOperations.Commands.CommandHandler
{
    public class DeleteActorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeleteActorCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Actor_ShouldBeDeleted()
        {
            //Arrange(Hazırlık)
            CreateActorCommand createCommand = new CreateActorCommand(_dbContext, _mapper);
            createCommand.Model = new CreateActorModel()
            {
                Name = "silinicektest",
                Surname = "silinicektest"
            };
            createCommand.Handle();
            var deletedId = _dbContext.Actors.FirstOrDefault(a => a.Name == "silinicektest" & a.Surname == "silinicektest").Id;
            DeleteActorCommand command = new DeleteActorCommand(_dbContext);
            command.ActorId = deletedId;

            //Act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var actor = _dbContext.Actors.FirstOrDefault(am => am.Id == deletedId);
            actor.Should().BeNull();
        }
        [Fact]
        public void WhenNonActorIdIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange(Hazırlık)
            DeleteActorCommand command = new DeleteActorCommand(_dbContext);
            command.ActorId = 999999999;

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aktor Bulunamadi.");
        }
        [Fact]
        public void WhenActorStarringInMovieGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange(Hazırlık)
            DeleteActorCommand command = new DeleteActorCommand(_dbContext);
            command.ActorId = _dbContext.Actors.Include(a => a.Movies).FirstOrDefault(a => a.Movies.Count() > 0).Id;

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aktorun Oynadigi Filmler Bulundugu Icin Silinemedi.");
        }
    }
}
