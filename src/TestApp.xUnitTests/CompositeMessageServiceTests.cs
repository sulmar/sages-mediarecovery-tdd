using TestApp.Mocking;

namespace TestApp.xUnitTests;

public class CompositeMessageServiceTests
{
    public void Test()
    {
        IMessageService emailMessageService = new EmailMessageService();
        IMessageService smsMessageService = new SmsMessageService();
        
        CompositeMessageService compositeMessageService = new CompositeMessageService(emailMessageService, smsMessageService);
        
        compositeMessageService.Send("a");
    }
}