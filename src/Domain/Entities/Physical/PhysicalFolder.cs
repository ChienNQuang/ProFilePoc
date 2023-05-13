using System.ComponentModel.DataAnnotations;

namespace ProFilePOC2.Domain.Entities.Physical;

public class PhysicalFolder : BaseEntity
{
    public string Name { get; set; }
    public Locker Locker { get; set; }
    public int Capacity { get; set; }
}