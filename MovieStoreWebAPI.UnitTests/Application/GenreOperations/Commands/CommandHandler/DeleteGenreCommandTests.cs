using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
    public class DeleteGenreCommandTests:IClassFixture<CommonTestFixture>
    {

        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Genre_ShouldBeDeleted()
        {
            //Arrange(Hazırlık)
            CreateGenreCommand createCommand = new CreateGenreCommand(_dbContext, _mapper);
            createCommand.Model = new CreateGenreModel()
            {
                Name = "silinicektest"
            };
            createCommand.Handle();
            var deletedId = _dbContext.Genres.FirstOrDefault(a => a.Name == "silinicektest").Id;
            DeleteGenreCommand command = new DeleteGenreCommand(_dbContext);
            command.GenreId = deletedId;

            //Act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var genre = _dbContext.Genres.FirstOrDefault(am => am.Id == deletedId);
            genre.Should().BeNull();
        }
        [Fact]
        public void WhenNonGenreIdIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange(Hazırlık)
            DeleteGenreCommand command = new DeleteGenreCommand(_dbContext);
            command.GenreId = 999999999;

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film Turu Bulunamadi.");
        }
        [Fact]
        public void WhenMovieOfTheGenreIsFound_InvalidOperationException_ShouldBeError()
        {
            //Arrange(Hazırlık)
            DeleteGenreCommand command = new DeleteGenreCommand(_dbContext);
            command.GenreId = _dbContext.Genres.FirstOrDefault(g=>_dbContext.Movies.Any(m=>m.GenreId!=g.Id)).Id;

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film Turu Ait Filmler Bulundugu Icin Film Turu Silinemedi.");
        }
    }
}
