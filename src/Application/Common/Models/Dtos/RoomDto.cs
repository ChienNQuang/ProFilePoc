using ProFilePOC2.Application.Common.Mappings;
using ProFilePOC2.Domain.Entities.Physical;

namespace ProFilePOC2.Application.Common.Models.Dtos;

public class RoomDto : IMapFrom<Room>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}