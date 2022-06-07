using System;
using System.Collections.Generic;

namespace checkin4you.Shared.Models
{
    public partial class TblReservation
    {
        public Guid Idreservation { get; set; }
        public int? ResNo { get; set; }
        public Guid? Idreservator { get; set; }
        public Guid? Idguest { get; set; }
        public Guid? IdresGroup { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? DepartureDate { get; set; }
        public string? ResType { get; set; }
        public string? ResText { get; set; }
        public bool? IsRoomChange { get; set; }
        public string? Lefrom { get; set; }
        public DateTime? Le { get; set; }
        public byte[]? Tstamp { get; set; }
        public string? Comment { get; set; }
        public Guid? IdbookingMotive { get; set; }
        public DateTime? OptionUntil { get; set; }
        public bool? NoAutoCo { get; set; }
        public int? ContingentExpiryDays { get; set; }
        public bool? UseForRegistrationForm { get; set; }
        public int? SpecialBackColor { get; set; }
        public string? Misc1 { get; set; }
        public string? Fefrom { get; set; }
        public DateTime? Fe { get; set; }
        public Guid? IdhealthInsurance { get; set; }
        public Guid? IdfamilyDoctor { get; set; }
        public string? Cifrom { get; set; }
        public DateTime? Cidate { get; set; }
        public string? Cofrom { get; set; }
        public DateTime? Codate { get; set; }
        public decimal? ReqDepositAmount { get; set; }
        public DateTime? ReqDepositDate { get; set; }
        public bool? IsEdilinked { get; set; }
        public Guid? IdinsurancePayer { get; set; }
        public Guid? Idimage { get; set; }
        public Guid? IdmarketCode { get; set; }
        public Guid? IdsourceCode { get; set; }
        public Guid? Idpaymaster { get; set; }
        public bool? IsPaymaster { get; set; }
        public Guid? IdstatFlagCountry { get; set; }
        public bool? IsFromContingent { get; set; }
        public Guid? IdcategoryReservation { get; set; }
        public Guid? Idagent { get; set; }
        public bool? IsConfirmed { get; set; }
        public int? RegistrationState { get; set; }
        public int? RequestNo { get; set; }
        public Guid? IdresPriority { get; set; }
        public string? ChargeLevel { get; set; }
        public int? ResNoGrouped { get; set; }
        public Guid? Idpartner { get; set; }
        public decimal? PartnerPrice { get; set; }
        public Guid? IdpartnerhotelInvoice { get; set; }
        public DateTime? StornoDate { get; set; }
        public DateTime? StornoTime { get; set; }
        public string? StornoReason { get; set; }
        public Guid? IdresContact { get; set; }
        public string? StornoFrom { get; set; }
        public bool? IsIfenabled { get; set; }
        public string? ReqDepositPaymentRef { get; set; }
        public int? ReqDepositDunningLevel { get; set; }
        public string? Restrictions { get; set; }
        public decimal? CcpreAutorisationAmount { get; set; }
        public string? CcpreAutorisationTrace { get; set; }
        public int? CcpreAutorisationReceiptNo { get; set; }
        public Guid? CcpreAutorisationPaymentType { get; set; }
    }
}
