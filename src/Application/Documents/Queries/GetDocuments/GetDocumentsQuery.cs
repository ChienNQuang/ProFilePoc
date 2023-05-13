using System.Collections.ObjectModel;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProFilePOC2.Application.Common.Exceptions;
using ProFilePOC2.Application.Common.Interfaces;
using ProFilePOC2.Application.Common.Models.Dtos;

namespace ProFilePOC2.Application.Documents.Queries.GetDocuments;

public record GetDocumentsQuery : IRequest<IEnumerable<DocumentDto>>
{
    public Guid RoomId { get; init; }
    public Guid LockerId { get; init; }
    public Guid FolderId { get; init; }
}

public class GetDocumentsQueryHandler : IRequestHandler<GetDocumentsQuery, IEnumerable<DocumentDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetDocumentsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<DocumentDto>> Handle(GetDocumentsQuery request, CancellationToken cancellationToken)
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
        
        var folder = await _context.PhysicalFolders.FirstOrDefaultAsync(f => f.Id == request.FolderId, cancellationToken);

        if (folder is null)
        {
            throw new NotFoundException(nameof(folder), request.FolderId);
        }
        
        var list = await _context.Documents.ToListAsync(cancellationToken);

        var result = new ReadOnlyCollection<DocumentDto>(_mapper.Map<List<DocumentDto>>(list));

        return result;
    }
} 