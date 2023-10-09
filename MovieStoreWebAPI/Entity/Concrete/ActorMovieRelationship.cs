using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStoreWebAPI.Entity.Concrete
{
    public class ActorMovieRelationship
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ActorId { get; set; }
        public Actor Actor { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
