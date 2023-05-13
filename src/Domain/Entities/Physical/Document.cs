using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProFilePOC2.Domain.Enums.Physical;

namespace ProFilePOC2.Domain.Entities.Physical;

public class Document : BaseEntity
{
    public string Name { get; set; }
    public Department Department { get; set; }
    public User Owner { get; set; }
    public PhysicalFolder PhysicalFolder { get; set; }
    public DocumentState State { get; set; }
    public bool IsPrivate { get; set; }
}