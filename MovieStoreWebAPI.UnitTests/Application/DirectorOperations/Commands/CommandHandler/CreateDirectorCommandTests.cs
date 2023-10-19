using AutoMapper;
using FluentAssertions;
using MovieStoreWebAPI.Application.DirectorOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.DirectorOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.UnitTests.TestsSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.DirectorOperations.Commands.CommandHandler
{
    public class CreateDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateDirectorCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Director_ShouldBeCreated()
        {
            //Arrange(Hazırlık)
            CreateDirectorCommand command = new CreateDirectorCommand(_dbContext, _mapper);
            command.Model = new CreateDirectorModel()
            {
                Name = "test",
                Surname = "test"
            };

            //Act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var director = _dbContext.Directors.FirstOrDefault(am => am.Name == "test" && am.Surname == "test");
            director.Should().NotBeNull();
        }
        [Fact]
        public void WhenAlreadyDirectorIsGiven_InvalidOperationException_ShouldBeError()
        {

            //Arrange(Hazırlık)
            var director = _dbContext.Directors.FirstOrDefault();
            CreateDirectorCommand command = new CreateDirectorCommand(_dbContext, _mapper);
            command.Model = new CreateDirectorModel()
            {
                Name = director.Name,
                Surname = director.Surname
            };

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yonetmen Bulunmaktadır.");
        }
    }
}
