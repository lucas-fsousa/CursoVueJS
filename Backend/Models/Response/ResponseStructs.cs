using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Models.Response {
  public readonly record struct CategoryDefault(int? Id, string? Name, int? ParentId, string? Path);
  public class CategoryTree {
    public int? Id { get; set; }
    public string? Name { get; set; }
    public int? ParentId { get; set; }
    public string? Path { get; set; }
    public IList<CategoryTree> Children { get; set; }

    public CategoryTree(int? id, string? name, int? parentId, string? path, IList<CategoryTree> children) {
      Id = id;
      Name = name;
      ParentId = parentId;
      Path = path;
      Children = children;
    }
  }

  public class Stat {
    [BsonId]
    public ObjectId _id { get; set; }
    public int Users { get; set; }
    public int Categories { get; set; }
    public int Articles { get; set; }
    public DateTime CreatedAt { get; set; }
  }

  public class StatInput {
    public int Users { get; set; }
    public int Categories { get; set; }
    public int Articles { get; set; }

    [BsonDateTimeOptions]
    public DateTime CreatedAt { get; set; }
  }
}
