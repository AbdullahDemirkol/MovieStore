using AutoMapper;
using FluentAssertions;
using MovieStoreWebAPI.Application.MovieOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.MovieOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.UnitTests.TestsSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.MovieOperations.Commands.CommandHandler
{
    public class UpdateMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateMovieCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
        }
        [Fact]
        public void WhenValidInputIsGiven_Update_ShouldBeUpdated()
        {
            //Arrange(Hazırlık)
            var movieId = _dbContext.Movies.LastOrDefault().Id;
            UpdateMovieCommand command = new UpdateMovieCommand(_dbContext);
            command.MovieId = movieId;
            command.Model = new UpdateMovieModel()
            {
                Price = 150,
                Title = "testt",
                GenreId = _dbContext.Genres.First().Id,
                DirectorId = _dbContext.Directors.First().Id,
                IsActive=true
            };

            //Act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var moive = _dbContext.Movies.FirstOrDefault(am => am.Title == "testt");
            moive.Should().NotBeNull();
        }
        [Fact]
        public void WhenAlreadyMovieIsGiven_InvalidOperationException_ShouldBeError()
        {

            //Arrange(Hazırlık)
            var movie = _dbContext.Movies.FirstOrDefault();
            UpdateMovieCommand command = new UpdateMovieCommand(_dbContext);
            command.MovieId = movie.Id;
            command.Model = new UpdateMovieModel()
            {
                Price = 150,
                Title = movie.Title,
                GenreId = movie.GenreId,
                DirectorId = movie.DirectorId,
                IsActive=true
            };

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bu İsimde Film Bulunmaktadır.");
        }
        [Fact]
        public void WhenNonGenreIdIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange(Hazırlık)
            var movieId = _dbContext.Movies.LastOrDefault().Id;
            UpdateMovieCommand command = new UpdateMovieCommand(_dbContext);
            command.MovieId = movieId;
            command.Model = new UpdateMovieModel()
            {
                Price = 150,
                Title = "test",
                GenreId = 999999999,
                DirectorId = _dbContext.Directors.First().Id,
                IsActive = true
            };

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film Turu Bulunamadi.");
        }
        [Fact]
        public void WhenNonMovieIdIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange(Hazırlık)
            UpdateMovieCommand command = new UpdateMovieCommand(_dbContext);
            command.MovieId = 999999999;
            command.Model = new UpdateMovieModel()
            {
                Price = 150,
                Title = "test",
                GenreId = _dbContext.Genres.First().Id,
                DirectorId = _dbContext.Directors.First().Id,
                IsActive = true
            };

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film Bulunamadi.");
        }
        [Fact]
        public void WhenNonDirectorIdIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange(Hazırlık)
            var movieId = _dbContext.Movies.LastOrDefault().Id;
            UpdateMovieCommand command = new UpdateMovieCommand(_dbContext);
            command.MovieId = movieId;
            command.Model = new UpdateMovieModel()
            {
                Price = 150,
                Title = "test",
                GenreId = _dbContext.Genres.First().Id,
                DirectorId = 999999999,
                IsActive=true
            };

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yonetmen Bulunamadi.");
        }
    }
}
