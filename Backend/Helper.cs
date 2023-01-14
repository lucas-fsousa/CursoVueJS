using PublicUtility.Extension;
using System.Text;

namespace Backend.LastProject {
  public static class Helper {

    public static byte[] AsByteArray(this object value) {
      value.ValueOrExeption();
      return Encoding.UTF8.GetBytes(value.ToString());
    }

  }
}
