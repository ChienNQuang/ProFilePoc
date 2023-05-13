using AutoMapper;
using ProFilePOC2.Application.Common.Mappings;
using ProFilePOC2.Domain.Entities.Physical;

namespace ProFilePOC2.Application.Common.Models.Dtos;

public class LockerDto : IMapFrom<Locker>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid RoomId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Locker, LockerDto>()
            .ForMember(dest => dest.RoomId,
                opt => opt.MapFrom(src => src.Room.Id));
    }
}