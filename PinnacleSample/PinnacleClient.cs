using PinnacleSample.Interfaces;

namespace PinnacleSample
{
    public class PinnacleClient
    {
        private readonly IPartInvoiceController _partInvoiceController;
     
        public PinnacleClient(IPartInvoiceController partInvoiceController)
        {
            _partInvoiceController = partInvoiceController;
        }

        public CreatePartInvoiceResult CreatePartInvoice(string stockCode, int quantity, string customerName)
        {
            return _partInvoiceController.CreatePartInvoice(stockCode, quantity, customerName);
        }
    }
}
