using AutoMapper;
using FluentAssertions;
using MovieStoreWebAPI.Application.GenreOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.GenreOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.Application.MovieOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.MovieOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.Entity.Concrete;
using MovieStoreWebAPI.UnitTests.TestsSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.MovieOperations.Commands.CommandHandler
{
    public class CreateMovieCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateMovieCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Movie_ShouldBeCreated()
        {
            //Arrange(Hazırlık)
            CreateMovieCommand command = new CreateMovieCommand(_dbContext, _mapper);
            command.Model = new CreateMovieModel()
            {
                Price = 150,
                Title = "test",
                YearOfMovie = new DateTime(1990, 12, 01),
                GenreId = _dbContext.Genres.First().Id,
                DirectorId = _dbContext.Directors.First().Id
            };

            //Act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var moive = _dbContext.Movies.FirstOrDefault(am => am.Title == "test");
            moive.Should().NotBeNull();
        }
        [Fact]
        public void WhenAlreadyMovieIsGiven_InvalidOperationException_ShouldBeError()
        {

            //Arrange(Hazırlık)
            var movie=_dbContext.Movies.FirstOrDefault();
            CreateMovieCommand command = new CreateMovieCommand(_dbContext, _mapper);
            command.Model = new CreateMovieModel()
            {
                Price = 150,
                Title = movie.Title,
                YearOfMovie = new DateTime(1991, 12, 01),
                GenreId = movie.GenreId,
                DirectorId = movie.DirectorId
            };

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bu Isim Ve Yönetmene Ait Film Bulunmaktadır.");
        }
        [Fact]
        public void WhenGenreOfMovieThatIsNotAvailableGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange(Hazırlık)
            CreateMovieCommand command = new CreateMovieCommand(_dbContext, _mapper);
            command.Model = new CreateMovieModel()
            {
                Price = 150,
                Title = "test",
                YearOfMovie = new DateTime(1990, 12, 01),
                GenreId = 999999999,
                DirectorId = _dbContext.Directors.First().Id
            };

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Gecerli Bir Film Turu Bulunamadi.");
        }
    }
}
