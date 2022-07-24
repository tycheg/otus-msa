using System.Text.Json.Serialization;
using Backend.Framework;

namespace Backend.Users;

internal record User(
    [property:JsonPropertyName("id")]
    long Id, 
    [property:JsonPropertyName("username")]
    string UserName, 
    [property:JsonPropertyName("firstName")]
    string FirstName, 
    [property:JsonPropertyName("lastName")]
    string LastName, 
    [property:JsonPropertyName("email")]
    string Email, 
    [property:JsonPropertyName("phone")]
    string Phone) : IDocument<long>;