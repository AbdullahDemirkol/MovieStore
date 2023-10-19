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
    public class UpdateActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateActorCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
        }
        [Fact]
        public void WhenValidInputIsGiven_Actor_ShouldBeUpdated()
        {
            //Arrange(Hazırlık)
            UpdateActorCommand command = new UpdateActorCommand(_dbContext);
            command.ActorId = 1;
            command.Model = new UpdateActorModel()
            {
                Name = "testt",
                Surname = "testt"
            };

            //Act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var actor = _dbContext.Actors.FirstOrDefault(am => am.Name == "testt" && am.Surname == "testt");
            actor.Should().NotBeNull();
        }
        [Fact]
        public void WhenAlreadyActorIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange(Hazırlık)
            var registeredActor = _dbContext.Actors.Last();
            UpdateActorCommand command = new UpdateActorCommand(_dbContext);
            command.ActorId = 1;
            command.Model = new UpdateActorModel()
            {
                Name = registeredActor.Name,
                Surname = registeredActor.Surname
            };

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aktor Isim ve Soyisime Ait Kayıtlı Aktor Bulunmaktadır.");
        }
        [Fact]
        public void WhenNonActorIdIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange(Hazırlık)
            UpdateActorCommand command = new UpdateActorCommand(_dbContext);
            command.ActorId = 999999999;
            command.Model = new UpdateActorModel()
            {
                Name = "",
                Surname = ""
            };

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aktor Bulunamadı.");
        }
    }
}
