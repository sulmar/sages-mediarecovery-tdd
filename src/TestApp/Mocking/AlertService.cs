using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace TestApp.Mocking;

public class AlertsModel
{
    public string Title { get; set; }
    public string Message { get; set; }
}

// public class AlertService
// {
//     private readonly AlertRepository _repository;
//     private readonly IMessageService _messageService;
//
//     public AlertService(AlertRepository repository, IMessageService messageService)
//     {
//         _repository = repository;
//         _messageService = messageService;
//     }
//
//     public void ProcessData(int deviceId)
//     {
//         var alerts = _repository.Get(deviceId);
//
//         foreach (var alert in alerts)
//         {
//             _messageService.Send(alert.ToString());
//         }
//     }
// }

public interface IAlertRepository
{
    IEnumerable<AlertsModel> Get(int deviceId);
}

public interface IBtsRepository
{
    
}

public interface IPowerRepository
{
    double GetPower(int btsId, int deviceId);
}

public class DbAlertRepository : IAlertRepository
{
    private readonly IMongoDatabase _db;
    
    private IMongoCollection<AlertsModel> _alertsTable;

    public DbAlertRepository(IMongoDatabase db)
    {
        _db = db;
    }

    public IEnumerable<AlertsModel> Get(int deviceId)
    {
        _alertsTable = _db.GetCollection<AlertsModel>("Alerts");

        var pipelineAlert = new EmptyPipelineDefinition<AlertsModel>();

        var alerts = _alertsTable.Aggregate<AlertsModel>(pipelineAlert).ToList();
        
        return alerts;
    }
}

public class Device
{
    public int Id { get; set; } 
    public string Name { get; set; }
}

public class EmailMessageService : IMessageService
{
    public void Send(string message)
    {
        Console.WriteLine($"Sending message: {message} via Email");
    }
}

public class SmsMessageService : IMessageService
{
    public void Send(string message)
    {
        Console.WriteLine($"Sending message: {message} via Sms");
    }
}

// Wzorzec Kompozycji
public class CompositeMessageService : IMessageService
{
    private readonly IMessageService[] _messageServices;
    public CompositeMessageService(params IMessageService[] messageServices)
    {
        _messageServices = messageServices;
    }
    
    public void Send(string message)
    {
        foreach (var messageService in _messageServices)
        {
            messageService.Send(message);
        }
    }
}

public interface IDeviceRepository
{
    Device Get(int deviceId);    
}

public class AlertService
{
    private readonly IMessageService _messageService;
    private readonly IAlertRepository _repository;
    private readonly IDeviceRepository _deviceRepository;

    public AlertService(
        IMessageService messageService, 
        IAlertRepository alertRepository,
        IDeviceRepository deviceRepository)
    {
        _messageService = messageService;
        _repository = alertRepository;
        _deviceRepository = deviceRepository;
    }
    
    public void SendAlertsToDevice(int deviceId)
    {
        // db
        var alerts = _repository.Get(deviceId);
        
        // logika
        foreach (var alert in alerts)
        {
            var name = _deviceRepository.Get(deviceId)?.Name;
            
            _messageService.Send($"Alert device name {name}: {alert.Message}");
        }

    }
}