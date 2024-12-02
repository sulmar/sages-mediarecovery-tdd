namespace TestApp.UnitTests;

[TestClass]
public class LoggerTests
{
    private const string WhiteSpace = " ";
    private const string NotEmptyMessage = "a";

    private Logger logger;

    [TestInitialize]
    public void Init()
    {
        logger = new Logger();
    }
    
    [TestMethod]
    public void Log_MessageIsEmpty_ShouldThrowArgumentNullException()
    {
        // Act
        var act = () => logger.Log(null);

        // Assert
        Assert.ThrowsException<ArgumentNullException>(act);
    }

    [TestMethod]
    public void Log_MessageIsWhiteSpace_ShouldThrowArgumentException()
    {
        // Act
        var act = () => logger.Log(WhiteSpace);
        
        // Assert
        Assert.ThrowsException<ArgumentException>(act);
    }

    [TestMethod]
    public void Log_MessageIsNotEmpty_ShouldSetLastMessage()
    {
        // Act
        logger.Log(NotEmptyMessage);
        
        // Assert
        Assert.AreEqual(NotEmptyMessage, logger.LastMessage);
    }
    
}