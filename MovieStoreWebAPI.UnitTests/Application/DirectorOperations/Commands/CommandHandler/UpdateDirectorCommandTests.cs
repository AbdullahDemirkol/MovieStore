using FluentAssertions;
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
    public class UpdateDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateDirectorCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
        }
        [Fact]
        public void WhenValidInputIsGiven_Director_ShouldBeUpdated()
        {
            //Arrange(Hazırlık)
            UpdateDirectorCommand command = new UpdateDirectorCommand(_dbContext);
            command.DirectorId = 1;
            command.Model = new UpdateDirectorModel()
            {
                Name = "testt",
                Surname = "testt"
            };

            //Act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var director = _dbContext.Directors.FirstOrDefault(am => am.Name == "testt" && am.Surname == "testt");
            director.Should().NotBeNull();
        }
        [Fact]
        public void WhenAlreadyDirectorIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange(Hazırlık)
            var registereddirector = _dbContext.Directors.Last();
            UpdateDirectorCommand command = new UpdateDirectorCommand(_dbContext);
            command.DirectorId = 1;
            command.Model = new UpdateDirectorModel()
            {
                Name = registereddirector.Name,
                Surname = registereddirector.Surname
            };

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yonetmen Isim ve Soyisime Ait Kayıtlı Yönetmen Bulunmaktadır.");
        }
        [Fact]
        public void WhenNonDirectorIdIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange(Hazırlık)
            UpdateDirectorCommand command = new UpdateDirectorCommand(_dbContext);
            command.DirectorId = 999999999;
            command.Model = new UpdateDirectorModel()
            {
                Name = "",
                Surname = ""
            };

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yonetmen Bulunamadı.");
        }
    }
}
