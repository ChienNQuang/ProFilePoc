using ProFilePOC2.Application.Common.Mappings;
using ProFilePOC2.Domain.Entities;

namespace ProFilePOC2.Application.Common.Models.Dtos;

public class RoleDto : IMapFrom<Role>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}