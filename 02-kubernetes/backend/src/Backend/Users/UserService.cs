using Backend.Framework;
using Backend.Sequences;
using MongoDB.Driver;

namespace Backend.Users;

internal class UserService
{
    private readonly IMongoStorage<User, long> _users;
    private readonly SequenceService _sequences;
    
    public UserService(IMongoStorage<User, long> users, SequenceService sequences)
    {
        _users = users;
        _sequences = sequences;
    }
    
    public async Task<long> CreateUser(CreateUser createUser)
    {
        var userId = await _sequences.GetSequence("User");
        var user = new User(userId, createUser.UserName, createUser.FirstName, createUser.LastName, createUser.Email, createUser.Phone);
        await _users.Insert(user);
        return userId;
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