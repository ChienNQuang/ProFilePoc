using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Xml.XPath;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProFilePOC2.Application.Common.Interfaces;
using ProFilePOC2.Application.Common.Models.Dtos;

namespace Microsoft.Extensions.DependencyInjection.Rooms.Queries.GetRooms;

public record GetRoomsQuery : IRequest<IEnumerable<RoomDto>>
{
    
}

public class GetRoomsQueryHandler : IRequestHandler<GetRoomsQuery, IEnumerable<RoomDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetRoomsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoomDto>> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.Rooms.ToListAsync(cancellationToken);

        var result = new ReadOnlyCollection<RoomDto>(_mapper.Map<List<RoomDto>>(list));

        return result;
    }
}