namespace ProFilePOC2.Domain.Entities.Digital;

public class Node : BaseAuditableEntity
{
    public string Name { get; set; }
    public User Owner { get; set; }
}