using MovieStoreWebAPI.Application.GenreOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.Application.OrderOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.Entity.Concrete;

namespace MovieStoreWebAPI.Application.CustomerOperations.Queries.QueryViewModel
{
    public class CustomerViewModel
    {
        public string FullName { get; set; }
        public ICollection<GenreViewModel> FavoriteGenre { get; set; }
        public ICollection<OrderViewModel> Orders { get; set; }
    }
}
