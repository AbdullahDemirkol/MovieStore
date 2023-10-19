using MovieStoreWebAPI.Application.DirectorOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Application.DirectorOperations.Commands.CommandHandler
{
    public class UpdateDirectorCommand
    {
        public int DirectorId { get; set; }
        public UpdateDirectorModel Model { get; set; }

        private readonly IMovieStoreDbContext _dbContext;

        public UpdateDirectorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var director=_dbContext.Directors.FirstOrDefault(d=>d.Id==DirectorId);
            if (director is null)
            {
                throw new InvalidOperationException("Yonetmen Bulunamadı.");
            }
            bool hasDirector = _dbContext.Directors
                .Any(d => d.Name.ToLower().Replace(" ", "") == Model.Name.ToLower().Replace(" ", "") &&
                d.Surname.ToLower().Replace(" ", "") == Model.Surname.ToLower().Replace(" ", "") &&
                d.Id != DirectorId);
            if (hasDirector)
            {
                throw new InvalidOperationException("Yonetmen Isim ve Soyisime Ait Kayıtlı Yönetmen Bulunmaktadır.");
            }

            director.Name = !string.IsNullOrEmpty(Model.Name) ? Model.Name : director.Name;
            director.Surname = !string.IsNullOrEmpty(Model.Surname) ? Model.Surname : director.Surname;

            _dbContext.SaveChanges();
        }

    }
}
