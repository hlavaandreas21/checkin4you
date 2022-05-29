namespace checkin4you.Shared.Models
{
    public partial class TblGuest
    {
        public Guid Idguest { get; set; }
        public string? Guestname { get; set; }
        public int? GuestNumber { get; set; }
        public Guid? IdguestGroup { get; set; }
        public Guid? IdguestLanguageCode { get; set; }
        public Guid? Idsalutation { get; set; }
        public Guid? IdpersonTitle { get; set; }
        public string? Street1 { get; set; }
        public string? Street2 { get; set; }
        public string? PostOfficeBox { get; set; }
        public Guid? Idstate { get; set; }
        public string? ZipCode { get; set; }
        public string? CityName { get; set; }
        public string? Note { get; set; }
        public byte? BirthDay { get; set; }
        public byte? BirthMonth { get; set; }
        public short? BirthYear { get; set; }
        public bool? DeleteFlag { get; set; }
        public Guid? IdmainGuest { get; set; }
        public Guid? Idcompany { get; set; }
        public byte[]? Tstamp { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Name1 { get; set; }
        public string? Name2 { get; set; }
        public string? Name3 { get; set; }
        public Guid? Idfolder { get; set; }
        public string? Lefrom { get; set; }
        public DateTime? Le { get; set; }
        public Guid? Idnationality { get; set; }
        public string? Province { get; set; }
        public DateTime? Fe { get; set; }
        public string? Fefrom { get; set; }
        public string? DeleteMark { get; set; }
        public Guid? IdfamilySalutation { get; set; }
        public string? GuestType { get; set; }
        public Guid? IdplacedBy { get; set; }
        public Guid? Idimage { get; set; }
        public string? ChargeLevel { get; set; }
        public Guid? IdpersonTitle2 { get; set; }
        public Guid? IdpersonTitle3 { get; set; }
        public Guid? IdpersonTitle4 { get; set; }
        public bool? IsEditable { get; set; }
        public Guid? Idheadquarter { get; set; }
        public bool? NoMailing { get; set; }
        public bool? NoNewsletter { get; set; }
    }
}
