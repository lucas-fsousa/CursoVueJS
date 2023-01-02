using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models {
  public class Article {
    public int? Id { get; set; }
    public User? User { get; set; }
    public Category? Category { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public byte[]? Content { get; set; }
    public int? UserId { get; set; }
    public int? CategoryId { get; set; }

    public static implicit operator Article(ArticleInput input) {
      return new() {
        Name = input.Name,
        Description = input.Description,
        ImageUrl = input.ImageUrl,
        Content = input.Content,
        UserId = input.UserId,
        CategoryId = input.CategoryId
      };
    }
  }
}
