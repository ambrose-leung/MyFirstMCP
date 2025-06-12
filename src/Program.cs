using MyFirstMCP;
using ModelContextProtocol;
using ModelContextProtocol.Server;

//https://devblogs.microsoft.com/dotnet/build-a-model-context-protocol-mcp-server-in-csharp

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole(consoleLogOptions =>
{
    // Configure all logs to go to stderr
    consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});

// Add MCP server services with HTTP transport
builder.Services
    .AddHttpClient()
    .AddSingleton<MonkeyService>()
    .AddMcpServer()
    //.WithStdioServerTransport()
    .WithHttpTransport()
    .WithToolsFromAssembly();

// Add CORS for HTTP transport support in browsers
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Enable CORS
app.UseCors();

// Map MCP endpoints
app.MapMcp();

// Add a simple home page
app.MapGet("/status", () => "MCP Server on Azure App Service - Ready for use with HTTP transport");

app.Run();