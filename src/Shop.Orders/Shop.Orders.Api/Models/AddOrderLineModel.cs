namespace Shop.Orders.Api.Models
{
    public class AddOrderLineModel
    {
        public string Sku { get; set; }
        public int Quanity { get; set; }
        public double ItemPrice { get; set; }
    }
}
