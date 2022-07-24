namespace Backend;

internal interface IDocument<TId>
{
    TId Id { get; }
}