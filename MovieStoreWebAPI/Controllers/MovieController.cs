using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebAPI.Application.MovieOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.MovieOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.Application.MovieOperations.Commands.Validator;
using MovieStoreWebAPI.Application.MovieOperations.Queries.QueryHandler.GetMovie;
using MovieStoreWebAPI.Application.MovieOperations.Queries.QueryHandler.GetMovies;
using MovieStoreWebAPI.Application.MovieOperations.Queries.Validator;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.DataAccess.Concrete;

namespace MovieStoreWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public MovieController(MovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetMovies()
        {
            GetMoviesQuery query = new GetMoviesQuery(_dbContext, _mapper);
            var result= query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetMovie(int id)
        {
            GetMovieByIdQuery query = new GetMovieByIdQuery(_dbContext, _mapper);
            query.MovieId = id;

            GetMovieByIdQueryValidator validator=new GetMovieByIdQueryValidator();
            validator.ValidateAndThrow(query);

            var result = query.Handle();
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        public IActionResult CreateMovie([FromBody] CreateMovieModel newMovie)
        {
            CreateMovieCommand command = new CreateMovieCommand(_dbContext, _mapper);
            command.Model=newMovie;

            CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
        [Authorize]
        [HttpPut("{movieId}")]
        public IActionResult UpdateMovie(int movieId, [FromBody] UpdateMovieModel updateMovie)
        {
            UpdateMovieCommand command = new UpdateMovieCommand(_dbContext);
            command.MovieId= movieId;
            command.Model=updateMovie;

            UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
        [Authorize]
        [HttpDelete("{movieId}")]
        public IActionResult DeleteMovie(int movieId)
        {
            DeleteMovieCommand command = new DeleteMovieCommand(_dbContext);
            command.MovieId= movieId;

            DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
    }
}
