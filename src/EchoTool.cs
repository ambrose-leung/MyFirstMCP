using ModelContextProtocol.Server;
using System.ComponentModel;

[McpServerToolType]
// Quick and hacky - no separation of Tool definition and Service
public static class EchoTool
{
    [McpServerTool, Description("Echoes the message back to the client.")]
    public static string Echo(string message) => $"Hello from C#: {message}";

    [McpServerTool, Description("Reverses the message sent by the client.")]
    public static string ReverseEcho(string message) => new (message.Reverse().ToArray());
}
