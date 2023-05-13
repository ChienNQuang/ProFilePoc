using MediatR;
using ProFilePOC2.Application.Common.Models.Dtos;

namespace ProFilePOC2.Application.Documents.Commands.CreateDocument;

public class CreateDocumentCommand : IRequest<DocumentDto>
{
    public Guid RoomId { get; set; }
    public Guid LockerId { get; set; }
    public Guid FolderId { get; set; }
    public string Name { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid OwnerId { get; set; }
    public string State { get; set; }
    
}