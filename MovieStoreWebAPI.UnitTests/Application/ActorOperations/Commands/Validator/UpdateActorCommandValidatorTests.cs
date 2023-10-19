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
    public class UpdateActorCommandValidatorTests
    {
        [Theory]
        [InlineData("", "", 1)]
        [InlineData("nam", "", 1)]
        [InlineData("", "sur", 1)]
        [InlineData("name", "", 1)]
        [InlineData("", "surname", 1)]
        [InlineData("name", "sur", 1)]
        [InlineData("nam", "surname", 1)]
        [InlineData("nam", "sur", 1)]
        [InlineData("", "", 0)]
        [InlineData("nam", "", 0)]
        [InlineData("", "sur", 0)]
        [InlineData("name", "", 0)]
        [InlineData("", "surname", 0)]
        [InlineData("name", "sur", 0)]
        [InlineData("nam", "surname", 0)]
        [InlineData("nam", "sur", 0)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(string actorName, string actorSurname, int actorId)
        {
            //Arrange(Hazırlık)
            UpdateActorCommand command = new UpdateActorCommand(null);
            command.ActorId = actorId;
            command.Model = new UpdateActorModel()
            {
                Name = actorName,
                Surname = actorSurname
            };

            //Act (Çalıştırma)
            UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
            var result = validator.Validate(command);

            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
