using AutoMapper;
using ProFilePOC2.Application.Common.Mappings;
using ProFilePOC2.Domain.Entities.Physical;

namespace ProFilePOC2.Application.Common.Models.Dtos;

public class PhysicalFolderDto : IMapFrom<PhysicalFolder>
{
    public Guid Id { get; set; }
    public Guid LockerId { get; set; }
    public Guid RoomId { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PhysicalFolder, PhysicalFolderDto>()
            .ForMember(dest => dest.LockerId,
                opt => opt.MapFrom(src => src.Locker.Id))
            .ForMember(dest => dest.RoomId,
                opt => opt.MapFrom(src => src.Locker.Room.Id));
    }
}