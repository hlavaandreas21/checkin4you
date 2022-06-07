using System;
using System.Collections.Generic;

namespace checkin4you.Shared.Models
{
    public partial class TblReservationItem
    {
        public Guid IdreservationItem { get; set; }
        public Guid? Idreservation { get; set; }
        public Guid? Iditem { get; set; }
        public DateTime? BookingDate { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public bool? IsSeasonPrice { get; set; }
        public decimal? RebatePercent { get; set; }
        public decimal? RebateAmount { get; set; }
        public string? Lefrom { get; set; }
        public DateTime? Le { get; set; }
        public byte[]? Tstamp { get; set; }
        public int? PersNo { get; set; }
        public Guid? IdpriceType { get; set; }
        public DateTime? BookingDateEnd { get; set; }
        public bool? UseForPersSum { get; set; }
        public string? ResItemType { get; set; }
        public Guid? IdresItemStorno { get; set; }
        public Guid? IdinvoiceCarrier { get; set; }
        public Guid? IdfixBooking { get; set; }
        public bool? IsDeleted { get; set; }
        public Guid? IdreservationPartial { get; set; }
        public string? BookingText { get; set; }
        public Guid? Idresource { get; set; }
        public DateTime? TimeFrom { get; set; }
        public DateTime? TimeTo { get; set; }
        public Guid? IdseatingType { get; set; }
        public string? Comment { get; set; }
        public bool? UseForAccounting { get; set; }
        public Guid? IdsubResource { get; set; }
        public Guid? IdmainReservationItem { get; set; }
        public Guid? IditemDetail { get; set; }
        public string? Code { get; set; }
        public Guid? IdreservationGuest { get; set; }
        public string? BookingState { get; set; }
        public decimal? PriceNet { get; set; }
        public decimal? RebateAmountNet { get; set; }
        public bool? IsConsumed { get; set; }
        public Guid? IdresourceImage { get; set; }
        public int? PersNo1 { get; set; }
        public int? PersNo2 { get; set; }
        public int? PersNo3 { get; set; }
        public int? PersNo4 { get; set; }
        public int? PersNo5 { get; set; }
        public int? PersNo6 { get; set; }
        public int? PersNo7 { get; set; }
        public int? PersNo8 { get; set; }
        public int? PersNo9 { get; set; }
        public bool? IsFixed { get; set; }
        public string? RebateText { get; set; }
        public int? Sequence { get; set; }
        public string? VariantCode { get; set; }
        public string? VariantSubCode { get; set; }
        public Guid? IdcateringGroup { get; set; }
        public bool? BeginCateringSection { get; set; }
        public string? ReservationItemDetails { get; set; }
        public short? AccountInterval { get; set; }
        public Guid? Idrate { get; set; }
        public string? RateSplitting { get; set; }
    }
}
