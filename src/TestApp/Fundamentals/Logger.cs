using System;

namespace TestApp;

// Testing Void Methods

public class Logger
{
    public string LastMessage { get; set; }

    public void Log(string message)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(message);

        LastMessage = message;

        // Write the log to a storage
        // ...
    }
}
