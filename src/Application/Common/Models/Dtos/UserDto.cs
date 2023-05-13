using AutoMapper;
using ProFilePOC2.Application.Common.Mappings;
using ProFilePOC2.Domain.Entities;

namespace ProFilePOC2.Application.Common.Models.Dtos;

public class UserDto : IMapFrom<User>
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserDto>()
            .ForMember(dest => dest.Role, 
                opt => opt.MapFrom(src => src.Role.Name));
    }
}