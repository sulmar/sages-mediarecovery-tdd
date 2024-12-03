using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace TestApp.Fundamentals;

public class Dimensions
{
    public int Width { get; set; }
    public int Height { get; set; }

    public Dimensions(int width, int height)
    {
        Width = width; 
        Height = height;
    }
}

// Abstract Builder
public interface ICommandBuilder
{
    ZplCommandBuilder SetText(string text);
    ZplCommandBuilder SetPosition(int x, int y);
}



// Concrete Builder A
public class ZplCommandBuilder : ICommandBuilder
{
    private readonly StringBuilder _builder = new StringBuilder();
    private readonly Dimensions _dimensions;
    
    private ZplCommandBuilder(int width, int height)
    {
        _dimensions = new Dimensions(width, height);

        StartLabel();
    }

    private void StartLabel()
    {
        _builder.Append("^XA");
    }

    public static ZplCommandBuilder CreateLabel(int width, int height)
    {
        return new ZplCommandBuilder(width, height);
    }

    public ZplCommandBuilder SetText(string text)
    {
        if (string.IsNullOrEmpty(text))
            throw new ArgumentNullException(nameof(text));
        
        _builder.Append($"^FD{text}^FS");
        
        return this;
    }
        
    public ZplCommandBuilder SetPosition(int x, int y)
    {
        if (x < 0 || x > _dimensions.Width)
            throw new ArgumentOutOfRangeException(nameof(x));
        
        if (y < 0 || y > _dimensions.Height)
            throw new ArgumentOutOfRangeException(nameof(y));
        
        _builder.Append($"^FO{x},{y}");

        return this;
    }

    public ZplCommandBuilder SetBarcode(string barcode)
    {
        _builder.Append($"^B3N,N,100,Y,N");
        SetText(barcode);

        return this;
    }
    
    public string Build()
    {
        EndLabel();

        return _builder.ToString();
    }

    private void EndLabel()
    {
        _builder.Append("^XZ");
    }
}

public interface IZplPrinter
{
    void Print(string content);
}

public class ConsoleZplPrinter : IZplPrinter
{
    public void Print(string content)
    {
        Console.WriteLine(content);
    }
}

public class TcpZplPrinter : IZplPrinter
{
    private readonly string ipAddress;
    private readonly int port;

    public TcpZplPrinter(string ipAddress, int port)
    {
        this.ipAddress = ipAddress;
        this.port = port;
    }

    public void Print(string content)
    {
        TcpClient tcpClient = new TcpClient();
        tcpClient.Connect(ipAddress, port);

        var stream = new StreamWriter(tcpClient.GetStream());
        stream.Write(content);
        stream.Flush(); 
        stream.Close();
        tcpClient.Close();
    }
}

// Concrete Builder B
public class EplCommandBuilder : ICommandBuilder
{
    public ZplCommandBuilder SetText(string text)
    {
        throw new NotImplementedException();
    }

    public ZplCommandBuilder SetPosition(int x, int y)
    {
        throw new NotImplementedException();
    }
}