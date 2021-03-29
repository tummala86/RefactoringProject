using Moq;
using PinnacleSample;
using PinnacleSample.Interfaces;
using Xunit;

namespace PinnacleSampleTests
{

    public class PinnacleClientTests
    {
        [Fact]
        public void CreatePartInvoice_WithStockCodeQuantityCustomerName_Returnstrue()
        {
            // Arrange
            Mock<IPartInvoiceController> partInvoiceController = new Mock<IPartInvoiceController>();
            var defaultResult = new CreatePartInvoiceResult(true);
            partInvoiceController.Setup(s => s.CreatePartInvoice(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(defaultResult);

            // Act
            var pinnacleClient = new PinnacleClient(partInvoiceController.Object);

            var result = pinnacleClient.CreatePartInvoice("N1234", 10, "Ford");

            // Assert
            Assert.Equal<bool>(defaultResult.Success, result.Success);
        }

        [Fact]
        public void CreatePartInvoice_WhenStockCodeEmpty_ReturnsFalse()
        {
            // Arrange            
            Mock<ICustomerRepository> customerRepository = new Mock<ICustomerRepository>();
            Mock<IPartInvoiceRepository> invoiceRepository = new Mock<IPartInvoiceRepository>();
            Mock<IPartAvailabilityService> partAvailabilityService = new Mock<IPartAvailabilityService>();
            var defaultResult = new CreatePartInvoiceResult(true);

            // Act          
            var pinnacleController = new PartInvoiceController(customerRepository.Object, invoiceRepository.Object, partAvailabilityService.Object);
            var pinnacleClient = new PinnacleClient(pinnacleController);

            var result = pinnacleController.CreatePartInvoice(string.Empty, 10, "Ford");

            // Assert
            Assert.NotEqual<bool>(defaultResult.Success, result.Success);
        }

    }
}
