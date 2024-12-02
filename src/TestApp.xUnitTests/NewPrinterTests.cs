using TestApp.Fundamentals;

namespace TestApp.xUnitTests;

public class NewPrinterTests
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

    [Fact]
    public void Print_WhenDocumentIsEmpty_ShouldThrowArgumentNullException()
    {
        // Arrange
        NewPrinter printer = new();

        // Act
        Action act = () => printer.Print(string.Empty, 1);

        // Assert
        Assert.Throws<ArgumentNullException>(act);
    }

    // [InlineData(-1)]
    // [InlineData(0)]


    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void Print_NumberOfCopiesLessThan1_ShouldThrowArgumentException(int numberOfCopies)
    {
        // Arrange
        NewPrinter printer = new();

        CounterTextWriter stringWriter = new CounterTextWriter();
        Console.SetOut(stringWriter);

        // Act
        Action act = () => printer.Print(DocumentNotEmpty, numberOfCopies);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    public void Print_NumberOfCopies_ShouldPrintCorrectNumberOfCopies(int numberOfCopies)
    {
        // Arrange
        NewPrinter printer = new();

        CounterTextWriter stringWriter = new CounterTextWriter();
        Console.SetOut(stringWriter);

        // Act
        printer.Print(DocumentNotEmpty, numberOfCopies);

        // Assert
        Assert.Equal(numberOfCopies, stringWriter.Copies);
    }
}