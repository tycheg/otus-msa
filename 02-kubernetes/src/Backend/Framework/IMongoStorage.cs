using MongoDB.Driver;
namespace Backend;
internal interface IMongoStorage<TDocument, TId> where TDocument : IDocument<TId>
{
    Task Insert(TDocument document);
    Task<TDocument?> Get(TId id);
    Task<TDocument> Update(Func<FilterDefinitionBuilder<TDocument>, FilterDefinition<TDocument>> filter,
        Func<UpdateDefinitionBuilder<TDocument>, UpdateDefinition<TDocument>> update,
        FindOneAndUpdateOptions<TDocument>? options = null);
    Task Delete(TId id);
}