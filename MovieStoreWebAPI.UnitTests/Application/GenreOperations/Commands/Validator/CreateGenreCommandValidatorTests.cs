using FluentAssertions;
using MovieStoreWebAPI.Application.ActorOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.ActorOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.Application.ActorOperations.Commands.Validator;
using MovieStoreWebAPI.Application.GenreOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.GenreOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.Application.GenreOperations.Commands.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.GenreOperations.Commands.Validator
{
    public class CreateGenreCommandValidatorTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("nam")]

        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(string genreName)
        {
            //Arrange(Hazırlık)
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            command.Model = new CreateGenreModel()
            {
                Name = genreName
            };

            //Act (Çalıştırma)
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);


            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
