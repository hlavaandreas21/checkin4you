namespace checkin4you.Shared.Models
{
    public partial class TblItem
    {
        public Guid Iditem { get; set; }
        public Guid? IditemGroup { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? ItemType { get; set; }
        public bool? IsNetPrice { get; set; }
        public decimal? BasePrice { get; set; }
        public bool? RebateAble { get; set; }
        public bool? HabitBased { get; set; }
        public string? Lefrom { get; set; }
        public DateTime? Le { get; set; }
        public byte[]? Tstamp { get; set; }
        public bool? IsDefault { get; set; }
        public string? InterfaceKey { get; set; }
        public bool? IsManualBookable { get; set; }
        public bool? IsManualDeletable { get; set; }
        public bool? IsInactive { get; set; }
        public string? Eancode { get; set; }
        public int? PriceBase { get; set; }
        public Guid? Idrate { get; set; }
        public short? AccountInterval { get; set; }
        public bool? ForceResPriceUpdate { get; set; }
        public string? DsfinVkgvtyp { get; set; }
    }
}
