namespace keeper.Models
{
  public class VaultKeep
  {
    public int id { get; set; }
    public int? keepId { get; set; }
    public int? vaultId { get; set; }
    public string creatorId { get; set; }
  }
}