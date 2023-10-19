using AutoMapper;
using FluentAssertions;
using MovieStoreWebAPI.Application.DirectorOperations.Queries.QueryHandler.GetDirector;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.UnitTests.TestsSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.DirectorOperations.Queries.QueryHandler.GetDirector
{
    public class GetDirectorByIdQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetDirectorByIdQueryTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Director_ShouldBeReturned()
        {
            //Arrange(Hazırlık)
            int directorId = _dbContext.Directors.FirstOrDefault().Id;
            GetDirectorByIdQuery query = new GetDirectorByIdQuery(_dbContext, _mapper);
            query.DirectorId = directorId;

            //Act (Çalıştırma)
            var director = FluentActions.Invoking(() => query.Handle()).Invoke();

            //Assert (Doğrulama)
            var registeredDirector = _dbContext.Directors.FirstOrDefault(am => am.Id == directorId);
            director.Should().NotBeNull();
            director.Movies.Count.Should().Be(registeredDirector.Movies.Count);
            director.DirectorFullName.Should().Be(registeredDirector.Name + " " + registeredDirector.Surname);
        }
        [Fact]
        public void WhenDirectorIdValueIsNotFound_InvalidOperationException_ShouldBeError()
        {
            //Arrange(Hazırlık)
            GetDirectorByIdQuery query = new GetDirectorByIdQuery(_dbContext, _mapper);
            query.DirectorId = 999999999;

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yönetmen Bulunamadı.");
        }
    }
}
