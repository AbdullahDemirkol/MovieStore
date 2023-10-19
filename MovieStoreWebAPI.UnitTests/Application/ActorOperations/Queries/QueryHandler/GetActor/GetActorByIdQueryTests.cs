using AutoMapper;
using FluentAssertions;
using MovieStoreWebAPI.Application.ActorOperations.Queries.QueryHandler.GetActor;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.UnitTests.TestsSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.ActorOperations.Queries.QueryHandler.GetActor
{
    public class GetActorByIdQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetActorByIdQueryTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Actor_ShouldBeReturned()
        {
            //Arrange(Hazırlık)
            int actorId = _dbContext.Actors.FirstOrDefault().Id;
            GetActorByIdQuery query = new GetActorByIdQuery(_dbContext, _mapper);
            query.ActorId = actorId;

            //Act (Çalıştırma)
            var actor = FluentActions.Invoking(() => query.Handle()).Invoke();

            //Assert (Doğrulama)
            var registeredActor = _dbContext.Actors.FirstOrDefault(am => am.Id == actorId);
            actor.Should().NotBeNull();
            actor.Movies.Count.Should().Be(registeredActor.Movies.Count);
            actor.ActorFullName.Should().Be(registeredActor.Name + " " + registeredActor.Surname);
        }
        [Fact]
        public void WhenActorIdValueIsNotFound_InvalidOperationException_ShouldBeError()
        {
            //Arrange(Hazırlık)
            GetActorByIdQuery query = new GetActorByIdQuery(_dbContext, _mapper);
            query.ActorId = 999999999;

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aktör Bulunamadı.");
        }
    }
}
