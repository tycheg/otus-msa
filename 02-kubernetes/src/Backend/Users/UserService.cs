using Backend;
using MongoDB.Driver;

internal class UserService
{
    private readonly IMongoStorage<User, long> _users;

    public UserService(IMongoStorage<User, long> users)
    {
        _users = users;
    }
    
    public async Task CreateUser(long userId, CreateUser createUser)
    {
        var user = new User(userId, createUser.UserName, createUser.FirstName, createUser.LastName, createUser.Email, createUser.Phone);
        await _users.Insert(user);
    }

    public async Task<User?> Get(long id)
    {
        var user = await _users.Get(id);
        return user;
    }
    
    public async Task UpdateUser(long userId, UpdateUser updateUser)
    {
        await _users.Update(f => f.Eq(x => x.Id, userId), u => u
            .Set(x => x.FirstName, updateUser.FirstName)
            .Set(x => x.LastName, updateUser.LastName)
            .Set(x => x.Phone, updateUser.Phone)
            .Set(x => x.Email, updateUser.Email));
    }
    
    public async Task Delete(long id)
    {
        await _users.Delete(id);
    }
}