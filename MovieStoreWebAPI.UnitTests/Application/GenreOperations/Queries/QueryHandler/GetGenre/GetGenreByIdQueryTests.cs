using AutoMapper;
using FluentAssertions;
using MovieStoreWebAPI.Application.GenreOperations.Queries.QueryHandler.GetGenre;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.UnitTests.TestsSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.GenreOperations.Queries.QueryHandler.GetGenre
{
    public class GetGenreByIdQueryTests : IClassFixture<CommonTestFixture>
    {

        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetGenreByIdQueryTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Genre_ShouldBeReturned()
        {
            //Arrange(Hazırlık)
            int genreId = _dbContext.Genres.FirstOrDefault().Id;
            GetGenreByIdQuery query = new GetGenreByIdQuery(_dbContext, _mapper);
            query.GenreId = genreId;

            //Act (Çalıştırma)
            var genre = FluentActions.Invoking(() => query.Handle()).Invoke();

            //Assert (Doğrulama)
            var registeredGenre = _dbContext.Genres.FirstOrDefault(am => am.Id == genreId);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(registeredGenre.Name);
        }
        [Fact]
        public void WhenGenreIdValueIsNotFound_InvalidOperationException_ShouldBeError()
        {
            //Arrange(Hazırlık)
            GetGenreByIdQuery query = new GetGenreByIdQuery(_dbContext, _mapper);
            query.GenreId = 999999999;

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film Türü Bulunamadı.");
        }
    }
}
