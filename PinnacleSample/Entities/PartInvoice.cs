namespace PinnacleSample
{
    public class PartInvoice
    {
        public PartInvoice(string stockCode, int quantity, int customerId)
        {
            StockCode = stockCode;
            Quantity = quantity;
            CustomerId = customerId;
        }
        public int Id { get; set; }
        public string StockCode { get; set; }
        public int Quantity { get; set; }
        public int CustomerId { get; set; }
    }
}
