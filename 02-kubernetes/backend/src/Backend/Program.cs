using System.Net.Mime;
using System.Text;
using Backend.Framework;
using Backend.Sequences;
using Backend.Users;

var builder = WebApplication.CreateBuilder(args);
var configuration = Environment.ExpandEnvironmentVariables(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")));
Console.WriteLine(configuration);
builder.Configuration.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(configuration)));

var services = builder.Services;

services.Configure<MongoConfiguration>(builder.Configuration.GetSection("Mongo"));
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddScoped<IMongoStorage<User, long>, MongoStorage<User, long>>();
services.AddScoped<IMongoStorage<Sequence, string>, MongoStorage<Sequence, string>>();
services.AddScoped<UserService>();
services.AddScoped<SequenceService>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/health", () => new {status = "OK"});
app.UseSwagger();

app.MapPost("/users", async (CreateUser createUser, UserService users) =>
{
    if (createUser.UserName.Length > 256)
    {
        return Results.BadRequest(new Error(1, "UserName is longer than 256 characters"));
    }
    
    var userId = await users.CreateUser(createUser);
    return Results.Created($"/users/{userId}", userId);
});

app.MapPut("/users/{userId}", async (long userId, UpdateUser updateUser, UserService users) =>
{
    await users.UpdateUser(userId, updateUser);
});

app.MapGet("/users/{userId}", async (long userId, UserService users) =>
{
    var user = await users.Get(userId);
    return user;
});

app.MapDelete("/users/{userId}", async (long userId, UserService users) =>
{
    await users.Delete(userId);
    return Results.StatusCode(204);
});

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = MediaTypeNames.Text.Plain;
        await context.Response.WriteAsync("500 internal server error");
    });
});

app.UseSwaggerUI();
app.Run("http://*:8000");