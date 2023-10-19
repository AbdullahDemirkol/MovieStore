using FluentAssertions;
using MovieStoreWebAPI.Application.GenreOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.GenreOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.UnitTests.TestsSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.GenreOperations.Commands.CommandHandler
{
    public class UpdateGenreCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
        }
        [Fact]
        public void WhenValidInputIsGiven_Genre_ShouldBeUpdated()
        {
            //Arrange(Hazırlık)
            UpdateGenreCommand command = new UpdateGenreCommand(_dbContext);
            command.GenreId = 1;
            command.Model = new UpdateGenreModel()
            {
                Name = "testt",
                IsActive = true
            };

            //Act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var actor = _dbContext.Genres.FirstOrDefault(am => am.Name == "testt" );
            actor.Should().NotBeNull();
        }
        [Fact]
        public void WhenAlreadyGenreIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange(Hazırlık)
            var registeredGenre = _dbContext.Genres.Last();
            UpdateGenreCommand command = new UpdateGenreCommand(_dbContext);
            command.GenreId = _dbContext.Genres.FirstOrDefault(g=>g.Id!=registeredGenre.Id).Id;
            command.Model = new UpdateGenreModel()
            {
                Name = registeredGenre.Name,
                IsActive = true
            };

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Verilen Film Turu Ismi Baska Bir Film Türünde Bulunmaktadır.");
        }
        [Fact]
        public void WhenNonGenreIdIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange(Hazırlık)
            UpdateGenreCommand command = new UpdateGenreCommand(_dbContext);
            command.GenreId = 999999999;
            command.Model = new UpdateGenreModel()
            {
                Name = "",
                IsActive=true
            };

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film Turu Bulunamadı.");
        }
        [Fact]
        public void WhenMovieInTheGenreIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange(Hazırlık)
            UpdateGenreCommand command = new UpdateGenreCommand(_dbContext);
            command.GenreId = _dbContext.Movies.FirstOrDefault().GenreId;
            command.Model = new UpdateGenreModel()
            {
                Name = "testtt",
                IsActive = false
            };

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film Turune Ait Film Bulundugu Icin Statusu Pasif Yapılamaz.");
        }
    }
}
