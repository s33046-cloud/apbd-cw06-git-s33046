using Microsoft.AspNetCore.Mvc;
using TrainingCenterApi.Data;
using TrainingCenterApi.Models;

namespace TrainingCenterApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Room>> GetAll()
    {
        return Ok(DB.Rooms);
    }

    [HttpGet("{id}")]
    public ActionResult<Room> GetById(int id)
    {
        var room = DB.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null) return NotFound();

        return Ok(room);
    }

    [HttpGet("building/{buildingCode}")]
    public ActionResult<IEnumerable<Room>> GetByBuilding(string buildingCode)
    {
        var rooms = DB.Rooms
            .Where(r => r.BuildingCode == buildingCode)
            .ToList();

        return Ok(rooms);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Room>> Filter(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)
    {
        var query = DB.Rooms.AsQueryable();

        if (minCapacity.HasValue)
            query = query.Where(r => r.Capacity >= minCapacity);

        if (hasProjector.HasValue)
            query = query.Where(r => r.HasProjector == hasProjector);

        if (activeOnly == true)
            query = query.Where(r => r.IsActive);

        return Ok(query.ToList());
    }

    [HttpPost]
    public ActionResult<Room> Create(Room room)
    {
        room.Id = DB.Rooms.Max(r => r.Id) + 1;
        DB.Rooms.Add(room);

        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Room updated)
    {
        var room = DB.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null) return NotFound();

        room.Name = updated.Name;
        room.BuildingCode = updated.BuildingCode;
        room.Floor = updated.Floor;
        room.Capacity = updated.Capacity;
        room.HasProjector = updated.HasProjector;
        room.IsActive = updated.IsActive;

        return Ok(room);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var room = DB.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null) return NotFound();

        DB.Rooms.Remove(room);
        return NoContent();
    }
}