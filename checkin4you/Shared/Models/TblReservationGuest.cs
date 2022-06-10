namespace checkin4you.Shared.Models
{
    public partial class TblReservationGuest
    {
        public Guid IdreservationGuest { get; set; }
        public Guid? Idreservation { get; set; }
        public Guid? Idguest { get; set; }
        public string? ZipCode { get; set; }
        public Guid? Idstate { get; set; }
        public DateTime? Birthdate { get; set; }
        public int? Sequence { get; set; }
        public string? Lefrom { get; set; }
        public DateTime? Le { get; set; }
        public byte[]? Tstamp { get; set; }
        public string? Street { get; set; }
        public byte? BirthDay { get; set; }
        public byte? BirthMonth { get; set; }
        public short? BirthYear { get; set; }
        public string? CityName { get; set; }
        public string? Name1 { get; set; }
        public string? Name2 { get; set; }
        public string? Name3 { get; set; }
        public bool? UseForReservation { get; set; }
        public bool? UseForRegForm { get; set; }
        public bool? IsTourGuide { get; set; }
        public Guid? IdregistrationForm { get; set; }
        public bool? CanEdit { get; set; }
        public Guid? IdreservationCopy { get; set; }
        public string? GuestCardId { get; set; }
        public bool? GuestCardApproval { get; set; }
        public string? Email { get; set; }
        public short? DisabilityDegree { get; set; }
        public string? Salutation { get; set; }
        public string? VehicleNo { get; set; }
        public byte[]? Signature { get; set; }
        public string? ExternalId { get; set; }
    }
}
