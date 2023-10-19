using AutoMapper;
using FluentAssertions;
using MovieStoreWebAPI.Application.GenreOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.GenreOperations.Commands.RequestCommandModel;
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
    public class DeleteMovieCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeleteMovieCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Movie_ShouldBeDeleted()
        {
            //Arrange(Hazırlık)
            CreateMovieCommand createCommand = new CreateMovieCommand(_dbContext, _mapper);
            createCommand.Model = new CreateMovieModel()
            {
                Title = "silinicektest",
                Price = 100,
                GenreId = _dbContext.Genres.FirstOrDefault().Id,
                DirectorId = _dbContext.Directors.FirstOrDefault().Id,
                YearOfMovie = new DateTime(1990, 10, 01)
            };
            createCommand.Handle();
            var deletedId = _dbContext.Movies.FirstOrDefault(a => a.Title == "silinicektest").Id;
            DeleteMovieCommand command = new DeleteMovieCommand(_dbContext);
            command.MovieId = deletedId;

            //Act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var movie = _dbContext.Movies.FirstOrDefault(am => am.Id == deletedId);
            movie.IsActive.Should().BeFalse();
        }
        [Fact]
        public void WhenNonMovieIdIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange(Hazırlık)
            DeleteMovieCommand command = new DeleteMovieCommand(_dbContext);
            command.MovieId = 999999999;

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film Bulunamadi.");
        }
    }
}
