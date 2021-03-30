using Moq;
using PinnacleSample;
using PinnacleSample.Interfaces;
using Xunit;

namespace PinnacleSampleTests
{
    public class PinnacleControllerTests
    {
        private readonly Customer _cutomerDetails;
        private readonly Mock<ICustomerRepository> _customerRepository;
        private readonly Mock<IPartInvoiceRepository> _invoiceRepository;
        public PinnacleControllerTests()
        {
            _customerRepository = new Mock<ICustomerRepository>();
            _invoiceRepository = new Mock<IPartInvoiceRepository>();
            _cutomerDetails = new Customer
            {
                Id = 10121,
                Name = "John",
                Address = "UK"
            };
        }
        [Fact]
        public void CreatePartInvoice_ValidStockCodeQuantityCustomerName_ReturnsTrue()
        {
            // Arrange
            Mock<IPartAvailabilityService> partAvailabilityService = new Mock<IPartAvailabilityService>();
            var defaultResult = new CreatePartInvoiceResult(true);

            partAvailabilityService.Setup(s => s.GetAvailability(It.IsAny<string>())).Returns(1);
            _customerRepository.Setup(s => s.GetByName(It.IsAny<string>())).Returns(_cutomerDetails);
            _invoiceRepository.Setup(s => s.Add(It.IsAny<PartInvoice>())).Returns(true);                   
            var pinnacleController = new PartInvoiceController(_customerRepository.Object, _invoiceRepository.Object, partAvailabilityService.Object);
            
            // Act   
            var result = pinnacleController.CreatePartInvoice("N1234", 10, "Ford");

            // Assert
            Assert.Equal<bool>(defaultResult.Success, result.Success);
        }

        [Fact]
        public void CreatePartInvoice_ValidStockCodeQuantityCustomerName_InvoiceInsertionToDatabaseFailed_ReturnsFalse()
        {
            // Arrange
            Mock<IPartAvailabilityService> partAvailabilityService = new Mock<IPartAvailabilityService>();
            var defaultResult = new CreatePartInvoiceResult(true);

            partAvailabilityService.Setup(s => s.GetAvailability(It.IsAny<string>())).Returns(1);
            _customerRepository.Setup(s => s.GetByName(It.IsAny<string>())).Returns(_cutomerDetails);
            _invoiceRepository.Setup(s => s.Add(It.IsAny<PartInvoice>())).Returns(false);
            var pinnacleController = new PartInvoiceController(_customerRepository.Object, _invoiceRepository.Object, partAvailabilityService.Object);

            // Act   
            var result = pinnacleController.CreatePartInvoice("N1234", 10, "Ford");

            // Assert
            Assert.NotEqual<bool>(defaultResult.Success, result.Success);
        }

        [Fact]
        public void CreatePartInvoice_QuantityZero_ReturnsFalse()
        {
            // Arrange 
            var defaultResult = new CreatePartInvoiceResult(true);
            _customerRepository.Setup(s => s.GetByName(It.IsAny<string>())).Returns(_cutomerDetails);
                                
            var pinnacleController = new PartInvoiceController(_customerRepository.Object, _invoiceRepository.Object, null);

            // Act 
            var result = pinnacleController.CreatePartInvoice("N1234", 0, "Ford");

            // Assert
            Assert.NotEqual<bool>(defaultResult.Success, result.Success);
        }

        [Fact]
        public void CreatePartInvoice_StockCodeIsNull_RetrunsFalse()
        {
            // Arrange
            var defaultResult = new CreatePartInvoiceResult(true);
            var pinnacleController = new PartInvoiceController(_customerRepository.Object, _invoiceRepository.Object, null);

            // Act
            var result = pinnacleController.CreatePartInvoice(null, 10, "Ford");

            // Assert
            Assert.NotEqual<bool>(defaultResult.Success, result.Success);
        }

        [Fact]
        public void CreatePartInvoice_StockCodeEmpty_ReturnsFalse()
        {
            // Arrange
            var defaultResult = new CreatePartInvoiceResult(true);                   
            var pinnacleController = new PartInvoiceController(_customerRepository.Object, _invoiceRepository.Object, null);

            // Act   
            var result = pinnacleController.CreatePartInvoice(string.Empty, 10, "Ford");

            // Assert
            Assert.NotEqual<bool>(defaultResult.Success, result.Success);
        }

        [Fact]
        public void CreatePartInvoice_ServiceNotAvailable_ReturnsFalse()
        {
            // Arrange
            Mock<IPartAvailabilityService> partAvailabilityService = new Mock<IPartAvailabilityService>();
            var defaultResult = new CreatePartInvoiceResult(true);

            partAvailabilityService.Setup(s => s.GetAvailability(It.IsAny<string>())).Returns(0);
            _customerRepository.Setup(s => s.GetByName(It.IsAny<string>())).Returns(_cutomerDetails);
            _invoiceRepository.Setup(s => s.Add(It.IsAny<PartInvoice>())).Returns(true);
                     
            var pinnacleController = new PartInvoiceController(_customerRepository.Object, _invoiceRepository.Object, partAvailabilityService.Object);

            // Act 
            var result = pinnacleController.CreatePartInvoice("N1234", 10, "Ford");

            // Assert
            Assert.NotEqual<bool>(defaultResult.Success, result.Success);
        }

        [Fact]
        public void CreatePartInvoice_InvalidCustomer_ReturnsFalse()
        {
            // Arrange
            Mock<IPartAvailabilityService> partAvailabilityService = new Mock<IPartAvailabilityService>();
            var defaultResult = new CreatePartInvoiceResult(true);
            Customer customer = null;
            partAvailabilityService.Setup(s => s.GetAvailability(It.IsAny<string>())).Returns(0);
            _customerRepository.Setup(s => s.GetByName(It.IsAny<string>())).Returns(customer);
            _invoiceRepository.Setup(s => s.Add(It.IsAny<PartInvoice>())).Returns(true);
            var pinnacleController = new PartInvoiceController(_customerRepository.Object, _invoiceRepository.Object, partAvailabilityService.Object);

            // Act
            var result = pinnacleController.CreatePartInvoice("N1234", 10, "Ford1");

            // Assert
            Assert.NotEqual<bool>(defaultResult.Success, result.Success);
        }
    }
}
