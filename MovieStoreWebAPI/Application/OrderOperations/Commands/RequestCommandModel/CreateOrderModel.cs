namespace MovieStoreWebAPI.Application.OrderOperations.Commands.RequestCommandModel
{
    public class CreateOrderModel
    {
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
    }
}
