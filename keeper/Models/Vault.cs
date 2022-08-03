namespace keeper.Models
{
  public class Vault
  {
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public bool? isPrivate { get; set; }
    public string img { get; set; }
    public string creatorId { get; set; }
    public Account creator { get; set; }
  }
}