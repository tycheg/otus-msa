using Backend;

internal record Sequence(string Id, long Value) : IDocument<string>;