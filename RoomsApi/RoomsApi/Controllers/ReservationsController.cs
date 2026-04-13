using Microsoft.AspNetCore.Mvc;
using TrainingCenterApi.Data;
using TrainingCenterApi.Models;

namespace TrainingCenterApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(DB.Reservations);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var res = DB.Reservations.FirstOrDefault(r => r.Id == id);
        if (res == null) return NotFound();

        return Ok(res);
    }

    [HttpPost]
    public IActionResult Create(Reservation reservation)
    {
        var room = DB.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
        if (room == null) return BadRequest("Room does not exist");

        if (!room.IsActive) return BadRequest("Room inactive");

        bool conflict = DB.Reservations.Any(r =>
            r.RoomId == reservation.RoomId &&
            r.Date.Date == reservation.Date.Date &&
            r.StartTime < reservation.EndTime &&
            reservation.StartTime < r.EndTime
        );

        if (conflict) return Conflict("Time conflict");

        reservation.Id = DB.Reservations.Max(r => r.Id) + 1;
        DB.Reservations.Add(reservation);

        return CreatedAtAction(nameof(Get), new { id = reservation.Id }, reservation);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var res = DB.Reservations.FirstOrDefault(r => r.Id == id);
        if (res == null) return NotFound();

        DB.Reservations.Remove(res);
        return NoContent();
    }
}