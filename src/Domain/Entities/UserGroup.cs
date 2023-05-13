namespace ProFilePOC2.Domain.Entities;

public class UserGroup : BaseEntity
{
    public string Name { get; set; }
    public Department Department { get; set; }
}