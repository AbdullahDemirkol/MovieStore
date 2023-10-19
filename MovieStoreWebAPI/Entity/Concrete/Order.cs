using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStoreWebAPI.Entity.Concrete
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime TransactionTime { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}