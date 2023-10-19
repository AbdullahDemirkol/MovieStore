using AutoMapper;
using FluentAssertions;
using MovieStoreWebAPI.Application.ActorMovieOperations.Query.QueryHandler.GetActorMovie;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.UnitTests.TestsSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.ActorMovieOperations.Queries.QueryHandler.GetActorMovie
{
    public class GetActorMovieByIdQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetActorMovieByIdQueryTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_ActorMovie_ShouldBeReturned()
        {
            //Arrange(Hazırlık)
            int actorMovieId = _dbContext.ActorsMovies.FirstOrDefault().Id;
            GetActorMovieByIdQuery query = new GetActorMovieByIdQuery(_dbContext, _mapper);
            query.ActorMovieId = actorMovieId;

            //Act (Çalıştırma)
            var actorMovie = FluentActions.Invoking(() => query.Handle()).Invoke();

            //Assert (Doğrulama)
            var registeredActorMovie = _dbContext.ActorsMovies.FirstOrDefault(am => am.Id == actorMovieId);
            actorMovie.Should().NotBeNull();
            actorMovie.MovieTitle.Should().Be(registeredActorMovie.Movie.Title);
            actorMovie.ActorName.Should().Be(registeredActorMovie.Actor.Name + " " + registeredActorMovie.Actor.Surname);
        }
        [Fact]
        public void WhenActorMovieIdValueIsNotFound_InvalidOperationException_ShouldBeError()
        {

            //Arrange(Hazırlık)
            GetActorMovieByIdQuery query = new GetActorMovieByIdQuery(_dbContext, _mapper);
            query.ActorMovieId = 999999999;

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aktor-Film Iliskisi Bulunamadi.");
        }
    }
}
