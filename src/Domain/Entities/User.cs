using ProFilePOC2.Domain.ValueObjects;

namespace ProFilePOC2.Domain.Entities;

public class User : BaseAuditableEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Role Role { get; set; }
    public string Position { get; set; }
}