using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProFilePOC2.Application.Common.Exceptions;
using ProFilePOC2.Application.Common.Interfaces;
using ProFilePOC2.Application.Common.Models.Dtos;
using ProFilePOC2.Domain.Entities.Physical;
using ProFilePOC2.Domain.Exceptions;

namespace ProFilePOC2.Application.Lockers.Commands.CreateLocker;

public record CreateLockerCommand : IRequest<LockerDto>
{
    public Guid CreatorId { get; init; }
    public Guid? RoomId { get; set; }
    public string Name { get; init; }

    internal Locker ToEntity()
    {
        return new Locker
        {
            Id = Guid.NewGuid(),
            Name = Name
        };
    }
}

public class CreateLockerCommandHandler : IRequestHandler<CreateLockerCommand, LockerDto>
{
    private IApplicationDbContext _context;
    private IMapper _mapper;

    public CreateLockerCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LockerDto> Handle(CreateLockerCommand request, CancellationToken cancellationToken)
    {
        var creator = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.CreatorId, cancellationToken);

        if (creator is null)
        {
            throw new NotFoundException(nameof(creator), request.CreatorId);
        }
        
        var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == request.RoomId, cancellationToken);

        if (room is null)
        {
            throw new NotFoundException(nameof(room), request.RoomId!);
        }

        var duplicateLocker = await _context.Lockers
            .FirstOrDefaultAsync(l => l.Name.Equals(request.Name) 
                                      && l.Room.Id == request.RoomId, cancellationToken);

        if (duplicateLocker is not null)
        {
            throw new DuplicateNameException($"Locker with name {request.Name} already exists!");
        }

        var entity = request.ToEntity();
        entity.Room = room;
        
        var locker = await _context.Lockers.AddAsync(entity, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<LockerDto>(locker.Entity);
    }
}