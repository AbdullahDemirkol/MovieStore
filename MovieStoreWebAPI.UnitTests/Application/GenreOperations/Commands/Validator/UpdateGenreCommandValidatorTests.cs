using FluentAssertions;
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
    public class UpdateGenreCommandValidatorTests
    {
        [Theory]
        [InlineData("", 0)]
        [InlineData("nam", 0)]
        [InlineData("",1)]
        [InlineData("nam",1)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(string genreName, int genreId)
        {
            //Arrange(Hazırlık)
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = genreId;
            command.Model = new UpdateGenreModel()
            {
                Name = genreName
            };

            //Act (Çalıştırma)
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
