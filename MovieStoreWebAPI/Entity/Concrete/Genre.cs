﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStoreWebAPI.Entity.Concrete
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAvtive { get; set; } = true;
    }
}