namespace Shop.Orders.Application.Domain
{
    public class OrderLine
    {
        public string Sku { get; }
        public int Quanity { get; private set; }
        public double ItemPrice { get; }

        public OrderLine(string sku, int quanity, double itemPrice)
        {
            Sku = sku;
            Quanity = quanity;
            ItemPrice = itemPrice;
        }

        public void ChangeQuantity(int quanity)
        {
            Quanity = quanity;
        }
    }
}
