using Microsoft.AspNetCore.Mvc;
using ProFilePOC2.Application.Common.Models.Dtos;
using ProFilePOC2.Application.Lockers.Commands.CreateLocker;
using ProFilePOC2.Application.Lockers.Queries.GetLockers;
using WebUI.Controllers.Payload;

namespace WebUI.Controllers.Room.Locker;

[Route("api/rooms/{roomId:guid}/[controller]")]
public class LockersController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromBody] CreateLockerCommand command, Guid roomId)
    {
        command.RoomId = roomId;
        var result = await Mediator.Send(command);
        return Ok(ApiResponse<LockerDto>.Succeed(result));
    }

    [HttpGet]
    public async Task<IActionResult> GetRooms(Guid roomId)
    {
        var query = new GetLockersQuery { RoomId = roomId };
        var result = await Mediator.Send(query);
        return Ok(ApiResponse<IEnumerable<LockerDto>>.Succeed(result));
    }
}