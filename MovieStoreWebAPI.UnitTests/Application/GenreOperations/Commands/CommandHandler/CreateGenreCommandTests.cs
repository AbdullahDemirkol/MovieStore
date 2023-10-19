using AutoMapper;
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
    public class CreateGenreCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Genre_ShouldBeCreated()
        {
            //Arrange(Hazırlık)
            CreateGenreCommand command = new CreateGenreCommand(_dbContext, _mapper);
            command.Model = new CreateGenreModel()
            {
                Name = "test"
            };

            //Act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var genre = _dbContext.Genres.FirstOrDefault(am => am.Name == "test");
            genre.Should().NotBeNull();
        }
        [Fact]
        public void WhenAlreadyGenreNameIsGiven_InvalidOperationException_ShouldBeError()
        {

            //Arrange(Hazırlık)
            CreateGenreCommand command = new CreateGenreCommand(_dbContext, _mapper);
            command.Model = new CreateGenreModel()
            {
                Name = _dbContext.Genres.FirstOrDefault().Name
            };

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film Türü Bulunmaktadır.");
        }
    }
}
