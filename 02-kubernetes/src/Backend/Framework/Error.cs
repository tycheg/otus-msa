using System.Text.Json.Serialization;

internal record Error([property:JsonPropertyName("code")]int Code, [property:JsonPropertyName("message")]string Message);