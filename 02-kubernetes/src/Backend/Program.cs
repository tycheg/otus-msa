using Backend;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMongoStorage<User, long>, MongoStorage<User, long>>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/health", () => new {status = "OK"});
app.UseSwagger();

app.MapPost("/users", async (CreateUser createUser, IMongoStorage<User, long> users) =>
{
    var id = 123;
    var user = new User(id, createUser.UserName, createUser.FirstName, createUser.LastName, createUser.Email, createUser.Phone);
    await users.Insert(user);
});

app.MapPut("/users/{userId}", async (long userId, UpdateUser updateUser, IMongoStorage<User, long> users) =>
{
    await users.Update(f => f.Eq(x => x.Id, userId), u => u
        .Set(x => x.FirstName, updateUser.FirstName)
        .Set(x => x.LastName, updateUser.LastName)
        .Set(x => x.Phone, updateUser.Phone)
        .Set(x => x.Email, updateUser.Email));
});

app.MapGet("/users/{userId}", async (long userId, IMongoStorage<User, long> users) =>
{
    var user = await users.Get(userId);
    return user;
});

app.MapDelete("/users/{userId}", async (long userId, IMongoStorage<User, long> users) =>
{
    await users.Delete(userId);
    return Results.StatusCode(204);
});


app.UseSwaggerUI();
app.Run("http://*:8000");