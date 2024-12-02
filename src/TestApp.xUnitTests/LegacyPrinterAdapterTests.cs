using TestApp.Fundamentals;

namespace TestApp.xUnitTests;

public class LegacyPrinterAdapterTests
{
    private class CounterTextWriter : StringWriter
    {
        public int Copies;

        public override void WriteLine(string format)
        {
            Copies++;
        }
    }

    private const string DocumentNotEmpty = "a";

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Print_WhenDocumentIsEmpty_ShouldThrowArgumentException(string document)
    {
        // Arrange
        IPrinterAdapter printer = new LegacyPrinterAdapter();

        // Act
        Action act = () => printer.Print(string.Empty, 1);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    public void Print_NumberOfCopies_ShouldPrintCorrectNumberOfCopies(int numberOfCopies)
    {
        // Arrange
        IPrinterAdapter printer = new LegacyPrinterAdapter();

        CounterTextWriter stringWriter = new CounterTextWriter();
        Console.SetOut(stringWriter);

        // Act
        printer.Print(DocumentNotEmpty, numberOfCopies);

        // Assert
        Assert.Equal(numberOfCopies, stringWriter.Copies);
    }
}