namespace Backend.Framework;

internal class ValidationException : Exception
{
    public ValidationException(int code, string message)
    {
    }
}