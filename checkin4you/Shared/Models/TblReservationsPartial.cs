namespace checkin4you.Shared.Models
{
    public partial class TblReservationsPartial
    {
        public Guid IdreservationPartial { get; set; }
        public Guid? Idreservation { get; set; }
        public DateTime? MoveInDate { get; set; }
        public DateTime? MoveOutDate { get; set; }
        public Guid? Idcategory { get; set; }
        public Guid? Idroom { get; set; }
        public string? ResState { get; set; }
        public int? Bed { get; set; }
        public string? Lefrom { get; set; }
        public DateTime? Le { get; set; }
        public byte[]? Tstamp { get; set; }
        public Guid? IdresProperty { get; set; }
        public Guid? IdwaitListCompany { get; set; }
        public string? Code { get; set; }
        public bool? IsActiveResSplit { get; set; }
        public Guid? IdlinkedRoom { get; set; }
    }
}
