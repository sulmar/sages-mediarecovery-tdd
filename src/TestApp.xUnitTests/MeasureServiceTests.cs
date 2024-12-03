using FluentAssertions;
using Moq;
using TestApp.Mocking;

namespace TestApp.xUnitTests;

public class MeasureServiceTests
{
    [Fact]
    public void Add_ValidMeasureDTO_ShouldPass()
    {
        // Arrange
        Mock<IExchangeRateService> mockExchangeRateService = new Mock<IExchangeRateService>();
        mockExchangeRateService
            .Setup(rt => rt.GetExchangeRate(It.IsAny<string>()))
            .Returns(4);
            
        IExchangeRateService exchangeRateService = mockExchangeRateService.Object;
        
        Mock<IMeasureRepository> mockMeasureRepository = new Mock<IMeasureRepository>();
        IMeasureRepository measureRepository = mockMeasureRepository.Object;
        
        Mock<IMessageService> mockMessageService = new Mock<IMessageService>();
        IMessageService messageService = mockMessageService.Object;
        
        MeasureService measureService = new MeasureService(exchangeRateService, measureRepository, messageService);
        
        // Act
        measureService.Add(new MeasureDTO() { Value = 10, Unit = "EUR"  } );
        
        // Assert
         measureService.LastMeasure.ExchangeValue.Should().Be(40);
        

    }
}