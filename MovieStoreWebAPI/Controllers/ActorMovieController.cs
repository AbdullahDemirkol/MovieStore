using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebAPI.Application.ActorMovieOperations.Command.CommandHandler;
using MovieStoreWebAPI.Application.ActorMovieOperations.Command.RequestCommandModel;
using MovieStoreWebAPI.Application.ActorMovieOperations.Command.Validator;
using MovieStoreWebAPI.Application.ActorMovieOperations.Query.QueryHandler.GetActorMovie;
using MovieStoreWebAPI.Application.ActorMovieOperations.Query.QueryHandler.GetActorsMovies;
using MovieStoreWebAPI.Application.ActorMovieOperations.Query.QueryViewModel;
using MovieStoreWebAPI.Application.ActorMovieOperations.Query.Validator;
using MovieStoreWebAPI.Application.ActorOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class ActorMovieController : ControllerBase
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public ActorMovieController(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetActorsMovies()
        {
            GetActorsMoviesQuery query = new GetActorsMoviesQuery(_dbContext, _mapper);

            List<ActorMovieViewModel> viewModels = query.Handle();

            return Ok(viewModels);
        }
        [HttpGet("{actorMovieId}")]
        public IActionResult GetActorMovie(int actorMovieId)
        {
            GetActorMovieByIdQuery query = new GetActorMovieByIdQuery(_dbContext, _mapper);
            query.ActorMovieId=actorMovieId;

            GetActorMovieByIdQueryValidator validator = new GetActorMovieByIdQueryValidator();
            validator.ValidateAndThrow(query);

            ActorMovieViewModel viewModel = query.Handle();
            return Ok(viewModel);
        }
        [HttpPost]
        public IActionResult CreateActorMovie([FromBody] CreateActorMovieModel newActorMovie)
        {
            CreateActorMovieCommand command = new CreateActorMovieCommand(_dbContext, _mapper);
            command.Model = newActorMovie;

            CreateActorMovieCommandValidator validator = new CreateActorMovieCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
        [HttpPut("{actorMovieId}")]
        public IActionResult UpdateActorMovie(int actorMovieId, [FromBody] UpdateActorMovieModel updateActorMovie)
        {
            UpdateActorMovieCommand command = new UpdateActorMovieCommand(_dbContext);
            command.ActorMovieId=actorMovieId;
            command.Model = updateActorMovie;

            UpdateActorMovieCommandValidator validator = new UpdateActorMovieCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
        [HttpDelete("{actorMovieId}")]
        public IActionResult DeleteActorMovie(int actorMovieId)
        {
            DeleteActorMovieCommand command = new DeleteActorMovieCommand(_dbContext);
            command.ActorMovieId = actorMovieId;

            DeleteActorMovieCommandValidator validator = new DeleteActorMovieCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
    }
}
