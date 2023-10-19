using MovieStoreWebAPI.Application.MovieOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.Application.OrderOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.Entity.Concrete;

namespace MovieStoreWebAPI.Application.ActorOperations.Queries.QueryViewModel
{
    public class ActorViewModel
    {
        public string ActorFullName { get; set; }
        public ICollection<string> Movies { get; set; }
    }
}
