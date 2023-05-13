using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Rooms.Queries.GetRooms;
using ProFilePOC2.Application.Common.Models.Dtos;
using ProFilePOC2.Application.Rooms.Commands.CreateRoom;
using WebUI.Controllers.Payload;

namespace WebUI.Controllers.Room;

public class RoomsController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(ApiResponse<RoomDto>.Succeed(result));
    }

    [HttpGet]
    public async Task<IActionResult> GetRooms()
    {
        var result = await Mediator.Send(new GetRoomsQuery());
        return Ok(ApiResponse<IEnumerable<RoomDto>>.Succeed(result));
    }
}