using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebAPI.Application.MovieOperations.Queries.QueryHandler.GetMovie;
using MovieStoreWebAPI.Application.MovieOperations.Queries.QueryHandler.GetMovies;
using MovieStoreWebAPI.Application.MovieOperations.Queries.Validator;
using MovieStoreWebAPI.DataAccess.Concrete;
using System.Runtime.CompilerServices;

namespace MovieStoreWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class MovieController : ControllerBase
    {
        private readonly MovieStoreDbContext _dbContext;
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
    }
}
