using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebAPI.Application.GenreOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.GenreOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.Application.GenreOperations.Commands.Validator;
using MovieStoreWebAPI.Application.GenreOperations.Queries.QueryHandler.GetGenre;
using MovieStoreWebAPI.Application.GenreOperations.Queries.QueryHandler.GetGenres;
using MovieStoreWebAPI.Application.GenreOperations.Queries.Validator;
using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GenreController(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_dbContext,_mapper);
            var genres = query.Handle();
            return Ok(genres);
        }
        [HttpGet("{genreId}")]
        public IActionResult GetGenreById(int genreId)
        {
            GetGenreByIdQuery query = new GetGenreByIdQuery(_dbContext, _mapper);
            query.GenreId=genreId;

            GetGenreByIdQueryValidator validator = new GetGenreByIdQueryValidator();
            validator.ValidateAndThrow(query);

            var genre = query.Handle();
            return Ok(genre);
        }
        [Authorize]
        [HttpPost]
        public IActionResult CreateGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_dbContext, _mapper);
            command.Model=newGenre;

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
        [Authorize]
        [HttpPut("{genreId}")]
        public IActionResult UpdateGenre(int genreId, [FromBody] UpdateGenreModel updateGenre)
        {
            UpdateGenreCommand command=new UpdateGenreCommand(_dbContext);
            command.GenreId = genreId;
            command.Model=updateGenre;

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
        [Authorize]
        [HttpDelete("{genreId}")]
        public IActionResult DeleteGenre(int genreId)
        {
            DeleteGenreCommand command=new DeleteGenreCommand(_dbContext);
            command.GenreId = genreId;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
    }
}
