﻿namespace MovieStoreWebAPI.Application.MovieOperations.Commands.RequestCommandModel
{
    public class UpdateMovieModel
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int DirectorId { get; set; }
        public int GenreId { get; set; }
        public bool IsActive { get; set; }
    }
}
