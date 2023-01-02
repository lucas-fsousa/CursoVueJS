namespace Models {
  public class User {
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public bool Admin { get; set; }

    public static implicit operator User(UserInput input) {
      return new() {
        Admin = input.Admin,
        Email = input.Email,
        Password = input.Password,
        Name = input.Name,
      };
    }
  }
}
