using FluentAssertions;
using MovieStoreWebAPI.Application.ActorOperations.Queries.QueryHandler.GetActor;
using MovieStoreWebAPI.Application.ActorOperations.Queries.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.ActorOperations.Queries.Validator
{
    public class GetActorByIdQueryValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int actorId)
        {
            //Arrange(Hazırlık)
            GetActorByIdQuery command = new GetActorByIdQuery(null, null);
            command.ActorId = actorId;

            //Act (Çalıştırma)
            GetActorByIdQueryValidator validator = new GetActorByIdQueryValidator();
            var result = validator.Validate(command);


            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
