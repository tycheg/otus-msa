var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/rewrite-test", async c =>
{
    async Task WriteLine(string? s)
    {
        await c.Response.WriteAsync(s ?? string.Empty);
        await c.Response.WriteAsync("\n");
    }

    await WriteLine("Path:");
    await WriteLine(c.Request.Path.Value);

    await WriteLine("QueryString:");
    await WriteLine(c.Request.QueryString.Value);
    
    await WriteLine("Headers:");
    foreach (var header in c.Request.Headers)
    {
        await WriteLine(header.Key + ": " + header.Value);
    }
});
app.MapGet("/health", () => "{\"status\": \"OK\"}");

app.Run("http://*:8000");
