using PinnacleSample.Interfaces;

namespace PinnacleSample
{
    public class PartInvoiceController : IPartInvoiceController
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IPartInvoiceRepository _invoiceRepository;
        private readonly IPartAvailabilityService _partAvailabilityService;
        public PartInvoiceController(ICustomerRepository customerRepository, IPartInvoiceRepository invoiceRepository, IPartAvailabilityService partAvailabilityService)
        {
            _customerRepository = customerRepository;
            _invoiceRepository = invoiceRepository;
            _partAvailabilityService = partAvailabilityService;
        }
        public CreatePartInvoiceResult CreatePartInvoice(string stockCode, int quantity, string customerName)
        {
            if (!string.IsNullOrEmpty(stockCode) && quantity > 0 && CheckPartAvailability(stockCode))
            {
                var customer = _customerRepository.GetByName(customerName);
                if (customer?.Id > 0)
                {
                    var partInvoice = new PartInvoice(stockCode, quantity, customer.Id);
                    var results = _invoiceRepository.Add(partInvoice);
                    return  new CreatePartInvoiceResult(results);
                }
            }
            return new CreatePartInvoiceResult(false);
        }

        public bool CheckPartAvailability(string stockCode)
        => _partAvailabilityService.GetAvailability(stockCode) > 0;
    }
}
