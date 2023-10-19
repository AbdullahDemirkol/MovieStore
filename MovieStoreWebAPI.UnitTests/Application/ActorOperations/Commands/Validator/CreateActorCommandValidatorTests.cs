using FluentAssertions;
using MovieStoreWebAPI.Application.ActorOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.ActorOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.Application.ActorOperations.Commands.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.ActorOperations.Commands.Validator
{
    public class CreateActorCommandValidatorTests
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

        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(string actorName, string actorSurname)
        {
            //Arrange(Hazırlık)
            CreateActorCommand command = new CreateActorCommand(null, null);
            command.Model = new CreateActorModel()
            {
                Name=actorName,
                Surname=actorSurname
            };

            //Act (Çalıştırma)
            CreateActorCommandValidator validator = new CreateActorCommandValidator();
            var result = validator.Validate(command);


            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
