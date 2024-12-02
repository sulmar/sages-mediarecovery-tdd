using System;

namespace TestApp.Fundamentals;

public interface IPrinterAdapter
{
    void Print(string document, int copies);
}

// Wzorzec Adapter
public class LegacyPrinterAdapter : IPrinterAdapter
{
    private readonly LegacyPrinter _printer;

    public LegacyPrinterAdapter()
    {
        _printer = new LegacyPrinter();
    }
    
    public void Print(string document, int copies)
    {
        if (string.IsNullOrWhiteSpace(document))
            throw new ArgumentException(nameof(document)); 
        
        for (int i = 0; i < copies; i++)
        {
            _printer.Print(document);
        }
    }
}

public sealed class LegacyPrinter 
{
    public void Print(string document)
    {
        Console.WriteLine($"Printer is printing... {document}");
    }
}


public class NewPrinter : IPrinterAdapter
{
    public void Print(string document, int copies)
    {
        if (string.IsNullOrEmpty(document))
            throw new ArgumentNullException(nameof(document));
        
        if (copies < 1)
            throw new ArgumentException(nameof(copies));
        
        for (int i = 0; i < copies; i++)
        {
            Console.WriteLine($"Printer is printing... {document}");
        }
    }
}

// TODO: Utwórz nową klasę Printer, która umożliwi drukowanie dokumentu w określonej ilości kopii
// Dostosuj starą drukarkę ale bez modyfikacji jej kodu!

