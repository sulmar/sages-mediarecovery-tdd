using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using TestApp.Mocking;

namespace TestApp.xUnitTests;

public class AlertServiceTests
{
    private Mock<IMessageService> mockMessageService;
    private Mock<IAlertRepository> mockAlertRepository;
    private Mock<IDeviceRepository> mockDeviceRepository;
    
    private AlertService sut;
    
    public AlertServiceTests()
    {
        mockMessageService = new Mock<IMessageService>();
        mockAlertRepository = new Mock<IAlertRepository>();
        mockDeviceRepository = new Mock<IDeviceRepository>();

        IAggregateResultService aggregateResultService =
            new AggregateResultService(mockAlertRepository.Object, mockDeviceRepository.Object);
        
        sut = new AlertService(aggregateResultService, mockMessageService.Object);
    }
    
    [Fact]
    public void SendAlertsToDevice_ValidDeviceId_ShouldSendMessages()
    {
        // Arrange
        mockMessageService
            .Setup(ms => ms.Send(It.IsAny<string>()));

        mockAlertRepository
            .Setup(ar => ar.Get(It.IsAny<int>()))
            .Returns(Enumerable.Range(1, 3).Select(_ => new AlertsModel { Message = "a" }));
        
        mockDeviceRepository
            .Setup(dr => dr.Get(It.IsAny<int>()))
            .Returns(new Device { Name = "d" });
        
        // Act
        sut.SendAlertsToDevice(1);
        
        // Assert
        mockMessageService.Verify(ms => ms.Send(It.IsAny<string>()), Times.Exactly(3));
        
    }
}