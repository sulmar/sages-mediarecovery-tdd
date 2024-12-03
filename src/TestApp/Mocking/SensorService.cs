using System.Security.Cryptography;
using Riok.Mapperly.Abstractions;

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

// mapperly

[Mapper]
public partial class Mapper
{
    public partial Measure Map(MeasureDTO dto);
}

// public class Mapper
// {
//     public Measure Map(MeasureDTO dto)
//     {
//         return new Measure
//         {
//             Unit = dto.Unit,
//             Value = dto.Value,
//         };
//     }
// }

public class MeasureService
{
    private readonly IExchangeRateService? _exchangeRateService; 
    private readonly IMeasureRepository _measureRepository;
    private readonly IMessageService _messageService;
    private readonly Mapper _mapper;

    public Measure LastMeasure { get; private set; }

    public MeasureService(
        IExchangeRateService exchangeRateService, 
        IMeasureRepository measureRepository, 
        IMessageService messageService,
        Mapper mapper)
    {
        _exchangeRateService = exchangeRateService;
        _measureRepository = measureRepository;
        _messageService = messageService;
        _mapper = mapper;
    }
    
    public void Add(MeasureDTO dto)
    {
        var exchangeValue = dto.Value * _exchangeRateService.GetExchangeRate(dto.Unit);

        Measure measure = _mapper.Map(dto);
        measure.Value = exchangeValue;

        _measureRepository.Add(measure);

        _messageService.Send(measure.ToString());
        
        LastMeasure = measure;
    }

    
}