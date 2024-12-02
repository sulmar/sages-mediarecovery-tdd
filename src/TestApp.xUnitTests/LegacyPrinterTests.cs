using System.Text;
using TestApp.Fundamentals;

namespace TestApp.xUnitTests;

public class LegacyPrinterTests
{
    private const string DocumentNotEmpty = "a";
    
    private class ConsoleTextWriter : StringWriter
    {
        public bool IsCalled = false;
    
        public override void WriteLine(string format)
        {
            IsCalled = true;
        }
    }
    
    [Fact]
    public void Print_WhenDocumentIsNotEmpty_ShouldCalledConsoleWriteLine()
    {
        // Arrange
        LegacyPrinter printer = new();

        ConsoleTextWriter stringWriter = new ConsoleTextWriter();
        Console.SetOut(stringWriter);
        
        // Act
        printer.Print(DocumentNotEmpty);
        
        // Assert
        Assert.True(stringWriter.IsCalled);
    }
}