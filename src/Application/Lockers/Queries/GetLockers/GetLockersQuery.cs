using System.Collections.ObjectModel;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProFilePOC2.Application.Common.Exceptions;
using ProFilePOC2.Application.Common.Interfaces;
using ProFilePOC2.Application.Common.Models.Dtos;

namespace ProFilePOC2.Application.Lockers.Queries.GetLockers;

public record GetLockersQuery : IRequest<IEnumerable<LockerDto>>
{
    public Guid RoomId { get; init; }
}

public class GetLockersQueryHandler : IRequestHandler<GetLockersQuery, IEnumerable<LockerDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetLockersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IEnumerable<LockerDto>> Handle(GetLockersQuery request, CancellationToken cancellationToken)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == request.RoomId, cancellationToken);

        if (room is null)
        {
            throw new NotFoundException(nameof(room), request.RoomId);
        }
        
        var list = await _context.Lockers.Where(l => l.Room.Id == request.RoomId)
                                        .Include(l => l.Room)
                                        .ToListAsync(cancellationToken);
        
        var result = new ReadOnlyCollection<LockerDto>(_mapper.Map<List<LockerDto>>(list));

        return result;
    }
} 