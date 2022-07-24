using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Backend.Framework;

internal class MongoStorage<TDocument, TId> : IMongoStorage<TDocument, TId> 
    where TDocument : IDocument<TId>
{
    private readonly IOptions<MongoConfiguration> _configuration;

    public MongoStorage(IOptions<MongoConfiguration> configuration)
    {
        _configuration = configuration;
    }
    
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
            var configuration = _configuration.Value;
            var url = MongoUrl.Create(configuration.ConnectionString);
            var settings = MongoClientSettings.FromUrl(url);
            settings.Credential = MongoCredential.CreateCredential("admin", configuration.UserName, configuration.Password);
            var client = new MongoClient(settings);
            var database = client.GetDatabase(configuration.DatabaseName);
            var collection = database.GetCollection<TDocument>(typeof(TDocument).Name);
            return collection;
        }
    }
}