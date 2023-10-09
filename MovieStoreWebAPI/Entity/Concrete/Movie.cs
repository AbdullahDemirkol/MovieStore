﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStoreWebAPI.Entity.Concrete
{
    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime YearOfMovie { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int DirectorId { get; set; }
        public Director Director { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<ActorMovieRelationship> ActorMovieRelationship { get; set; }
    }
}