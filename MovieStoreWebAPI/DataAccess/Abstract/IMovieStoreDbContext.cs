using Microsoft.EntityFrameworkCore;
using MovieStoreWebAPI.Entity.Concrete;

namespace MovieStoreWebAPI.DataAccess.Abstract
{
    public interface IMovieStoreDbContext
    {
        int SaveChanges();
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorMovieRelationship> ActorMovieRelationships { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
