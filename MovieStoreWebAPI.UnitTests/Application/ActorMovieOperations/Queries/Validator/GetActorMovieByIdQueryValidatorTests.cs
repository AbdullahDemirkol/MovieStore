using FluentAssertions;
using MovieStoreWebAPI.Application.ActorMovieOperations.Query.QueryHandler.GetActorMovie;
using MovieStoreWebAPI.Application.ActorMovieOperations.Query.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.ActorMovieOperations.Queries.Validator
{
    public class GetActorMovieByIdQueryValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int actorMovieId)
        {
            //Arrange(Hazırlık)
            GetActorMovieByIdQuery command = new GetActorMovieByIdQuery(null, null);
            command.ActorMovieId = actorMovieId;

            //Act (Çalıştırma)
            GetActorMovieByIdQueryValidator validator = new GetActorMovieByIdQueryValidator();
            var result = validator.Validate(command);


            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
