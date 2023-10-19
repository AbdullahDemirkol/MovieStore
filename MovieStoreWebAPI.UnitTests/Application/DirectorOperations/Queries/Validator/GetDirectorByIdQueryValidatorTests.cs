using FluentAssertions;
using MovieStoreWebAPI.Application.DirectorOperations.Queries.QueryHandler.GetDirector;
using MovieStoreWebAPI.Application.DirectorOperations.Queries.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.DirectorOperations.Queries.Validator
{
    public class GetDirectorByIdQueryValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int directorId)
        {
            //Arrange(Hazırlık)
            GetDirectorByIdQuery command = new GetDirectorByIdQuery(null, null);
            command.DirectorId = directorId;

            //Act (Çalıştırma)
            GetDirectorByIdQueryValidator validator = new GetDirectorByIdQueryValidator();
            var result = validator.Validate(command);


            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
