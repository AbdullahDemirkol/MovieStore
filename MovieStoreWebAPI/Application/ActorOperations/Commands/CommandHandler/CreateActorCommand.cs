using AutoMapper;
using MovieStoreWebAPI.Application.ActorOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.Entity.Concrete;

namespace MovieStoreWebAPI.Application.ActorOperations.Commands.CommandHandler
{
    public class CreateActorCommand
    {
        public CreateActorModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateActorCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var actor=_dbContext.Actors.FirstOrDefault(a=>
            a.Name.Replace(" ","").ToLower()==Model.Name.Replace(" ", "").ToLower() 
            && a.Surname.Replace(" ", "").ToLower() == Model.Surname.Replace(" ", "").ToLower());
            if (actor is not null)
            {
                throw new InvalidOperationException("Aktor Bulunmaktadır.");
            }

            actor = _mapper.Map<Actor>(Model);

            _dbContext.Actors.Add(actor);
            _dbContext.SaveChanges();
        }
    }
}
