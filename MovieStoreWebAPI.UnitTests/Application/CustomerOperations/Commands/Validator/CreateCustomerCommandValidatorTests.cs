using FluentAssertions;
using MovieStoreWebAPI.Application.CustomerOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.CustomerOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.Application.CustomerOperations.Commands.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.CustomerOperations.Commands.Validator
{
    public class CreateCustomerCommandValidatorTests
    {
        [Theory]
        [InlineData("", "", "", "")]
        [InlineData("Name", "", "", "")]
        [InlineData("", "Surname", "", "")]
        [InlineData("", "", "Email", "")]
        [InlineData("", "", "", "Password")]
        [InlineData("Name", "Surname", "", "")]
        [InlineData("Name", "", "Email", "")]
        [InlineData("Name", "", "", "Password")]
        [InlineData("", "Surname", "Email", "")]
        [InlineData("", "Surname", "", "Password")]
        [InlineData("", "", "Email", "Password")]
        [InlineData("Name", "Surname", "Email", "")]
        [InlineData("Name", "Surname", "", "Password")]
        [InlineData("Name", "", "Email", "Password")]
        [InlineData("", "Surname", "Email", "Password")]
        public void WhenInvalidInputsAreGiven_InvalidOperationException_ShouldBeErrors(string name, string surname, string email, string password)
        {
            //Arrange(Hazırlık)
            CreateCustomerCommand command = new CreateCustomerCommand(null, null);
            command.Model = new CreateCustomerModel()
            {
                Name = name,
                Surname = surname,
                Email = email,
                Password = password
            };

            //Act (Çalıştırma)
            CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
            var result = validator.Validate(command);

            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);

        }
    }
}
