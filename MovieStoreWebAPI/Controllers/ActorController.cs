using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebAPI.Application.ActorOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.ActorOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.Application.ActorOperations.Commands.Validator;
using MovieStoreWebAPI.Application.ActorOperations.Queries.QueryHandler.GetActor;
using MovieStoreWebAPI.Application.ActorOperations.Queries.QueryHandler.GetActors;
using MovieStoreWebAPI.Application.ActorOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.Application.ActorOperations.Queries.Validator;
using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class ActorController : ControllerBase
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public ActorController(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetActors()
        {
            GetActorsQuery query = new GetActorsQuery(_dbContext, _mapper);
            List<ActorViewModel> actors = query.Handle();
            return Ok(actors);
        }
        [Authorize]
        [HttpGet("{actorId}")]
        public IActionResult GetActorById(int actorId)
        {
            GetActorByIdQuery query = new GetActorByIdQuery(_dbContext, _mapper);
            query.ActorId=actorId;

            GetActorByIdQueryValidator validator= new GetActorByIdQueryValidator();
            validator.ValidateAndThrow(query);

            var actor = query.Handle();
            return Ok(actor);
        }
        [Authorize]
        [HttpPost]
        public IActionResult CreateActor([FromBody] CreateActorModel newActor)
        {
            CreateActorCommand command = new CreateActorCommand(_dbContext, _mapper);
            command.Model = newActor;

            CreateActorCommandValidator validator = new CreateActorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
        [Authorize]
        [HttpPut]
        public IActionResult UpdateActor(int actorId, [FromBody] UpdateActorModel updateActor)
        {
            UpdateActorCommand command=new UpdateActorCommand(_dbContext);
            command.ActorId=actorId;
            command.Model =updateActor;

            UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
        [Authorize]
        [HttpDelete]
        public IActionResult DeleteActor(int actorId)
        {
            DeleteActorCommand command = new DeleteActorCommand(_dbContext);
            command.ActorId = actorId;

            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

    }
}
