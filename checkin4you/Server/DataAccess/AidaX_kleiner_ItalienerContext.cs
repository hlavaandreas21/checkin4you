using checkin4you.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace checkin4you.Server.DataAccess
{
    public partial class AidaX_kleiner_ItalienerContext : DbContext
    {
        public AidaX_kleiner_ItalienerContext()
        {
        }

        public AidaX_kleiner_ItalienerContext(DbContextOptions<AidaX_kleiner_ItalienerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblGuest> TblGuests { get; set; } = null!;
        public virtual DbSet<TblGuests2> TblGuests2s { get; set; } = null!;
        public virtual DbSet<TblItem> TblItems { get; set; } = null!;
        public virtual DbSet<TblReservation> TblReservations { get; set; } = null!;
        public virtual DbSet<TblReservationGuest> TblReservationGuests { get; set; } = null!;
        public virtual DbSet<TblReservationItem> TblReservationItems { get; set; } = null!;
        public virtual DbSet<TblReservationsExt> TblReservationsExts { get; set; } = null!;
        public virtual DbSet<TblReservationsPartial> TblReservationsPartials { get; set; } = null!;
        public virtual DbSet<TblState> TblStates { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=AidaX_kleiner_Italiener;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblGuest>(entity =>
            {
                entity.HasKey(e => e.Idguest)
                    .HasName("PK__tblGuests__398D8EEE");

                entity.ToTable("tblGuests");

                entity.HasIndex(e => e.DeleteFlag, "IX_tblGuests_DeleteFlag");

                entity.HasIndex(e => e.GuestNumber, "IX_tblGuests_GuestNumber");

                entity.HasIndex(e => e.GuestType, "IX_tblGuests_GuestType")
                    .HasFillFactor(50);

                entity.HasIndex(e => e.Guestname, "IX_tblGuests_Guestname ASC");

                entity.HasIndex(e => e.Idcompany, "IX_tblGuests_IDCompany");

                entity.HasIndex(e => e.Idfolder, "IX_tblGuests_IDFolder");

                entity.HasIndex(e => e.IdmainGuest, "IX_tblGuests_IDMainGuest");

                entity.HasIndex(e => e.IdplacedBy, "IX_tblGuests_IDPlacedBy");

                entity.HasIndex(e => e.Idstate, "IX_tblGuests_IDState");

                entity.HasIndex(e => e.ZipCode, "IX_tblGuests_ZipCode ASC");

                entity.HasIndex(e => e.Tstamp, "UIX_tblGuests_TStamp")
                    .IsUnique();

                entity.Property(e => e.Idguest)
                    .HasColumnName("IDGuest")
                    .HasDefaultValueSql("(null)");

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.ChargeLevel).HasMaxLength(10);

                entity.Property(e => e.CityName).HasMaxLength(50);

                entity.Property(e => e.DeleteMark).HasMaxLength(1);

                entity.Property(e => e.Fe)
                    .HasColumnType("datetime")
                    .HasColumnName("FE");

                entity.Property(e => e.Fefrom)
                    .HasMaxLength(30)
                    .HasColumnName("FEFrom");

                entity.Property(e => e.GuestType)
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.Guestname).HasMaxLength(200);

                entity.Property(e => e.Idcompany).HasColumnName("IDCompany");

                entity.Property(e => e.IdfamilySalutation).HasColumnName("IDFamilySalutation");

                entity.Property(e => e.Idfolder).HasColumnName("IDFolder");

                entity.Property(e => e.IdguestGroup).HasColumnName("IDGuestGroup");

                entity.Property(e => e.IdguestLanguageCode).HasColumnName("IDGuestLanguageCode");

                entity.Property(e => e.Idheadquarter).HasColumnName("IDHeadquarter");

                entity.Property(e => e.Idimage).HasColumnName("IDImage");

                entity.Property(e => e.IdmainGuest).HasColumnName("IDMainGuest");

                entity.Property(e => e.Idnationality).HasColumnName("IDNationality");

                entity.Property(e => e.IdpersonTitle).HasColumnName("IDPersonTitle");

                entity.Property(e => e.IdpersonTitle2).HasColumnName("IDPersonTitle2");

                entity.Property(e => e.IdpersonTitle3).HasColumnName("IDPersonTitle3");

                entity.Property(e => e.IdpersonTitle4).HasColumnName("IDPersonTitle4");

                entity.Property(e => e.IdplacedBy).HasColumnName("IDPlacedBy");

                entity.Property(e => e.Idsalutation).HasColumnName("IDSalutation");

                entity.Property(e => e.Idstate).HasColumnName("IDState");

                entity.Property(e => e.Le)
                    .HasColumnType("datetime")
                    .HasColumnName("LE");

                entity.Property(e => e.Lefrom)
                    .HasMaxLength(30)
                    .HasColumnName("LEFrom");

                entity.Property(e => e.Name1).HasMaxLength(80);

                entity.Property(e => e.Name2).HasMaxLength(80);

                entity.Property(e => e.Name3).HasMaxLength(80);

                entity.Property(e => e.Note).HasMaxLength(250);

                entity.Property(e => e.PostOfficeBox).HasMaxLength(50);

                entity.Property(e => e.Province).HasMaxLength(20);

                entity.Property(e => e.Street1).HasMaxLength(80);

                entity.Property(e => e.Street2).HasMaxLength(80);

                entity.Property(e => e.Tstamp)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("TStamp");

                entity.Property(e => e.ZipCode).HasMaxLength(10);
            });

            modelBuilder.Entity<TblGuests2>(entity =>
            {
                entity.HasKey(e => e.Idguest)
                    .HasName("PK__tblGuests2__5CD6CB2B");

                entity.ToTable("tblGuests2");

                entity.HasIndex(e => e.Email, "IX_tblGuests2_Email");

                entity.HasIndex(e => e.Idextern, "IX_tblGuests2_IDExtern");

                entity.HasIndex(e => e.InsurancePolNo, "IX_tblGuests2_InsurancePolNo");

                entity.HasIndex(e => e.Tstamp, "UIX_tblGuests2_TStamp")
                    .IsUnique();

                entity.Property(e => e.Idguest)
                    .HasColumnName("IDGuest")
                    .HasDefaultValueSql("(null)");

                entity.Property(e => e.Bic)
                    .HasMaxLength(11)
                    .HasColumnName("BIC");

                entity.Property(e => e.Comment).HasMaxLength(300);

                entity.Property(e => e.CompanyPhone).HasMaxLength(30);

                entity.Property(e => e.CompanyPhone2).HasMaxLength(30);

                entity.Property(e => e.CreditCardOwner).HasMaxLength(50);

                entity.Property(e => e.CreditCardValidTo).HasMaxLength(5);

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .HasColumnName("EMail");

                entity.Property(e => e.Email2)
                    .HasMaxLength(250)
                    .HasColumnName("EMail2");

                entity.Property(e => e.Email3)
                    .HasMaxLength(250)
                    .HasColumnName("EMail3");

                entity.Property(e => e.FamilyLetterSalutation).HasMaxLength(300);

                entity.Property(e => e.Fax).HasMaxLength(30);

                entity.Property(e => e.GuestExtraDate1).HasColumnType("datetime");

                entity.Property(e => e.GuestExtraDate2).HasColumnType("datetime");

                entity.Property(e => e.GuestExtraString1).HasMaxLength(100);

                entity.Property(e => e.Homepage).HasMaxLength(250);

                entity.Property(e => e.Iban)
                    .HasMaxLength(34)
                    .HasColumnName("IBAN");

                entity.Property(e => e.Idagent).HasColumnName("IDAgent");

                entity.Property(e => e.IddefaultArrangement).HasColumnName("IDDefaultArrangement");

                entity.Property(e => e.IddefaultBookingMotive).HasColumnName("IDDefaultBookingMotive");

                entity.Property(e => e.IddefaultMarketCode).HasColumnName("IDDefaultMarketCode");

                entity.Property(e => e.IddefaultPersonType).HasColumnName("IDDefaultPersonType");

                entity.Property(e => e.IddefaultSourceCode).HasColumnName("IDDefaultSourceCode");

                entity.Property(e => e.Idextern)
                    .HasMaxLength(30)
                    .HasColumnName("IDExtern");

                entity.Property(e => e.IdfamilyDoctor).HasColumnName("IDFamilyDoctor");

                entity.Property(e => e.IdfinancialAccount).HasColumnName("IDFinancialAccount");

                entity.Property(e => e.IdhealthInsurance).HasColumnName("IDHealthInsurance");

                entity.Property(e => e.IdinsuranceInvoiceLocation).HasColumnName("IDInsuranceInvoiceLocation");

                entity.Property(e => e.IdinsurancePayer).HasColumnName("IDInsurancePayer");

                entity.Property(e => e.IdinvoiceNoRange).HasColumnName("IDInvoiceNoRange");

                entity.Property(e => e.IdpaymentCondition).HasColumnName("IDPaymentCondition");

                entity.Property(e => e.IdpaymentType).HasColumnName("IDPaymentType");

                entity.Property(e => e.Idsex).HasColumnName("IDSex");

                entity.Property(e => e.IdtravelDocument).HasColumnName("IDTravelDocument");

                entity.Property(e => e.InsurancePol).HasMaxLength(20);

                entity.Property(e => e.InsurancePolNo).HasMaxLength(20);

                entity.Property(e => e.Job).HasMaxLength(80);

                entity.Property(e => e.JobPosition).HasMaxLength(50);

                entity.Property(e => e.LastImportDate).HasColumnType("datetime");

                entity.Property(e => e.LetterName1).HasMaxLength(200);

                entity.Property(e => e.LetterName2).HasMaxLength(200);

                entity.Property(e => e.LetterName3).HasMaxLength(200);

                entity.Property(e => e.LetterSalutation).HasMaxLength(300);

                entity.Property(e => e.MatchCode).HasMaxLength(20);

                entity.Property(e => e.Misc).HasMaxLength(50);

                entity.Property(e => e.Misc2).HasMaxLength(50);

                entity.Property(e => e.Mobile).HasMaxLength(30);

                entity.Property(e => e.Phone).HasMaxLength(30);

                entity.Property(e => e.Phone2).HasMaxLength(30);

                entity.Property(e => e.RebateAmount).HasColumnType("money");

                entity.Property(e => e.RebateArra).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Salutation).HasMaxLength(100);

                entity.Property(e => e.SpecialismField).HasMaxLength(2);

                entity.Property(e => e.TableCode).HasMaxLength(20);

                entity.Property(e => e.TravelDocumentBirthCity).HasMaxLength(50);

                entity.Property(e => e.TravelDocumentCity).HasMaxLength(50);

                entity.Property(e => e.TravelDocumentDate).HasColumnType("datetime");

                entity.Property(e => e.TravelDocumentNo).HasMaxLength(50);

                entity.Property(e => e.Tstamp)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("TStamp");

                entity.Property(e => e.VatidentNo)
                    .HasMaxLength(20)
                    .HasColumnName("VATIdentNo");

                entity.Property(e => e.VehicleNo).HasMaxLength(80);
            });

            modelBuilder.Entity<TblItem>(entity =>
            {
                entity.HasKey(e => e.Iditem)
                    .HasName("PK__tblItems__33D4B598");

                entity.ToTable("tblItems");

                entity.HasIndex(e => e.AccountInterval, "IX_tblItems_AccountInterval");

                entity.HasIndex(e => e.IditemGroup, "IX_tblItems_IDItemGroup");

                entity.HasIndex(e => e.ItemCode, "IX_tblItems_ItemCode");

                entity.HasIndex(e => e.ItemType, "IX_tblItems_ItemType");

                entity.Property(e => e.Iditem)
                    .HasColumnName("IDItem")
                    .HasDefaultValueSql("(null)");

                entity.Property(e => e.BasePrice).HasColumnType("money");

                entity.Property(e => e.DsfinVkgvtyp)
                    .HasMaxLength(30)
                    .HasColumnName("DSFinVKGVTyp");

                entity.Property(e => e.Eancode).HasColumnName("EANCode");

                entity.Property(e => e.IditemGroup).HasColumnName("IDItemGroup");

                entity.Property(e => e.Idrate).HasColumnName("IDRate");

                entity.Property(e => e.InterfaceKey).HasMaxLength(50);

                entity.Property(e => e.ItemCode).HasMaxLength(20);

                entity.Property(e => e.ItemName).HasMaxLength(100);

                entity.Property(e => e.ItemType)
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.Le)
                    .HasColumnType("datetime")
                    .HasColumnName("LE");

                entity.Property(e => e.Lefrom)
                    .HasMaxLength(30)
                    .HasColumnName("LEFrom");

                entity.Property(e => e.Tstamp)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("TStamp");
            });

            modelBuilder.Entity<TblReservation>(entity =>
            {
                entity.HasKey(e => e.Idreservation)
                    .HasName("PK__tblReservations__286302EC");

                entity.ToTable("tblReservations");

                entity.HasIndex(e => e.ArrivalDate, "IX_tblReservations_ArrivalDate");

                entity.HasIndex(e => e.DepartureDate, "IX_tblReservations_DepartureDate");

                entity.HasIndex(e => e.Idguest, "IX_tblReservations_IDGuest");

                entity.HasIndex(e => e.Idpartner, "IX_tblReservations_IDPartner");

                entity.HasIndex(e => e.Idpaymaster, "IX_tblReservations_IDPaymaster");

                entity.HasIndex(e => e.IdresGroup, "IX_tblReservations_IDResGroup");

                entity.HasIndex(e => new { e.IdresGroup, e.Idreservation }, "IX_tblReservations_IDResGroup_IDReservation");

                entity.HasIndex(e => e.Idreservator, "IX_tblReservations_IDReservator");

                entity.HasIndex(e => e.RegistrationState, "IX_tblReservations_RegistrationState");

                entity.HasIndex(e => e.ReqDepositDate, "IX_tblReservations_ReqDepositDate");

                entity.HasIndex(e => e.RequestNo, "IX_tblReservations_RequestNo");

                entity.HasIndex(e => e.ResNo, "IX_tblReservations_ResNo");

                entity.HasIndex(e => e.ResNoGrouped, "IX_tblReservations_ResNoGrouped");

                entity.HasIndex(e => e.StornoDate, "IX_tblReservations_StornoDate");

                entity.HasIndex(e => e.Tstamp, "UIX_tblReservations_TStamp")
                    .IsUnique();

                entity.Property(e => e.Idreservation)
                    .HasColumnName("IDReservation")
                    .HasDefaultValueSql("(null)");

                entity.Property(e => e.ArrivalDate).HasColumnType("datetime");

                entity.Property(e => e.CcpreAutorisationAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("CCPreAutorisationAmount");

                entity.Property(e => e.CcpreAutorisationPaymentType).HasColumnName("CCPreAutorisationPaymentType");

                entity.Property(e => e.CcpreAutorisationReceiptNo).HasColumnName("CCPreAutorisationReceiptNo");

                entity.Property(e => e.CcpreAutorisationTrace)
                    .HasMaxLength(50)
                    .HasColumnName("CCPreAutorisationTrace");

                entity.Property(e => e.ChargeLevel).HasMaxLength(10);

                entity.Property(e => e.Cidate)
                    .HasColumnType("datetime")
                    .HasColumnName("CIDate");

                entity.Property(e => e.Cifrom)
                    .HasMaxLength(30)
                    .HasColumnName("CIFrom");

                entity.Property(e => e.Codate)
                    .HasColumnType("datetime")
                    .HasColumnName("CODate");

                entity.Property(e => e.Cofrom)
                    .HasMaxLength(30)
                    .HasColumnName("COFrom");

                entity.Property(e => e.Comment).HasMaxLength(300);

                entity.Property(e => e.DepartureDate).HasColumnType("datetime");

                entity.Property(e => e.Fe)
                    .HasColumnType("datetime")
                    .HasColumnName("FE");

                entity.Property(e => e.Fefrom)
                    .HasMaxLength(30)
                    .HasColumnName("FEFrom");

                entity.Property(e => e.Idagent).HasColumnName("IDAgent");

                entity.Property(e => e.IdbookingMotive).HasColumnName("IDBookingMotive");

                entity.Property(e => e.IdcategoryReservation).HasColumnName("IDCategoryReservation");

                entity.Property(e => e.IdfamilyDoctor).HasColumnName("IDFamilyDoctor");

                entity.Property(e => e.Idguest).HasColumnName("IDGuest");

                entity.Property(e => e.IdhealthInsurance).HasColumnName("IDHealthInsurance");

                entity.Property(e => e.Idimage).HasColumnName("IDImage");

                entity.Property(e => e.IdinsurancePayer).HasColumnName("IDInsurancePayer");

                entity.Property(e => e.IdmarketCode).HasColumnName("IDMarketCode");

                entity.Property(e => e.Idpartner).HasColumnName("IDPartner");

                entity.Property(e => e.IdpartnerhotelInvoice).HasColumnName("IDPartnerhotelInvoice");

                entity.Property(e => e.Idpaymaster).HasColumnName("IDPaymaster");

                entity.Property(e => e.IdresContact).HasColumnName("IDResContact");

                entity.Property(e => e.IdresGroup).HasColumnName("IDResGroup");

                entity.Property(e => e.IdresPriority).HasColumnName("IDResPriority");

                entity.Property(e => e.Idreservator).HasColumnName("IDReservator");

                entity.Property(e => e.IdsourceCode).HasColumnName("IDSourceCode");

                entity.Property(e => e.IdstatFlagCountry).HasColumnName("IDStatFlagCountry");

                entity.Property(e => e.IsEdilinked).HasColumnName("IsEDILinked");

                entity.Property(e => e.IsIfenabled).HasColumnName("IsIFEnabled");

                entity.Property(e => e.Le)
                    .HasColumnType("datetime")
                    .HasColumnName("LE");

                entity.Property(e => e.Lefrom)
                    .HasMaxLength(30)
                    .HasColumnName("LEFrom");

                entity.Property(e => e.Misc1).HasMaxLength(100);

                entity.Property(e => e.NoAutoCo).HasColumnName("NoAutoCO");

                entity.Property(e => e.OptionUntil).HasColumnType("datetime");

                entity.Property(e => e.PartnerPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ReqDepositAmount).HasColumnType("money");

                entity.Property(e => e.ReqDepositDate).HasColumnType("datetime");

                entity.Property(e => e.ReqDepositPaymentRef).HasMaxLength(50);

                entity.Property(e => e.ResText).HasMaxLength(100);

                entity.Property(e => e.ResType).HasMaxLength(2);

                entity.Property(e => e.StornoDate).HasColumnType("datetime");

                entity.Property(e => e.StornoFrom).HasMaxLength(30);

                entity.Property(e => e.StornoReason).HasMaxLength(100);

                entity.Property(e => e.StornoTime).HasColumnType("datetime");

                entity.Property(e => e.Tstamp)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("TStamp");
            });

            modelBuilder.Entity<TblReservationGuest>(entity =>
            {
                entity.HasKey(e => e.IdreservationGuest)
                    .HasName("PK__tblReservationGu__1AD3FDA4");

                entity.ToTable("tblReservationGuests");

                entity.HasIndex(e => e.Idguest, "IX_tblReservationGuests_IDGuest");

                entity.HasIndex(e => e.IdregistrationForm, "IX_tblReservationGuests_IDRegistrationForm");

                entity.HasIndex(e => e.Idreservation, "IX_tblReservationGuests_IDReservation");

                entity.HasIndex(e => e.Tstamp, "UIX_tblReservationGuests_TStamp")
                    .IsUnique();

                entity.Property(e => e.IdreservationGuest)
                    .HasColumnName("IDReservationGuest")
                    .HasDefaultValueSql("(null)");

                entity.Property(e => e.Birthdate).HasColumnType("datetime");

                entity.Property(e => e.CityName).HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .HasColumnName("EMail");

                entity.Property(e => e.ExternalId).HasMaxLength(50);

                entity.Property(e => e.GuestCardId)
                    .HasMaxLength(30)
                    .HasColumnName("GuestCardID");

                entity.Property(e => e.Idguest).HasColumnName("IDGuest");

                entity.Property(e => e.IdregistrationForm).HasColumnName("IDRegistrationForm");

                entity.Property(e => e.Idreservation).HasColumnName("IDReservation");

                entity.Property(e => e.IdreservationCopy).HasColumnName("IDReservationCopy");

                entity.Property(e => e.Idstate).HasColumnName("IDState");

                entity.Property(e => e.Le)
                    .HasColumnType("datetime")
                    .HasColumnName("LE");

                entity.Property(e => e.Lefrom)
                    .HasMaxLength(30)
                    .HasColumnName("LEFrom");

                entity.Property(e => e.Name1).HasMaxLength(80);

                entity.Property(e => e.Name2).HasMaxLength(80);

                entity.Property(e => e.Name3).HasMaxLength(80);

                entity.Property(e => e.Salutation).HasMaxLength(100);

                entity.Property(e => e.Street).HasMaxLength(80);

                entity.Property(e => e.Tstamp)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("TStamp");

                entity.Property(e => e.VehicleNo).HasMaxLength(80);

                entity.Property(e => e.ZipCode).HasMaxLength(10);
            });

            modelBuilder.Entity<TblReservationItem>(entity =>
            {
                entity.HasKey(e => e.IdreservationItem)
                    .HasName("PK__tblReservationIt__1A14E395");

                entity.ToTable("tblReservationItems");

                entity.HasIndex(e => e.BookingDate, "IX_tblReservationItems_BookingDate");

                entity.HasIndex(e => e.BookingDateEnd, "IX_tblReservationItems_BookingDateEnd")
                    .HasFillFactor(25);

                entity.HasIndex(e => e.IdcateringGroup, "IX_tblReservationItems_IDCateringGroup");

                entity.HasIndex(e => e.Iditem, "IX_tblReservationItems_IDItem");

                entity.HasIndex(e => e.IdmainReservationItem, "IX_tblReservationItems_IDMainReservationItem")
                    .HasFillFactor(25);

                entity.HasIndex(e => e.IdpriceType, "IX_tblReservationItems_IDPriceType");

                entity.HasIndex(e => e.Idrate, "IX_tblReservationItems_IDRate");

                entity.HasIndex(e => e.Idreservation, "IX_tblReservationItems_IDReservation");

                entity.HasIndex(e => e.IdreservationGuest, "IX_tblReservationItems_IDReservationGuest")
                    .HasFillFactor(25);

                entity.HasIndex(e => e.IdreservationPartial, "IX_tblReservationItems_IDReservationPartial");

                entity.HasIndex(e => e.Idresource, "IX_tblReservationItems_IDResource")
                    .HasFillFactor(25);

                entity.HasIndex(e => e.IdsubResource, "IX_tblReservationItems_IDSubResource")
                    .HasFillFactor(25);

                entity.HasIndex(e => e.ResItemType, "IX_tblReservationItems_ResItemType")
                    .HasFillFactor(25);

                entity.HasIndex(e => e.TimeFrom, "IX_tblReservationItems_TimeFrom")
                    .HasFillFactor(25);

                entity.HasIndex(e => e.TimeTo, "IX_tblReservationItems_TimeTo")
                    .HasFillFactor(25);

                entity.HasIndex(e => e.Tstamp, "UIX_tblReservationItems_TStamp")
                    .IsUnique();

                entity.Property(e => e.IdreservationItem)
                    .HasColumnName("IDReservationItem")
                    .HasDefaultValueSql("(null)");

                entity.Property(e => e.BookingDate).HasColumnType("datetime");

                entity.Property(e => e.BookingDateEnd).HasColumnType("datetime");

                entity.Property(e => e.BookingState)
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.BookingText).HasMaxLength(100);

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.Comment).HasMaxLength(300);

                entity.Property(e => e.IdcateringGroup).HasColumnName("IDCateringGroup");

                entity.Property(e => e.IdfixBooking).HasColumnName("IDFixBooking");

                entity.Property(e => e.IdinvoiceCarrier).HasColumnName("IDInvoiceCarrier");

                entity.Property(e => e.Iditem).HasColumnName("IDItem");

                entity.Property(e => e.IditemDetail).HasColumnName("IDItemDetail");

                entity.Property(e => e.IdmainReservationItem).HasColumnName("IDMainReservationItem");

                entity.Property(e => e.IdpriceType).HasColumnName("IDPriceType");

                entity.Property(e => e.Idrate).HasColumnName("IDRate");

                entity.Property(e => e.IdresItemStorno).HasColumnName("IDResItemStorno");

                entity.Property(e => e.Idreservation).HasColumnName("IDReservation");

                entity.Property(e => e.IdreservationGuest).HasColumnName("IDReservationGuest");

                entity.Property(e => e.IdreservationPartial).HasColumnName("IDReservationPartial");

                entity.Property(e => e.Idresource).HasColumnName("IDResource");

                entity.Property(e => e.IdresourceImage).HasColumnName("IDResourceImage");

                entity.Property(e => e.IdseatingType).HasColumnName("IDSeatingType");

                entity.Property(e => e.IdsubResource).HasColumnName("IDSubResource");

                entity.Property(e => e.Le)
                    .HasColumnType("datetime")
                    .HasColumnName("LE");

                entity.Property(e => e.Lefrom)
                    .HasMaxLength(30)
                    .HasColumnName("LEFrom");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.PriceNet).HasColumnType("money");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RebateAmount).HasColumnType("money");

                entity.Property(e => e.RebateAmountNet).HasColumnType("money");

                entity.Property(e => e.RebatePercent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RebateText).HasMaxLength(100);

                entity.Property(e => e.ResItemType)
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.TimeFrom).HasColumnType("datetime");

                entity.Property(e => e.TimeTo).HasColumnType("datetime");

                entity.Property(e => e.Tstamp)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("TStamp");

                entity.Property(e => e.VariantCode).HasMaxLength(50);

                entity.Property(e => e.VariantSubCode).HasMaxLength(50);
            });

            modelBuilder.Entity<TblReservationsExt>(entity =>
            {
                entity.HasKey(e => e.IdreservationExt)
                    .HasName("PK__tblReservationsE__65C116E7");

                entity.ToTable("tblReservationsExt");

                entity.HasIndex(e => e.IdextEdikurAtescort, "IX_tblReservationsExt_IDExtEdikurATEscort");

                entity.HasIndex(e => e.IfhiatCoInsuredGuestId, "IX_tblReservationsExt_IFHIAT_CoInsuredGuestId");

                entity.HasIndex(e => e.ThshowRestate, "IX_tblReservationsExt_THShowREState");

                entity.HasIndex(e => e.Idreservation, "UIX_tblReservationsExt_IDReservation")
                    .IsUnique()
                    .HasFillFactor(25);

                entity.HasIndex(e => e.Tstamp, "UIX_tblReservationsExt_TStamp")
                    .IsUnique();

                entity.Property(e => e.IdreservationExt)
                    .HasColumnName("IDReservationExt")
                    .HasDefaultValueSql("(null)");

                entity.Property(e => e.AccessCode).HasMaxLength(50);

                entity.Property(e => e.AccessCodeShort).HasMaxLength(20);

                entity.Property(e => e.AdministratorName).HasMaxLength(200);

                entity.Property(e => e.AdministratorPhone).HasMaxLength(30);

                entity.Property(e => e.Aps)
                    .HasMaxLength(200)
                    .HasColumnName("APs");

                entity.Property(e => e.DesiredArrivalDate).HasColumnType("datetime");

                entity.Property(e => e.DesiredDepartureDate).HasColumnType("datetime");

                entity.Property(e => e.EdiFinding).HasColumnName("EDI_Finding");

                entity.Property(e => e.EdiInterimFinding).HasColumnName("EDI_InterimFinding");

                entity.Property(e => e.ExternId1)
                    .HasMaxLength(20)
                    .HasColumnName("ExternID1");

                entity.Property(e => e.ExternId2)
                    .HasMaxLength(20)
                    .HasColumnName("ExternID2");

                entity.Property(e => e.ExternId3)
                    .HasMaxLength(20)
                    .HasColumnName("ExternID3");

                entity.Property(e => e.ExternalResId).HasMaxLength(50);

                entity.Property(e => e.ExtraDecimal1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ExtraDecimal2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ExtraDecimal3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ExtraDecimal4).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ExtraText1).HasMaxLength(100);

                entity.Property(e => e.ExtraText2).HasMaxLength(100);

                entity.Property(e => e.ExtraText3).HasMaxLength(100);

                entity.Property(e => e.ExtraText4).HasMaxLength(100);

                entity.Property(e => e.ExtraText5).HasMaxLength(100);

                entity.Property(e => e.ExtraText6).HasMaxLength(100);

                entity.Property(e => e.Idadministrator).HasColumnName("IDAdministrator");

                entity.Property(e => e.IddesiredCategory).HasColumnName("IDDesiredCategory");

                entity.Property(e => e.IdedikurStatFlag).HasColumnName("IDEdikurStatFlag");

                entity.Property(e => e.IdextEdikurAtescort).HasColumnName("IDExtEdikurATEscort");

                entity.Property(e => e.IdimageQrcode).HasColumnName("IDImageQRCode");

                entity.Property(e => e.Idofficer).HasColumnName("IDOfficer");

                entity.Property(e => e.Idreservation).HasColumnName("IDReservation");

                entity.Property(e => e.IdseatingType).HasColumnName("IDSeatingType");

                entity.Property(e => e.Idthportfolio).HasColumnName("IDTHPortfolio");

                entity.Property(e => e.IfexportState1).HasColumnName("IFExportState1");

                entity.Property(e => e.IfexportState2).HasColumnName("IFExportState2");

                entity.Property(e => e.IfexportState3).HasColumnName("IFExportState3");

                entity.Property(e => e.IfguestMessage).HasColumnName("IFGuestMessage");

                entity.Property(e => e.IfguestMessageId)
                    .HasMaxLength(10)
                    .HasColumnName("IFGuestMessageID");

                entity.Property(e => e.IfhiatAddFlag)
                    .HasMaxLength(2)
                    .HasColumnName("IFHIAT_AddFlag");

                entity.Property(e => e.IfhiatCategory)
                    .HasMaxLength(20)
                    .HasColumnName("IFHIAT_Category");

                entity.Property(e => e.IfhiatCoInsuredGuestId).HasColumnName("IFHIAT_CoInsuredGuestId");

                entity.Property(e => e.IfhiatGrantDate)
                    .HasColumnType("datetime")
                    .HasColumnName("IFHIAT_GrantDate");

                entity.Property(e => e.IfhiatGrantNo)
                    .HasMaxLength(8)
                    .HasColumnName("IFHIAT_GrantNo");

                entity.Property(e => e.IfhiatHiccardId)
                    .HasMaxLength(30)
                    .HasColumnName("IFHIAT_HICCardId");

                entity.Property(e => e.IfhiatHicchecked).HasColumnName("IFHIAT_HICChecked");

                entity.Property(e => e.IfhiatHicexpiryDate)
                    .HasColumnType("datetime")
                    .HasColumnName("IFHIAT_HICExpiryDate");

                entity.Property(e => e.IfhiatHicinstitutionId)
                    .HasMaxLength(30)
                    .HasColumnName("IFHIAT_HICInstitutionId");

                entity.Property(e => e.IfhiatHicpersonalId)
                    .HasMaxLength(30)
                    .HasColumnName("IFHIAT_HICPersonalId");

                entity.Property(e => e.IfhiatIsChiefPhysicanPermission).HasColumnName("IFHIAT_IsChiefPhysicanPermission");

                entity.Property(e => e.IfhiatReferralDate)
                    .HasColumnType("datetime")
                    .HasColumnName("IFHIAT_ReferralDate");

                entity.Property(e => e.IgnoreThportfolioCheck).HasColumnName("IgnoreTHPortfolioCheck");

                entity.Property(e => e.KeyCardIds)
                    .HasMaxLength(200)
                    .HasColumnName("KeyCardIDs");

                entity.Property(e => e.OfficerName).HasMaxLength(200);

                entity.Property(e => e.PersNoPlanned).HasMaxLength(10);

                entity.Property(e => e.QrcodeLink)
                    .HasMaxLength(100)
                    .HasColumnName("QRCodeLink");

                entity.Property(e => e.SelfCiNoPaymentReq).HasColumnName("SelfCI_NoPaymentReq");

                entity.Property(e => e.TableCode).HasMaxLength(20);

                entity.Property(e => e.ThadvisorComment)
                    .HasMaxLength(200)
                    .HasColumnName("THAdvisorComment");

                entity.Property(e => e.Thnote).HasColumnName("THNote");

                entity.Property(e => e.ThshowRestate).HasColumnName("THShowREState");

                entity.Property(e => e.TimeFrom).HasColumnType("datetime");

                entity.Property(e => e.TimeTo).HasColumnType("datetime");

                entity.Property(e => e.Tstamp)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("TStamp");

                entity.Property(e => e.Wlanlocked).HasColumnName("WLANLocked");
            });

            modelBuilder.Entity<TblReservationsPartial>(entity =>
            {
                entity.HasKey(e => e.IdreservationPartial)
                    .HasName("PK__tblReservationsP__3D2915A8");

                entity.ToTable("tblReservationsPartial");

                entity.HasIndex(e => e.Idcategory, "IX_tblReservationsPartial_IDCategory");

                entity.HasIndex(e => e.IdlinkedRoom, "IX_tblReservationsPartial_IDLinkedRoom");

                entity.HasIndex(e => e.Idreservation, "IX_tblReservationsPartial_IDReservation");

                entity.HasIndex(e => new { e.Idreservation, e.MoveInDate, e.MoveOutDate, e.Idroom }, "IX_tblReservationsPartial_IDReservation ASC_MoveInDate ASC_MoveOutDate ASC_IDRoom ASC");

                entity.HasIndex(e => e.Idroom, "IX_tblReservationsPartial_IDRoom");

                entity.HasIndex(e => e.IsActiveResSplit, "IX_tblReservationsPartial_IsActiveResSplit");

                entity.HasIndex(e => e.MoveInDate, "IX_tblReservationsPartial_MoveInDate");

                entity.HasIndex(e => new { e.MoveInDate, e.MoveOutDate, e.Idroom }, "IX_tblReservationsPartial_MoveInDate ASC_MoveOutDate ASC_IDRoom ASC");

                entity.HasIndex(e => e.MoveOutDate, "IX_tblReservationsPartial_MoveOutDate");

                entity.HasIndex(e => e.ResState, "IX_tblReservationsPartial_ResState");

                entity.HasIndex(e => e.Tstamp, "UIX_tblReservationsPartial_TStamp")
                    .IsUnique();

                entity.Property(e => e.IdreservationPartial)
                    .HasColumnName("IDReservationPartial")
                    .HasDefaultValueSql("(null)");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.Idcategory).HasColumnName("IDCategory");

                entity.Property(e => e.IdlinkedRoom).HasColumnName("IDLinkedRoom");

                entity.Property(e => e.IdresProperty).HasColumnName("IDResProperty");

                entity.Property(e => e.Idreservation).HasColumnName("IDReservation");

                entity.Property(e => e.Idroom).HasColumnName("IDRoom");

                entity.Property(e => e.IdwaitListCompany).HasColumnName("IDWaitListCompany");

                entity.Property(e => e.Le)
                    .HasColumnType("datetime")
                    .HasColumnName("LE");

                entity.Property(e => e.Lefrom)
                    .HasMaxLength(30)
                    .HasColumnName("LEFrom");

                entity.Property(e => e.MoveInDate).HasColumnType("datetime");

                entity.Property(e => e.MoveOutDate).HasColumnType("datetime");

                entity.Property(e => e.ResState).HasMaxLength(2);

                entity.Property(e => e.Tstamp)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("TStamp");
            });

            modelBuilder.Entity<TblState>(entity =>
            {
                entity.HasKey(e => e.Idstate)
                    .HasName("PK__tblStates__173876EA");

                entity.ToTable("tblStates");

                entity.Property(e => e.Idstate)
                    .HasColumnName("IDState")
                    .HasDefaultValueSql("(null)");

                entity.Property(e => e.AreaCode).HasMaxLength(10);

                entity.Property(e => e.Capital).HasMaxLength(50);

                entity.Property(e => e.Continent).HasMaxLength(50);

                entity.Property(e => e.Iso3166)
                    .HasMaxLength(2)
                    .HasColumnName("ISO3166");

                entity.Property(e => e.Iso3166a3)
                    .HasMaxLength(3)
                    .HasColumnName("ISO3166A3");

                entity.Property(e => e.Le)
                    .HasColumnType("datetime")
                    .HasColumnName("LE");

                entity.Property(e => e.Lefrom)
                    .HasMaxLength(30)
                    .HasColumnName("LEFrom");

                entity.Property(e => e.LicencePlateNumber).HasMaxLength(3);

                entity.Property(e => e.StateCode).HasMaxLength(10);

                entity.Property(e => e.StateName).HasMaxLength(50);

                entity.Property(e => e.Tstamp)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("TStamp");

                entity.Property(e => e.WinTopKey).HasMaxLength(5);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
