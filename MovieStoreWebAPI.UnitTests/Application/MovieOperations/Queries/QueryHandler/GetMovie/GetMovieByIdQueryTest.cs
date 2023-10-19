using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebAPI.Application.MovieOperations.Queries.QueryHandler.GetMovie;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.DataAccess.Concrete;
using MovieStoreWebAPI.UnitTests.TestsSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.MovieOperations.Queries.QueryHandler.GetMovie
{
    public class GetMovieByIdQueryTest:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetMovieByIdQueryTest(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenValidMovieIdIsGiven_Movie_ShouldBeReturned()
        {

            //Assert (Doğrulama)
            var movieId = _dbContext.Movies.Include(m => m.Actors).FirstOrDefault(m => m.Actors.Count > 0).Id;
            GetMovieByIdQuery query = new GetMovieByIdQuery(_dbContext, _mapper);
            query.MovieId = movieId;

            //Act (Çalıştırma)
            var movie = FluentActions.Invoking(() => query.Handle()).Invoke();

            //Arrange (Hazırlık)
            var registeredMovie = _dbContext.Movies
                .Include(m => m.Genre)
                .Include(m => m.Director)
                .Include(m => m.Actors).ThenInclude(amr => amr.Actor)
                .FirstOrDefault(m => m.Id == movieId);

            movie.Should().NotBeNull();
            movie.Price.Should().Be(registeredMovie.Price);
            movie.Actors.Count().Should().Be(registeredMovie.Actors.Count());
            movie.Director.Should().Be(registeredMovie.Director.Name + " " + registeredMovie.Director.Surname);
            movie.Genre.Should().Be(registeredMovie.Genre.Name);
            movie.Title.Should().Be(registeredMovie.Title);
            movie.YearOfMovie.Should().Be(registeredMovie.YearOfMovie.Year.ToString());
        }
        [Fact]
        public void WhenInvalidMovieIdIsGiven_Movie_ShouldBeErrorMessageReturned()
        {

            //Assert (Doğrulama)
            GetMovieByIdQuery query = new GetMovieByIdQuery(_dbContext, _mapper);
            query.MovieId = 999999999;

            //Act & Arrange (Çalıştırma-Hazırlık)
            var movie = FluentActions.Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film Bulunamadı.");
        }
    }
}
