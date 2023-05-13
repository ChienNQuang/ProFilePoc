using System.ComponentModel.DataAnnotations;

namespace ProFilePOC2.Domain.Entities.Physical;

public class Locker : BaseEntity
{
    public string Name { get; set; }
    public Room Room { get; set; }
}