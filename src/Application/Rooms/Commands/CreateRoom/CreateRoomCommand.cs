using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProFilePOC2.Application.Common.Exceptions;
using ProFilePOC2.Application.Common.Interfaces;
using ProFilePOC2.Application.Common.Mappings;
using ProFilePOC2.Application.Common.Models.Dtos;
using ProFilePOC2.Domain.Entities.Physical;
using ProFilePOC2.Domain.Exceptions;

namespace ProFilePOC2.Application.Rooms.Commands.CreateRoom;

public record CreateRoomCommand : IRequest<RoomDto>
{
    public Guid CreatorId { get; set; }
    public string Name { get; set; }

    internal Room ToEntity()
    {
        return new Room
        {
            Id = Guid.NewGuid(),
            Name = Name
        };
    }
}

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, RoomDto>
{
    private IApplicationDbContext _context;
    private IMapper _mapper;

    public CreateRoomCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<RoomDto> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var creator = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.CreatorId, cancellationToken);

        if (creator is null)
        {
            throw new NotFoundException(nameof(creator), request.CreatorId);
        }

        var duplicateRoom = await _context.Rooms.FirstOrDefaultAsync(r => r.Name.Equals(request.Name), cancellationToken);

        if (duplicateRoom is not null)
        {
            throw new DuplicateNameException($"Room with name {request.Name} already exists!");
        }

        var entity = request.ToEntity();
        
        var room = await _context.Rooms.AddAsync(entity, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<RoomDto>(room.Entity);
    }
}