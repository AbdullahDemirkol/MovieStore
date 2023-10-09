using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieStoreWebAPI.DataAccess.Concrete;
using MovieStoreWebAPI.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Extensions
{
    public static class MovieStoreDbContextExtensions
    {
        public static void Initialize(this MovieStoreDbContext context)
        {
            if (!context.Movies.Any())
            {
                context.Genres.AddRange(GetGenres());
                context.Directors.AddRange(GetDirector());
                context.Actors.AddRange(GetActor());
                context.SaveChanges();
                context.Movies.AddRange(GetMovie(context));
                context.SaveChanges();
                context.ActorMovieRelationships.AddRange(GetActorMovieRelationship(context));
                context.Customers.AddRange(GetCustomers());
                context.SaveChanges();
            }
        }

        private static List<Customer> GetCustomers()
        {
            var customers = new List<Customer>()
            {
                new Customer()
                {
                    Name = "Test",
                    Surname = "Test",
                    Email = "Test@gmail.com",
                    Password = "Test",
                }
            };
            return customers;
        }

        private static List<ActorMovieRelationship> GetActorMovieRelationship(MovieStoreDbContext context)
        {
            var actorsInterstellar = new List<ActorMovieRelationship>()
            {
                //Interstellar
                new ActorMovieRelationship()
                {
                   MovieId=context.Movies.FirstOrDefault(m=>m.Title=="Interstellar").Id,
                   ActorId=context.Actors.FirstOrDefault(a=>a.Name=="Matthew" && a.Surname=="McConaughey").Id
                },
                new ActorMovieRelationship()
                {
                   MovieId=context.Movies.FirstOrDefault(m=>m.Title=="Interstellar").Id,
                   ActorId=context.Actors.FirstOrDefault(a=>a.Name=="Anne" && a.Surname=="Hathaway").Id
                },
                new ActorMovieRelationship()
                {
                   MovieId=context.Movies.FirstOrDefault(m=>m.Title=="Interstellar").Id,
                   ActorId=context.Actors.FirstOrDefault(a=>a.Name=="Jessica" && a.Surname=="Chastain").Id
                },
                new ActorMovieRelationship()
                {
                   MovieId=context.Movies.FirstOrDefault(m=>m.Title=="Interstellar").Id,
                   ActorId=context.Actors.FirstOrDefault(a=>a.Name=="Michael" && a.Surname=="Caine").Id
                },
                new ActorMovieRelationship()
                {
                   MovieId=context.Movies.FirstOrDefault(m=>m.Title=="Interstellar").Id,
                   ActorId=context.Actors.FirstOrDefault(a=>a.Name=="Matt" && a.Surname=="Damon").Id
                }
            };
            var actorsTheGreenMile = new List<ActorMovieRelationship>()
            {
                //The Green Mile
                new ActorMovieRelationship()
                {
                    MovieId = context.Movies.FirstOrDefault(m => m.Title == "The Green Mile").Id,
                    ActorId = context.Actors.FirstOrDefault(a => a.Name == "Tom" && a.Surname == "Hanks").Id
                },
                new ActorMovieRelationship()
                {
                    MovieId = context.Movies.FirstOrDefault(m => m.Title == "The Green Mile").Id,
                    ActorId = context.Actors.FirstOrDefault(a => a.Name == "Michael" && a.Surname == "Clarke Duncan").Id
                },
                new ActorMovieRelationship()
                {
                    MovieId = context.Movies.FirstOrDefault(m => m.Title == "The Green Mile").Id,
                    ActorId = context.Actors.FirstOrDefault(a => a.Name == "David" && a.Surname == "Morse").Id
                },
                new ActorMovieRelationship()
                {
                    MovieId = context.Movies.FirstOrDefault(m => m.Title == "The Green Mile").Id,
                    ActorId = context.Actors.FirstOrDefault(a => a.Name == "Bonnie" && a.Surname == "Hunt").Id
                },
                new ActorMovieRelationship()
                {
                    MovieId = context.Movies.FirstOrDefault(m => m.Title == "The Green Mile").Id,
                    ActorId = context.Actors.FirstOrDefault(a => a.Name == "James" && a.Surname == "Cromwell").Id
                }
            };
            var actors8Mile = new List<ActorMovieRelationship>()
            {
                new ActorMovieRelationship()
                {
                    MovieId = context.Movies.FirstOrDefault(m => m.Title == "8 Mile").Id,
                    ActorId = context.Actors.FirstOrDefault(a => a.Name == "Marshall" && a.Surname == "Mathers").Id
                },
                new ActorMovieRelationship()
                {
                    MovieId = context.Movies.FirstOrDefault(m => m.Title == "8 Mile").Id,
                    ActorId = context.Actors.FirstOrDefault(a => a.Name == "Brittany" && a.Surname == "Murphy").Id
                },
                new ActorMovieRelationship()
                {
                    MovieId = context.Movies.FirstOrDefault(m => m.Title == "8 Mile").Id,
                    ActorId = context.Actors.FirstOrDefault(a => a.Name == "Mekhi" && a.Surname == "Phifer").Id
                },
                new ActorMovieRelationship()
                {
                    MovieId = context.Movies.FirstOrDefault(m => m.Title == "8 Mile").Id,
                    ActorId = context.Actors.FirstOrDefault(a => a.Name == "Kim" && a.Surname == "Basinger").Id
                },
                new ActorMovieRelationship()
                {
                    MovieId = context.Movies.FirstOrDefault(m => m.Title == "8 Mile").Id,
                    ActorId = context.Actors.FirstOrDefault(a => a.Name == "Evan" && a.Surname == "Jones").Id
                }
            };
            var actorsDoNotDisturb = new List<ActorMovieRelationship>()
            {
                //Interstellar
                new ActorMovieRelationship()
                {
                   MovieId=context.Movies.FirstOrDefault(m=>m.Title=="Do Not Disturb").Id,
                   ActorId=context.Actors.FirstOrDefault(a=>a.Name=="Cem" && a.Surname=="Yılmaz").Id
                },
                new ActorMovieRelationship()
                {
                   MovieId=context.Movies.FirstOrDefault(m=>m.Title=="Do Not Disturb").Id,
                   ActorId=context.Actors.FirstOrDefault(a=>a.Name=="Ahsen" && a.Surname=="Eroglu").Id
                },
                new ActorMovieRelationship()
                {
                   MovieId=context.Movies.FirstOrDefault(m=>m.Title=="Do Not Disturb").Id,
                   ActorId=context.Actors.FirstOrDefault(a=>a.Name=="Nilperi" && a.Surname=="Sahinkaya").Id
                },
                new ActorMovieRelationship()
                {
                   MovieId=context.Movies.FirstOrDefault(m=>m.Title=="Do Not Disturb").Id,
                   ActorId=context.Actors.FirstOrDefault(a=>a.Name=="Özge" && a.Surname=="Borak").Id
                },
                new ActorMovieRelationship()
                {
                   MovieId=context.Movies.FirstOrDefault(m=>m.Title=="Do Not Disturb").Id,
                   ActorId=context.Actors.FirstOrDefault(a=>a.Name=="Bülent" && a.Surname=="Şakrak").Id
                }
            };
            var actorsInTime = new List<ActorMovieRelationship>()
            {
                //Interstellar
                new ActorMovieRelationship()
                {
                   MovieId=context.Movies.FirstOrDefault(m=>m.Title=="In Time").Id,
                   ActorId=context.Actors.FirstOrDefault(a=>a.Name=="Justin" && a.Surname=="Timberlake").Id
                },
                new ActorMovieRelationship()
                {
                   MovieId=context.Movies.FirstOrDefault(m=>m.Title=="In Time").Id,
                   ActorId=context.Actors.FirstOrDefault(a=>a.Name=="Amanda" && a.Surname=="Seyfried").Id
                },
                new ActorMovieRelationship()
                {
                   MovieId=context.Movies.FirstOrDefault(m=>m.Title=="In Time").Id,
                   ActorId=context.Actors.FirstOrDefault(a=>a.Name=="Cillian" && a.Surname=="Murphy").Id
                },
                new ActorMovieRelationship()
                {
                   MovieId=context.Movies.FirstOrDefault(m=>m.Title=="In Time").Id,
                   ActorId=context.Actors.FirstOrDefault(a=>a.Name=="Olivia" && a.Surname=="Wilde").Id
                },
                new ActorMovieRelationship()
                {
                   MovieId=context.Movies.FirstOrDefault(m=>m.Title=="In Time").Id,
                   ActorId=context.Actors.FirstOrDefault(a=>a.Name=="Alex" && a.Surname=="Pettyfer").Id
                }
            };
            var actorMovieRelationShips = new List<ActorMovieRelationship>();
            actorMovieRelationShips.AddListRange(actorsInterstellar, actorsTheGreenMile, actors8Mile, actorsDoNotDisturb, actorsInTime);
            return actorMovieRelationShips;
        }

        private static List<Movie> GetMovie(MovieStoreDbContext context)
        {
            var movies = new List<Movie>()
            {
                new Movie()
                {
                    Title="Interstellar",
                    DirectorId=context.Directors.FirstOrDefault(d=>d.Name=="Cristopher" && d.Surname=="Nolan").Id,
                    GenreId=context.Genres.FirstOrDefault(g=>g.Name=="Science Fiction").Id,
                    YearOfMovie=new DateTime(2014,1,1),
                    Price=300
                },
                new Movie()
                {
                    Title="Do Not Disturb",
                    DirectorId=context.Directors.FirstOrDefault(d=>d.Name=="Cem" && d.Surname=="Yılmaz").Id,
                    GenreId=context.Genres.FirstOrDefault(g=>g.Name=="Comedy").Id,
                    YearOfMovie=new DateTime(2023,1,1),
                    Price=150
                },
                new Movie()
                {
                    Title="The Green Mile",
                    DirectorId=context.Directors.FirstOrDefault(d=>d.Name=="Frank" && d.Surname=="Darabont").Id,
                    GenreId=context.Genres.FirstOrDefault(g=>g.Name=="Drama").Id,
                    YearOfMovie=new DateTime(1999,1,1),
                    Price=250
                },
                new Movie()
                {
                    Title="In Time",
                    DirectorId=context.Directors.FirstOrDefault(d=>d.Name=="Andrew" && d.Surname=="Niccol").Id,
                    GenreId=context.Genres.FirstOrDefault(g=>g.Name=="Action").Id,
                    YearOfMovie=new DateTime(2011,1,1),
                    Price=200
                },
                new Movie()
                {
                    Title="8 Mile",
                    DirectorId=context.Directors.FirstOrDefault(d=>d.Name=="Curtis" && d.Surname=="Hanson").Id,
                    GenreId=context.Genres.FirstOrDefault(g=>g.Name=="Music").Id,
                    YearOfMovie=new DateTime(2014,1,1),
                    Price=300
                }
            };
            return movies;
        }

        private static List<Actor> GetActor()
        {
            var actors = new List<Actor>
            {
                //Interstellar
                new Actor { Name = "Matthew", Surname = "McConaughey" },
                new Actor { Name = "Anne", Surname = "Hathaway" },
                new Actor { Name = "Jessica", Surname = "Chastain" },
                new Actor { Name = "Michael", Surname = "Caine" },
                new Actor { Name = "Matt", Surname = "Damon" },
                //The Green Mile
                new Actor { Name = "Tom", Surname = "Hanks" },
                new Actor { Name = "Michael", Surname = "Clarke Duncan" },
                new Actor { Name = "David", Surname = "Morse" },
                new Actor { Name = "Bonnie", Surname = "Hunt" },
                new Actor { Name = "James", Surname = "Cromwell" },
                //8 Mile
                new Actor { Name = "Marshall", Surname = "Mathers" },
                new Actor { Name = "Brittany", Surname = "Murphy" },
                new Actor { Name = "Mekhi", Surname = "Phifer" },
                new Actor { Name = "Kim", Surname = "Basinger" },
                new Actor { Name = "Evan", Surname = "Jones" },
                //Do Not Disturb
                new Actor { Name = "Cem", Surname = "Yılmaz" },
                new Actor { Name = "Ahsen", Surname = "Eroglu" },
                new Actor { Name = "Nilperi", Surname = "Sahinkaya" },
                new Actor { Name = "Özge", Surname = "Borak" },
                new Actor { Name = "Bülent", Surname = "Şakrak" },
                //In Time
                new Actor { Name = "Justin", Surname = "Timberlake" },
                new Actor { Name = "Amanda", Surname = "Seyfried" },
                new Actor { Name = "Cillian", Surname = "Murphy" },
                new Actor { Name = "Olivia", Surname = "Wilde" },
                new Actor { Name = "Alex", Surname = "Pettyfer" }
            };
            return actors;
        }

        private static List<Director> GetDirector()
        {
            var directors = new List<Director>()
            {
                new Director()
                {
                    Name="Cem",
                    Surname="Yılmaz"
                },
                new Director()
                {
                    Name="Cristopher",
                    Surname="Nolan"
                },
                new Director()
                {
                    Name="Frank",
                    Surname="Darabont"
                },
                new Director()
                {
                    Name="Andrew",
                    Surname="Niccol"
                },
                new Director()
                {
                    Name="Curtis",
                    Surname="Hanson"
                },
            };
            return directors;
        }

        private static List<Genre> GetGenres()
        {
            var genres = new List<Genre>()
            {
                new Genre()
                {
                    Name="Science Fiction"
                },
                new Genre()
                {
                    Name="Action"
                },
                new Genre()
                {
                    Name="Music"
                },
                new Genre()
                {
                    Name="Drama"
                },
                new Genre()
                {
                    Name="Comedy"
                }
            };
            return genres;
        }
    }
}
