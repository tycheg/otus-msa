using Backend.Framework;

namespace Backend.Sequences;

internal record Sequence(string Id, long Value) : IDocument<string>;