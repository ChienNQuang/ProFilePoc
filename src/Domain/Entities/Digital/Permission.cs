using ProFilePOC2.Domain.Enums;
using ProFilePOC2.Domain.Enums.Digital;

namespace ProFilePOC2.Domain.Entities.Digital;

public class Permission : BaseAuditableEntity
{
    public User User { get; set; }
    public Node Node { get; set; }
    public AccessLevel AccessLevel { get; set; }
}