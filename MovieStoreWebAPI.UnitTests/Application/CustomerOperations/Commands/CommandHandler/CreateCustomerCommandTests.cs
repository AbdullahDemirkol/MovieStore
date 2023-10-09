using AutoMapper;
using FluentAssertions;
using MovieStoreWebAPI.Application.CustomerOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.CustomerOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.DataAccess.Concrete;
using MovieStoreWebAPI.UnitTests.TestsSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.CustomerOperations.Commands.CommandHandler
{
    public class CreateCustomerCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateCustomerCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_User_ShouldBeCreated()
        {
            //Arrange(Hazırlık)
            CreateCustomerCommand command = new CreateCustomerCommand(_dbContext, _mapper);
            command.Model = new CreateCustomerModel()
            {
                Name = "newName",
                Surname = "newSurname",
                Email = "newEmail@gmail.com",
                Password = "12345",

            };


            //Act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var customer = _dbContext.Customers.FirstOrDefault(u => u.Email == "newEmail@gmail.com");
            customer.Should().NotBeNull();
            customer.Id.Should().BeGreaterThan(0);
            customer.Name.Should().NotBeNull();
            customer.Surname.Should().NotBeNull();
            customer.Email.Should().NotBeNull();
            customer.Password.Should().NotBeNull();
        }
        [Fact]
        public void WhenAlreadyExistEmailIsGiven_InvalidOperationException_ShouldBeReturned()
        {

            //Arrange(Hazırlık)
            CreateCustomerCommand command = new CreateCustomerCommand(_dbContext, _mapper);
            command.Model = new CreateCustomerModel()
            {
                Name = "newName",
                Surname = "newSurname",
                Email = "Test@gmail.com",
                Password = "12345",

            };
            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kullanıcı Zaten Mevcut.");

        }
    }
}
