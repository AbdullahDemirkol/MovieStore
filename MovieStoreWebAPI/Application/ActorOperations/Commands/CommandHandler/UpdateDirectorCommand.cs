using AutoMapper;
using MovieStoreWebAPI.Application.ActorOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Application.ActorOperations.Commands.CommandHandler
{
    public class UpdateActorCommand
    {
        public int ActorId { get; set; }
        public UpdateActorModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateActorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var actor=_dbContext.Actors.FirstOrDefault(a=>a.Id==ActorId);
            if (actor is null)
            {
                throw new InvalidOperationException("Aktor Bulunamadı.");
            }
            bool hasActor = _dbContext.Actors.Any(a => a.Name.ToLower().Replace(" ", "") == Model.Name.ToLower().Replace(" ", "")
            && a.Surname.ToLower().Replace(" ", "") == Model.Surname.ToLower().Replace(" ", "")
            && a.Id != ActorId);
            if (hasActor)
            {
                throw new InvalidOperationException("Aktor Isim ve Soyisime Ait Kayıtlı Aktor Bulunmaktadır.");
            }
            
            actor.Name= !string.IsNullOrEmpty(Model.Name) ? Model.Name : actor.Name;
            actor.Surname = !string.IsNullOrEmpty(Model.Surname) ? Model.Surname:actor.Surname;

            _dbContext.SaveChanges();
        }

    }
}
