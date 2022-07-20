namespace checkin4you.Shared.DTOs
{
    public class ReservationDTO
    {
        public List<string>? Idreservations { get; set; }
        public List<string>? ExternalResIds { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? DepartureDate { get; set; }
        public List<string>? IdRooms { get; set; }
        public List<string>? ItemCodes { get; set; }
        public List<GuestDTO>? Guests { get; set; }
        public int GuestCount { get; set; }
        public bool IsComplete { get; set; }
    }
}
