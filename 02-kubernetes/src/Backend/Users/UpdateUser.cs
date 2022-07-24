using System.Text.Json.Serialization;

namespace Backend;

record UpdateUser (
    [property:JsonPropertyName("firstName")]
    string FirstName, 
    [property:JsonPropertyName("lastName")]
    string LastName, 
    [property:JsonPropertyName("email")]
    string Email, 
    [property:JsonPropertyName("phone")]
    string Phone);