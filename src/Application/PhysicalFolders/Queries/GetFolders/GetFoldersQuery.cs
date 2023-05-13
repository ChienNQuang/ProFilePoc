using System.Collections.ObjectModel;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProFilePOC2.Application.Common.Exceptions;
using ProFilePOC2.Application.Common.Interfaces;
using ProFilePOC2.Application.Common.Models.Dtos;

namespace ProFilePOC2.Application.PhysicalFolders.Queries.GetFolders;

public record GetFoldersQuery : IRequest<IEnumerable<PhysicalFolderDto>>
{
    public Guid RoomId { get; set; }
    public Guid LockerId { get; set; }
}

public class GetFoldersQueryHandler : IRequestHandler<GetFoldersQuery, IEnumerable<PhysicalFolderDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetFoldersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IEnumerable<PhysicalFolderDto>> Handle(GetFoldersQuery request, CancellationToken cancellationToken)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == request.RoomId, cancellationToken);

        if (room is null)
        {
            throw new NotFoundException(nameof(room), request.RoomId);
        }
        
        var locker = await _context.Lockers.FirstOrDefaultAsync(l => l.Id == request.LockerId, cancellationToken);

        if (locker is null)
        {
            throw new NotFoundException(nameof(locker), request.LockerId);
        }
        
        var list = await _context.PhysicalFolders
            .Include(f => f.Locker)
            .ThenInclude(l => l.Room)
            .Where(f => f.Locker.Id == request.LockerId && f.Locker.Room.Id == request.RoomId)
            .ToListAsync(cancellationToken);
        
        var result = new ReadOnlyCollection<PhysicalFolderDto>(_mapper.Map<List<PhysicalFolderDto>>(list));

        return result;
    }
}