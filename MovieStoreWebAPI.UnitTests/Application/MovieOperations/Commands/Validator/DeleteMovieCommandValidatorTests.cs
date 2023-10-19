using FluentAssertions;
using MovieStoreWebAPI.Application.GenreOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.GenreOperations.Commands.Validator;
using MovieStoreWebAPI.Application.MovieOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.MovieOperations.Commands.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.MovieOperations.Commands.Validator
{
    public class DeleteMovieCommandValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int movieId)
        {
            //Arrange(Hazırlık)
            DeleteMovieCommand command = new DeleteMovieCommand(null);
            command.MovieId = movieId;

            //Act (Çalıştırma)
            DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
            var result = validator.Validate(command);


            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
