namespace WebShop.Enums
{
    public enum Status
    {
        Created = 0,
        AwaitingPayment = 5,
        Paid = 10,
        Processing = 15,
        Shipped = 20,
        Delivered = 25,
        Cancelled = 30,
        Refunded = 35
    }
}
