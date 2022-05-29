namespace checkin4you.Shared.DTOs
{
    public class ReservationDTO
    {
        public string? Idreservation { get; set; }
        public string? ExternalResId { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string? Idroom { get; set; }
        public string? Idguest { get; set; }
        public string? Name1 { get; set; }
        public string? Name2 { get; set; }
        public string? Name3 { get; set; }
        public string? Street { get; set; }
        public string? ZipCode { get; set; }
        public string? CityName { get; set; }
        public string? Idstate { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? Email { get; set; }
    }
}
