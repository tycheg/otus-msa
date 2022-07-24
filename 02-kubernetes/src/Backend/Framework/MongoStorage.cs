using MongoDB.Driver;
namespace Backend;

internal class MongoStorage<TDocument, TId> : IMongoStorage<TDocument, TId> 
    where TDocument : IDocument<TId>
{
    public async Task Insert(TDocument document)
    {
        await Collection.InsertOneAsync(document);
    }

    public async Task<TDocument> Update(
        Func<FilterDefinitionBuilder<TDocument>, FilterDefinition<TDocument>> filter,
        Func<UpdateDefinitionBuilder<TDocument>, UpdateDefinition<TDocument>> update,
        FindOneAndUpdateOptions<TDocument>? options = null
    )
    {
        var result = await Collection.FindOneAndUpdateAsync(filter(Builders<TDocument>.Filter), update(Builders<TDocument>.Update), options);
        return result;
    }

    public async Task<TDocument?> Get(TId id)
    {
        var findResult = await Collection.FindAsync(Builders<TDocument>.Filter.Eq(x => x.Id, id));
        var list = await findResult.ToListAsync();
        return list.FirstOrDefault();
    }

    public async Task Delete(TId id)
    {
        await Collection.DeleteOneAsync(Builders<TDocument>.Filter.Eq(x => x.Id, id));
    }

    private IMongoCollection<TDocument> Collection
    {
        get
        {
            var client = new MongoClient("mongodb://docker:docker@localhost:27023");
            var database = client.GetDatabase("otus");
            var collection = database.GetCollection<TDocument>(typeof(TDocument).Name);
            return collection;
        }
    }
}