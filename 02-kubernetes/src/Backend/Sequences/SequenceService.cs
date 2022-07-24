using Backend;
using MongoDB.Driver;

internal class SequenceService
{
    private readonly IMongoStorage<Sequence, string> _sequences;

    public SequenceService(IMongoStorage<Sequence, string> sequences)
    {
        _sequences = sequences;
    }
    
    public async Task<long> GetSequence(string sequenceName)
    {
        var findOneAndUpdateOptions = new FindOneAndUpdateOptions<Sequence>
        {
            ReturnDocument = ReturnDocument.After, 
            IsUpsert = true
        };
        var sequence = await _sequences.Update(f => f.Eq(x => x.Id, sequenceName), u => u.Inc(x => x.Value, 1), findOneAndUpdateOptions);

        return sequence.Value;
    }
}