namespace checkin4you.Shared.Models
{
    public partial class TblState
    {
        public Guid Idstate { get; set; }
        public string? StateCode { get; set; }
        public string? StateName { get; set; }
        public string? AreaCode { get; set; }
        public string? Lefrom { get; set; }
        public DateTime? Le { get; set; }
        public byte[]? Tstamp { get; set; }
        public string? Iso3166 { get; set; }
        public string? Capital { get; set; }
        public string? Continent { get; set; }
        public bool? IsDefault { get; set; }
        public string? LicencePlateNumber { get; set; }
        public int? Backcolor { get; set; }
        public bool? ZipCodeObligated { get; set; }
        public string? WinTopKey { get; set; }
        public string? Iso3166a3 { get; set; }
    }
}
