namespace Backend.Framework;

internal class ValidationException : Exception
{
    public ValidationException(int code, string message)
        : base(message)
    {
        Code = code;
    }

    public int Code { get; }
}