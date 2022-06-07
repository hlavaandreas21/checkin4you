﻿using System;
using System.Collections.Generic;

namespace checkin4you.Shared.Models
{
    public partial class TblReservationsExt
    {
        public Guid IdreservationExt { get; set; }
        public Guid? Idreservation { get; set; }
        public int? PersNo { get; set; }
        public string? PersNoPlanned { get; set; }
        public Guid? IdseatingType { get; set; }
        public Guid? Idofficer { get; set; }
        public string? OfficerName { get; set; }
        public Guid? Idadministrator { get; set; }
        public string? AdministratorName { get; set; }
        public string? AdministratorPhone { get; set; }
        public DateTime? TimeFrom { get; set; }
        public DateTime? TimeTo { get; set; }
        public byte[]? Tstamp { get; set; }
        public string? ExtraText1 { get; set; }
        public string? ExtraText2 { get; set; }
        public string? ExtraText3 { get; set; }
        public bool? ExtraBool1 { get; set; }
        public int? ExtraInt1 { get; set; }
        public string? TableCode { get; set; }
        public decimal? ExtraDecimal1 { get; set; }
        public Guid? IdedikurStatFlag { get; set; }
        public Guid? Idthportfolio { get; set; }
        public Guid? IdextEdikurAtescort { get; set; }
        public string? Thnote { get; set; }
        public int? ThshowRestate { get; set; }
        public string? ThadvisorComment { get; set; }
        public bool? ExtraBool2 { get; set; }
        public bool? ExtraBool3 { get; set; }
        public bool? ExtraBool4 { get; set; }
        public bool? ExtraBool5 { get; set; }
        public bool? EdiFinding { get; set; }
        public byte? KunLeiSysDep { get; set; }
        public bool? IgnoreThportfolioCheck { get; set; }
        public bool? IfhiatIsChiefPhysicanPermission { get; set; }
        public DateTime? IfhiatGrantDate { get; set; }
        public string? IfhiatCategory { get; set; }
        public Guid? IfhiatCoInsuredGuestId { get; set; }
        public string? IfhiatHicpersonalId { get; set; }
        public string? IfhiatHicinstitutionId { get; set; }
        public string? IfhiatHiccardId { get; set; }
        public DateTime? IfhiatHicexpiryDate { get; set; }
        public string? IfhiatAddFlag { get; set; }
        public string? IfhiatGrantNo { get; set; }
        public DateTime? IfhiatReferralDate { get; set; }
        public string? KeyCardIds { get; set; }
        public byte? IfexportState1 { get; set; }
        public byte? IfexportState2 { get; set; }
        public byte? IfexportState3 { get; set; }
        public string? ExternId1 { get; set; }
        public string? ExternId2 { get; set; }
        public string? ExternId3 { get; set; }
        public DateTime? DesiredArrivalDate { get; set; }
        public DateTime? DesiredDepartureDate { get; set; }
        public Guid? IddesiredCategory { get; set; }
        public string? ExternalResId { get; set; }
        public string? Aps { get; set; }
        public byte? KunleiSysArr { get; set; }
        public bool? IfhiatHicchecked { get; set; }
        public bool? Wlanlocked { get; set; }
        public bool? BartechLocked { get; set; }
        public string? RoomCleaningDates { get; set; }
        public bool? CashRegisterLocked { get; set; }
        public bool? EdiInterimFinding { get; set; }
        public string? AccessCode { get; set; }
        public string? AccessCodeShort { get; set; }
        public string? ExtraText4 { get; set; }
        public string? ExtraText5 { get; set; }
        public string? ExtraText6 { get; set; }
        public bool? ExtraBool6 { get; set; }
        public bool? ExtraBool7 { get; set; }
        public bool? ExtraBool8 { get; set; }
        public bool? ExtraBool9 { get; set; }
        public bool? ExtraBool10 { get; set; }
        public bool? ExtraBool11 { get; set; }
        public bool? ExtraBool12 { get; set; }
        public bool? ExtraBool13 { get; set; }
        public bool? ExtraBool14 { get; set; }
        public bool? ExtraBool15 { get; set; }
        public bool? ExtraBool16 { get; set; }
        public bool? ExtraBool17 { get; set; }
        public bool? ExtraBool18 { get; set; }
        public int? ExtraInt2 { get; set; }
        public int? ExtraInt3 { get; set; }
        public int? ExtraInt4 { get; set; }
        public decimal? ExtraDecimal2 { get; set; }
        public decimal? ExtraDecimal3 { get; set; }
        public decimal? ExtraDecimal4 { get; set; }
        public string? IfguestMessage { get; set; }
        public string? IfguestMessageId { get; set; }
        public string? MiscDates1 { get; set; }
        public string? MiscDates2 { get; set; }
        public string? MiscDates3 { get; set; }
        public string? QrcodeLink { get; set; }
        public Guid? IdimageQrcode { get; set; }
        public bool? SelfCiNoPaymentReq { get; set; }
    }
}
