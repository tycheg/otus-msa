namespace Backend.Framework;

internal interface IDocument<TId>
{
    TId Id { get; }
}