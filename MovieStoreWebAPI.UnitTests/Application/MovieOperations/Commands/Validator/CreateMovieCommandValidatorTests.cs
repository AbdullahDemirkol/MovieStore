using FluentAssertions;
using MovieStoreWebAPI.Application.GenreOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.GenreOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.Application.GenreOperations.Commands.Validator;
using MovieStoreWebAPI.Application.MovieOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.MovieOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.Application.MovieOperations.Commands.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.MovieOperations.Commands.Validator
{
    public class CreateMovieCommandValidatorTests
    {
        [Theory]
        [InlineData(0, 0, 0, "")]
        [InlineData(1, 0, 0, "")]
        [InlineData(0, 1, 0, "")]
        [InlineData(0, 0, 1, "")]
        [InlineData(1, 1, 0, "")]
        [InlineData(1, 0, 1, "")]
        [InlineData(0, 1, 1, "")]
        [InlineData(1, 1, 1, "")]
        [InlineData(0, 0, 0, "tit")]
        [InlineData(1, 0, 0, "tit")]
        [InlineData(0, 1, 0, "tit")]
        [InlineData(0, 0, 1, "tit")]
        [InlineData(1, 1, 0, "tit")]
        [InlineData(1, 0, 1, "tit")]
        [InlineData(0, 1, 1, "tit")]
        [InlineData(1, 1, 1, "tit")]
        [InlineData(0, 0, 0, "title")]
        [InlineData(1, 0, 0, "title")]
        [InlineData(0, 1, 0, "title")]
        [InlineData(0, 0, 1, "title")]
        [InlineData(1, 1, 0, "title")]
        [InlineData(1, 0, 1, "title")]
        [InlineData(0, 1, 1, "title")]

        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int genreId,int directorId,decimal price,string title)
        {
            //Arrange(Hazırlık)
            CreateMovieCommand command = new CreateMovieCommand(null, null);
            command.Model = new CreateMovieModel()
            {
                Title = title,
                GenreId = genreId,
                DirectorId = directorId,
                Price = price,
                YearOfMovie = DateTime.Now.AddYears(-1)
            };

            //Act (Çalıştırma)
            CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
            var result = validator.Validate(command);


            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenAnIncorrectDateIsEntered_InvalidOperationException_ShouldBeError()
        {
            //Arrange(Hazırlık)
            CreateMovieCommand command = new CreateMovieCommand(null, null);
            command.Model = new CreateMovieModel()
            {
                Title = "asd",
                GenreId = 1,
                DirectorId = 1,
                Price = 100,
                YearOfMovie = DateTime.Now
            };

            //Act (Çalıştırma)
            CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
            var result = validator.Validate(command);


            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
