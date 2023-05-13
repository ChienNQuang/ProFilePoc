using Microsoft.AspNetCore.Mvc;
using ProFilePOC2.Application.Common.Models.Dtos;
using ProFilePOC2.Application.Lockers.Commands.CreateLocker;
using ProFilePOC2.Application.Lockers.Queries.GetLockers;
using ProFilePOC2.Application.PhysicalFolders.Commands.CreateFolder;
using ProFilePOC2.Application.PhysicalFolders.Queries.GetFolders;
using WebUI.Controllers.Payload;

namespace WebUI.Controllers.Room.Locker.Folder;

[Route("api/rooms/{roomId:guid}/lockers/{lockerId:guid}/[controller]")]
public class FoldersController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateFolder([FromBody] CreateFolderCommand command, Guid roomId, Guid lockerId)
    {
        command.RoomId = roomId;
        command.LockerId = lockerId;
        var result = await Mediator.Send(command);
        return Ok(ApiResponse<PhysicalFolderDto>.Succeed(result));
    }

    [HttpGet]
    public async Task<IActionResult> GetRooms(Guid roomId, Guid lockerId)
    {
        var query = new GetFoldersQuery { RoomId = roomId, LockerId = lockerId};
        var result = await Mediator.Send(query);
        return Ok(ApiResponse<IEnumerable<PhysicalFolderDto>>.Succeed(result));
    }
}