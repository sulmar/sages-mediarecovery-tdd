using System.Security.Cryptography;

namespace TestApp.Mocking;

// Data Transfer Object
public class MeasureDTO 
{
    public double Value { get; set; }
    public string Unit { get; set; }
    
}

public abstract class Base
{
    
}
public abstract class BaseEntity : Base
{
    public int Id { get; set; }
}

// Entity
public class Measure : BaseEntity
{
    public double Value { get; set; }
    public string Unit { get; set; }
    public double ExchangeValue { get; set; }
}


public interface IExchangeRateService
{
    public double GetExchangeRate(string unit); 
}


public interface IMeasureRepository
{
    public void Add(Measure measure);
}

public interface IMessageService
{
    public void Send(string message); 
}


public class MeasureService
{
    private readonly IExchangeRateService? _exchangeRateService; 
    private readonly IMeasureRepository _measureRepository;
    private readonly IMessageService _messageService;
    
    public Measure LastMeasure { get; private set; }

    public MeasureService(IExchangeRateService exchangeRateService, IMeasureRepository measureRepository, IMessageService messageService)
    {
        _exchangeRateService = exchangeRateService;
        _measureRepository = measureRepository;
        _messageService = messageService;
    }
    
    public void Add(MeasureDTO dto)
    {
        var exchangeValue = dto.Value * _exchangeRateService.GetExchangeRate(dto.Unit);

        Measure measure = new Measure
        {
            Unit = dto.Unit,
            Value = dto.Value,
            ExchangeValue = exchangeValue
        };

        _measureRepository.Add(measure);

        _messageService.Send(measure.ToString());
        
        LastMeasure = measure;
    }
}