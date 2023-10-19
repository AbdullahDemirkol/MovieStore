using FluentAssertions;
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
    public class UpdateMovieCommandValidatorTests
    {
        [Theory]
        [InlineData("", 0, 0, 0, 0, true)]
        [InlineData("", 1, 0, 0, 0, true)]
        [InlineData("", 0, 1, 0, 0, true)]
        [InlineData("", 0, 0, 1, 0, true)]
        [InlineData("", 0, 0, 0, 1, true)]
        [InlineData("", 1, 1, 0, 0, true)]
        [InlineData("", 1, 0, 1, 0, true)]
        [InlineData("", 1, 0, 0, 1, true)]
        [InlineData("", 0, 1, 1, 0, true)]
        [InlineData("", 0, 1, 0, 1, true)]
        [InlineData("", 0, 0, 1, 1, true)]
        [InlineData("", 1, 1, 1, 0, true)]
        [InlineData("", 1, 1, 0, 1, true)]
        [InlineData("", 1, 0, 1, 1, true)]
        [InlineData("", 0, 1, 1, 1, true)]
        [InlineData("", 1, 1, 1, 1, true)]
        [InlineData("tit", 0, 0, 0, 0, true)]
        [InlineData("tit", 1, 0, 0, 0, true)]
        [InlineData("tit", 0, 1, 0, 0, true)]
        [InlineData("tit", 0, 0, 1, 0, true)]
        [InlineData("tit", 0, 0, 0, 1, true)]
        [InlineData("tit", 1, 1, 0, 0, true)]
        [InlineData("tit", 1, 0, 1, 0, true)]
        [InlineData("tit", 1, 0, 0, 1, true)]
        [InlineData("tit", 0, 1, 1, 0, true)]
        [InlineData("tit", 0, 1, 0, 1, true)]
        [InlineData("tit", 0, 0, 1, 1, true)]
        [InlineData("tit", 1, 1, 1, 0, true)]
        [InlineData("tit", 1, 1, 0, 1, true)]
        [InlineData("tit", 1, 0, 1, 1, true)]
        [InlineData("tit", 0, 1, 1, 1, true)]
        [InlineData("tit", 1, 1, 1, 1, true)]
        [InlineData("tittle", 0, 0, 0, 0, true)]
        [InlineData("tittle", 1, 0, 0, 0, true)]
        [InlineData("tittle", 0, 1, 0, 0, true)]
        [InlineData("tittle", 0, 0, 1, 0, true)]
        [InlineData("tittle", 0, 0, 0, 1, true)]
        [InlineData("tittle", 1, 1, 0, 0, true)]
        [InlineData("tittle", 1, 0, 1, 0, true)]
        [InlineData("tittle", 1, 0, 0, 1, true)]
        [InlineData("tittle", 0, 1, 1, 0, true)]
        [InlineData("tittle", 0, 1, 0, 1, true)]
        [InlineData("tittle", 0, 0, 1, 1, true)]
        [InlineData("tittle", 1, 1, 1, 0, true)]
        [InlineData("tittle", 1, 1, 0, 1, true)]
        [InlineData("tittle", 1, 0, 1, 1, true)]
        [InlineData("tittle", 0, 1, 1, 1, true)]
        [InlineData("", 0, 0, 0, 0, false)]
        [InlineData("", 1, 0, 0, 0, false)]
        [InlineData("", 0, 1, 0, 0, false)]
        [InlineData("", 0, 0, 1, 0, false)]
        [InlineData("", 0, 0, 0, 1, false)]
        [InlineData("", 1, 1, 0, 0, false)]
        [InlineData("", 1, 0, 1, 0, false)]
        [InlineData("", 1, 0, 0, 1, false)]
        [InlineData("", 0, 1, 1, 0, false)]
        [InlineData("", 0, 1, 0, 1, false)]
        [InlineData("", 0, 0, 1, 1, false)]
        [InlineData("", 1, 1, 1, 0, false)]
        [InlineData("", 1, 1, 0, 1, false)]
        [InlineData("", 1, 0, 1, 1, false)]
        [InlineData("", 0, 1, 1, 1, false)]
        [InlineData("", 1, 1, 1, 1, false)]
        [InlineData("tit", 0, 0, 0, 0, false)]
        [InlineData("tit", 1, 0, 0, 0, false)]
        [InlineData("tit", 0, 1, 0, 0, false)]
        [InlineData("tit", 0, 0, 1, 0, false)]
        [InlineData("tit", 0, 0, 0, 1, false)]
        [InlineData("tit", 1, 1, 0, 0, false)]
        [InlineData("tit", 1, 0, 1, 0, false)]
        [InlineData("tit", 1, 0, 0, 1, false)]
        [InlineData("tit", 0, 1, 1, 0, false)]
        [InlineData("tit", 0, 1, 0, 1, false)]
        [InlineData("tit", 0, 0, 1, 1, false)]
        [InlineData("tit", 1, 1, 1, 0, false)]
        [InlineData("tit", 1, 1, 0, 1, false)]
        [InlineData("tit", 1, 0, 1, 1, false)]
        [InlineData("tit", 0, 1, 1, 1, false)]
        [InlineData("tit", 1, 1, 1, 1, false)]
        [InlineData("tittle", 0, 0, 0, 0, false)]
        [InlineData("tittle", 1, 0, 0, 0, false)]
        [InlineData("tittle", 0, 1, 0, 0, false)]
        [InlineData("tittle", 0, 0, 1, 0, false)]
        [InlineData("tittle", 0, 0, 0, 1, false)]
        [InlineData("tittle", 1, 1, 0, 0, false)]
        [InlineData("tittle", 1, 0, 1, 0, false)]
        [InlineData("tittle", 1, 0, 0, 1, false)]
        [InlineData("tittle", 0, 1, 1, 0, false)]
        [InlineData("tittle", 0, 1, 0, 1, false)]
        [InlineData("tittle", 0, 0, 1, 1, false)]
        [InlineData("tittle", 1, 1, 1, 0, false)]
        [InlineData("tittle", 1, 1, 0, 1, false)]
        [InlineData("tittle", 1, 0, 1, 1, false)]
        [InlineData("tittle", 0, 1, 1, 1, false)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(string title, int movieId, int directorId, int genreId, decimal price, bool isActive)
        {
            //Arrange(Hazırlık)
            UpdateMovieCommand command = new UpdateMovieCommand(null);
            command.MovieId = movieId;
            command.Model = new UpdateMovieModel()
            {
                Title = title,
                DirectorId = directorId,
                GenreId = genreId,
                Price = price,
                IsActive = isActive 
            };

            //Act (Çalıştırma)
            UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
            var result = validator.Validate(command);

            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
