using FluentAssertions;
using MovieStoreWebAPI.Application.DirectorOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.DirectorOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.Application.DirectorOperations.Commands.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.DirectorOperations.Commands.Validator
{
    public class CreateDirectorCommandValidatorTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("nam", "")]
        [InlineData("", "sur")]
        [InlineData("name", "")]
        [InlineData("", "surname")]
        [InlineData("name", "sur")]
        [InlineData("nam", "surname")]
        [InlineData("nam", "sur")]

        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(string directorName, string directorSurname)
        {
            //Arrange(Hazırlık)
            CreateDirectorCommand command = new CreateDirectorCommand(null, null);
            command.Model = new CreateDirectorModel()
            {
                Name = directorName,
                Surname = directorSurname
            };

            //Act (Çalıştırma)
            CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
            var result = validator.Validate(command);


            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
