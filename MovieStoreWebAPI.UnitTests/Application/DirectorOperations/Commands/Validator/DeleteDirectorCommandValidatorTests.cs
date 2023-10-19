using FluentAssertions;
using MovieStoreWebAPI.Application.DirectorOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.DirectorOperations.Commands.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.DirectorOperations.Commands.Validator
{
    public class DeleteDirectorCommandValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int directorId)
        {
            //Arrange(Hazırlık)
            DeleteDirectorCommand command = new DeleteDirectorCommand(null);
            command.DirectorId = directorId;

            //Act (Çalıştırma)
            DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
            var result = validator.Validate(command);


            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
