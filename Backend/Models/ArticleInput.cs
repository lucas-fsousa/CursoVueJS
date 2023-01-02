using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models {
  public class ArticleInput {
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public byte[]? Content { get; set; }
    public int? UserId { get; set; }
    public int? CategoryId { get; set; }
  }
}
