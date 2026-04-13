using TrainingCenterApi.Models;

namespace TrainingCenterApi.Data;

public static class DB
{
    public static List<Room> Rooms = new List<Room>
    {
        new Room { Id = 1, Name = "Sala A1", BuildingCode = "A", Floor = 1, Capacity = 20, HasProjector = true, IsActive = true },
        new Room { Id = 2, Name = "Sala B2", BuildingCode = "B", Floor = 2, Capacity = 30, HasProjector = false, IsActive = true },
        new Room { Id = 3, Name = "Sala C3", BuildingCode = "C", Floor = 3, Capacity = 15, HasProjector = true, IsActive = false },
        new Room { Id = 4, Name = "Sala A4", BuildingCode = "A", Floor = 4, Capacity = 40, HasProjector = true, IsActive = true }
    };

    public static List<Reservation> Reservations = new List<Reservation>
    {
        new Reservation
        {
            Id = 1,
            RoomId = 1,
            OrganizerName = "Jan Kowalski",
            Topic = "C# Basics",
            Date = new DateTime(2026,5,10),
            StartTime = new TimeSpan(10,0,0),
            EndTime = new TimeSpan(12,0,0),
            Status = "confirmed"
        }
    };
}