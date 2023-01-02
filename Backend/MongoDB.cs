using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PublicUtility.Extension;

namespace Backend.LastProjectA {
  public readonly record struct UpdateField(string FieldName, object FieldValue);

  public partial class MongoDB {
    private readonly IMongoClient _mongoClient;
    private IMongoDatabase _database;
    private IMongoCollection<BsonDocument> _collection;

    private MongoDB(string connectionString) {
      _mongoClient = new MongoClient(connectionString);
    }

    public static MongoDB GetConn(string connectionString) => new(connectionString);

    public void LoadDataAndCollection(string databaseName, string collectionName) {
      _database = _mongoClient.GetDatabase(databaseName.ValueOrExeption());
      _collection = _database.GetCollection<BsonDocument>(collectionName.ValueOrExeption());
    }

    public IEnumerable<T> GetAllFromCollection<T>() where T : class => GetAll<T>();

    public IEnumerable<T> GetFromCollectionByFilter<T>(Func<T, bool> filter) where T : class => GetAll(filter);

    public T GetFirst<T>() where T : class => GetAll<T>().First();

    private IEnumerable<T> GetAll<T>(Func<T, bool> filter = null) where T : class {
      IEnumerable<T> result = null;
      try {
        var find = _collection.Find(new BsonDocument()).ToEnumerable();
        result = find.Select(x => BsonSerializer.Deserialize<T>(x));

        if(filter.IsFilled())
          result = result.Where(filter);

        return result;
      } catch(Exception ex) {

        throw new Exception(ex.Message);
      }
    }

    public void Insert<T>(T value) {
      var doc = BsonDocument.Parse(value.JsonSerialize());
      _collection.InsertOne(doc);
    }

    public void Insert<T>(IEnumerable<T> values) {
      IEnumerable<BsonDocument> docs = values.Select(x => BsonDocument.Parse(x.JsonSerialize()));
      _collection.InsertMany(docs);
    }

    public void Delete(ObjectId objectId) {
      var filter = Builders<BsonDocument>.Filter.Eq("_id", objectId);
      _collection.DeleteOne(filter);
    }

    public void Delete(IEnumerable<ObjectId> objectIds) {
      foreach(var objectId in objectIds) {
        var filter = Builders<BsonDocument>.Filter.Eq("_id", objectId);
        _collection.DeleteOne(filter);
      }
    }

    public void Update(ObjectId objectId, IEnumerable<UpdateField> fields) {
      var filter = Builders<BsonDocument>.Filter.Eq("_id", objectId);
      fields.Select(x => Builders<BsonDocument>.Update.Set(x.FieldName, x.FieldValue)).ToList().ForEach(updateDefinition => {
        _collection.UpdateOne(filter, updateDefinition);
      });
    }

    public void Update(ObjectId objectId, UpdateField field) {
      var filter = Builders<BsonDocument>.Filter.Eq("_id", objectId);
      var updateDefinition = Builders<BsonDocument>.Update.Set(field.FieldName, field.FieldValue);
      _collection.UpdateOne(filter, updateDefinition);
    }


  }
}
