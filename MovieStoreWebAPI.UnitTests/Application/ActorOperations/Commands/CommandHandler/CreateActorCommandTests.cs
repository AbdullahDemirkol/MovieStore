using AutoMapper;
using FluentAssertions;
using MovieStoreWebAPI.Application.ActorOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.ActorOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.UnitTests.TestsSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.ActorOperations.Commands.CommandHandler
{
    public class CreateActorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateActorCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Actor_ShouldBeCreated()
        {
            //Arrange(Hazırlık)
            CreateActorCommand command = new CreateActorCommand(_dbContext, _mapper);
            command.Model = new CreateActorModel()
            {
                Name="test",
                Surname="test"
            };

            //Act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var actor = _dbContext.Actors.FirstOrDefault(am => am.Name == "test" && am.Surname == "test");
            actor.Should().NotBeNull();
        }
        [Fact]
        public void WhenAlreadyActorIsGiven_InvalidOperationException_ShouldBeError()
        {

            //Arrange(Hazırlık)
            CreateActorCommand command = new CreateActorCommand(_dbContext, _mapper);
            command.Model = new CreateActorModel()
            {
                Name = "Matthew",
                Surname = "McConaughey"
            };

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aktor Bulunmaktadır.");
        }
    }
}
