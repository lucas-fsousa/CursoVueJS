using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models {
  public class Category {
    public int? Id { get; set; }
    public string? Name { get; set; }
    public List<Category>? SubCategorys { get; set; }
    public int? CategoryId { get; set; }

    public static implicit operator Category(CategoryInput input) {
      return new() {
        Name = input.Name,
        CategoryId = input.SubCategoryId
      };
    }
  }
}
