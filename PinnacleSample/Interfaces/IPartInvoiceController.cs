using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinnacleSample.Interfaces
{
    public interface IPartInvoiceController
    {
        CreatePartInvoiceResult CreatePartInvoice(string stockCode, int quantity, string customerName);

        bool CheckPartAvailability(string stockCode);
    }
}
