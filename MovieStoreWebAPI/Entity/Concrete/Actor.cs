using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStoreWebAPI.Entity.Concrete
{
    public class Actor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<ActorMovie> Movies { get; set; }
    }
}
