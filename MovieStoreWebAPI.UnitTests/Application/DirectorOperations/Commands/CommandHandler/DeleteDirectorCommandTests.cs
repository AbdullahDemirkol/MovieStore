using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebAPI.Application.ActorOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.ActorOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.Application.DirectorOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.DirectorOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.UnitTests.TestsSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.DirectorOperations.Commands.CommandHandler
{
    public class DeleteDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeleteDirectorCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Director_ShouldBeDeleted()
        {
            //Arrange(Hazırlık)
            CreateDirectorCommand createCommand = new CreateDirectorCommand(_dbContext, _mapper);
            createCommand.Model = new CreateDirectorModel()
            {
                Name = "silinicektest",
                Surname = "silinicektest"
            };
            createCommand.Handle();
            var deletedId = _dbContext.Directors.FirstOrDefault(a => a.Name == "silinicektest" & a.Surname == "silinicektest").Id;
            DeleteDirectorCommand command = new DeleteDirectorCommand(_dbContext);
            command.DirectorId = deletedId;

            //Act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var director = _dbContext.Directors.FirstOrDefault(am => am.Id == deletedId);
            director.Should().BeNull();
        }
        [Fact]
        public void WhenNonDirectorIdIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange(Hazırlık)
            DeleteDirectorCommand command = new DeleteDirectorCommand(_dbContext);
            command.DirectorId = 999999999;

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yonetmen Bulunamadı.");
        }
        [Fact]
        public void WhenActorStarringInMovieGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange(Hazırlık)
            DeleteDirectorCommand command = new DeleteDirectorCommand(_dbContext);
            command.DirectorId = _dbContext.Directors.Include(a => a.Movies).FirstOrDefault(a => a.Movies.Count() > 0).Id;

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yonetmenin Filmleri Bulundugu Icin Silinemedi.");
        }
    }
}
