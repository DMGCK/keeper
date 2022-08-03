namespace keeper.Models
{
  public class Creator
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Picture { get; set; }

  }
  public class Account : Creator
  {
    public string Email { get; set; }
  }
}