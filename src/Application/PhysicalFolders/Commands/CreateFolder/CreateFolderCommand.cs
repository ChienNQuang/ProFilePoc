using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProFilePOC2.Application.Common.Exceptions;
using ProFilePOC2.Application.Common.Interfaces;
using ProFilePOC2.Application.Common.Models.Dtos;
using ProFilePOC2.Domain.Entities.Physical;
using ProFilePOC2.Domain.Exceptions;

namespace ProFilePOC2.Application.PhysicalFolders.Commands.CreateFolder;

public class CreateFolderCommand : IRequest<PhysicalFolderDto>
{
    public Guid CreatorId { get; init; }
    public Guid? LockerId { get; set; }
    public Guid? RoomId { get; set; }
    public string Name { get; init; }

    internal PhysicalFolder ToEntity()
    {
        return new PhysicalFolder
        {
            Id = Guid.NewGuid(),
            Name = Name
        };
    }
}

public class CreateFolderCommandHandler : IRequestHandler<CreateFolderCommand, PhysicalFolderDto>
{
    private IApplicationDbContext _context;
    private IMapper _mapper;

    public CreateFolderCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PhysicalFolderDto> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
    {
        var creator = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.CreatorId, cancellationToken);

        if (creator is null)
        {
            throw new NotFoundException(nameof(creator), request.CreatorId);
        }
        
        var locker = await _context.Lockers
                                        .Include(l => l.Room)
                                        .FirstOrDefaultAsync(r => r.Id == request.LockerId, cancellationToken);

        if (locker is null)
        {
            throw new NotFoundException(nameof(locker), request.LockerId!);
        }

        var duplicateFolder = await _context.PhysicalFolders
            .FirstOrDefaultAsync(f => f.Name.Equals(request.Name), cancellationToken);

        if (duplicateFolder is not null)
        {
            throw new DuplicateNameException($"Folder with name {request.Name} already exists!");
        }

        var entity = request.ToEntity();
        entity.Locker = locker;
        
        var folder = await _context.PhysicalFolders.AddAsync(entity, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<PhysicalFolderDto>(folder.Entity);
    }
} 