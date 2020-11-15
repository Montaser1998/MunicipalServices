using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MunicipalServices.Data
{
    //administration Department
    //finance department
    //Engineering Department
    //Department of water
    /// <summary>
    /// جدول المستخدم
    /// </summary>
    public class Users : IdentityUser
    {
        [Display(Name ="الاسم الرباعي")]
        [Required(AllowEmptyStrings = false, ErrorMessage ="يجب ادخال اسم المستخدم الرباعي")]
        public string FullName { get; set; }
        [Display(Name = "محذوف")]
        public bool Deleted { get; set; }
        [Display(Name = "تاريخ الميلاد")]
        [DataType(DataType.Date)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "يجب ادخال تاريخ الميلاد")]
        public DateTime Birthday { get; set; }
        [Display(Name = "تاريخ الإنشاء")]
        public DateTime CreatedDate { get; set; }
    }
    public class BaseTable
    {
        [Display(Name = "المعرف")]
        public Guid ID { get; set; }
        [Display(Name = "تاريخ الإنشاء")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "اخر تعديل")]
        public DateTime UpdatedDate { get; set; }
        public string UserID { get; set; }
        [Display(Name = "تم بوسطة")]
        public Users User { get; set; }
        [Display(Name = "محذوف")]
        public bool Deleted { get; set; }
    }
    public enum CurrencyType
    {
        [Description("شيقل جديد")]
        NewShekel,
        [Description("دولار")]
        Dollar,
        [Description("دينار اردني")]
        JordanianDinar,
    }
    /// <summary>
    /// جدول سند القبض
    /// </summary>
    public class CatchReceipts : BaseTable
    {
        [Display(Name = "وصلني من")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "يجب ادخال اسم المرسل")]
        public string FullName { get; set; }
        [Display(Name = "المبلغ بالارقام")]
        public decimal AmountOfMoneyNumber { get; set; }
        [Display(Name = "المبلغ بالحروف")]
        public string AmountOfMoneyText { get; set; }
        [Display(Name = "وذلك عن")]
        public string Reason { get; set; }
        [Display(Name = "لحساب")]
        public string ToAccount { get; set; }
        [Display(Name = "العملة")]
        public CurrencyType Currency { get; set; }
        public ICollection<CashiersCheck> CashiersChecks { get; set; }
    }
    /// <summary>
    /// جدول سند الصرف
    /// </summary>
    public class Receipts : BaseTable
    {
        [Display(Name = "المدفوع له")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "يجب ادخال اسم المدفوع له")]
        public string FullName { get; set; }
        [Display(Name = "المبلغ بالارقام")]
        public decimal AmountOfMoneyNumber { get; set; }
        [Display(Name = "المبلغ بالحروف")]
        public string AmountOfMoneyText { get; set; }
        [Display(Name = "وذلك عن")]
        public string Reason { get; set; }
        [Display(Name = "على حساب")]
        public string OnAccount { get; set; }
        [Display(Name = "وصلني من")]
        public string ReceivedFrom { get; set; }
        [Display(Name = "العملة")]
        public CurrencyType Currency { get; set; }
        public ICollection<CashiersCheck> CashiersChecks { get; set; }
    }

    /// <summary>
    /// جدول الشيكات
    /// </summary>
    public class CashiersCheck : BaseTable
    {
        public Guid CatchReceiptID { get; set; }
        public CatchReceipts CatchReceipt { get; set; }
        public Guid ReceiptID { get; set; }
        public Receipts Receipt { get; set; }
        [Display(Name = "الرقم")]
        public int NumberID { get; set; } 
        [Display(Name = "رقم الشيك")]
        public int CheckNumber { get; set; }
        [Display(Name = "رقم الحساب")]
        public string AccountNumber { get; set; }
        [Display(Name = "بنك")]
        public int CodeBank { get; set; }
        [Display(Name = "إسم البنك")]
        public string BankName { get; set; }
        [Display(Name = "فرع")]
        public int CodeBranch { get; set; }
        [Display(Name = "إسم الفرع")]
        public string BranchName { get; set; }
        [Display(Name = "يستحق في")]
        [DataType(DataType.Date)]
        public DateTime DateOfTheWorthy { get; set; }
        [Display(Name = "المبلغ")]
        public int AmountOfMoney { get; set; }
    }
    /// <summary>
    /// جدول شهادة البناء
    /// </summary>
    public class ConstructionLicense : BaseTable 
    {
        [Display(Name = "تاريخ اصدار الترخيص")]
        public DateTime DateOfIssuance { get; set; }
        [Display(Name = "رقم الملف ")]
        public int FileNumber { get; set; }
        [Display(Name = "صدق بجلسة رقم ")]
        public int ValidatedBySessionNumber { get; set; }
        [Display(Name = "اسم البلدة")]
        public string TownName { get; set; }
        [Display(Name = "اسم الحي")]
        public string District { get; set; }
        [Display(Name = "الشارع")]
        public string Street { get; set; }
        [Display(Name = "الحوض")]
        public int Basin { get; set; }
        [Display(Name = "القسيمة")]
        public int Part { get; set; }
        [Display(Name = "استعمال البناء")]
        public string ConstructionUse { get; set; }
        [Display(Name = "نوع البناء")]
        public ConstructionLicenseType ConstructionLicenseType { get; set; }
        [Display(Name = "معلومات صاحب الرخصة")]
        public LicenseHolderInformation LicenseHolderInformation { get; set; }
        [Display(Name = "رقم اللجنة المحلية")]
        public int LocalCommitteeNumber { get; set; }
        [Display(Name = "تاريخ مصادقة اللجنة المحلية")]
        public DateTime DateApprovalLocalCommittee { get; set; }
        [Display(Name = "وصف الرخصة")]
        public string LicenseDescription { get; set; }
        [Display(Name = "شروط الرخصة")]
        public string LicenseConditions { get; set; }
        [Display(Name = "رسوم (غير مستردة) بتاريخ")]
        public DateTime FeeDate { get; set; }
        [Display(Name = "تاريخ باقي الرسوم")]
        public DateTime RemainingFeesDate { get; set; }
        [Display(Name = "وصل الرسوم (غير مستردة) ")]
        public Guid BillOfFeesID { get; set; }
        public CatchReceipts BillOfFees { get; set; }
        [Display(Name = "وصل باقي الرسوم")]
        public Guid BillRemainingFeesID { get; set; }
        public CatchReceipts BillRemainingFees { get; set; }
    }
    /// <summary>
    /// جدول معلومات صاحب الرخصة
    /// </summary>
    public class LicenseHolderInformation : BaseTable
    {
        public Guid ConstructionLicenseID { get; set; }
        public ConstructionLicense ConstructionLicense { get; set; }
        [Display(Name = "اسم صاحب الرخصة")]
        public string NameLicenseHolder { get; set; }
        [Display(Name = "رقم هوية صاحب الرخصة")]
        public string IdentityNumberLicenseHolder { get; set; }
        [Display(Name = "عنوان صاحب الرخصة")]
        public string AddressLicenseHolder { get; set; }
        [Display(Name = "هاتف صاحب الرخصة")]
        public string PhoneNumberLicenseHolder { get; set; }
        [Display(Name = "اسم المكتب المصمم")]
        public string NameDesigningOffice { get; set; }
        [Display(Name = "رقم هوية المكتب المصمم ")]
        public string IdentityNumberDesignerOffice { get; set; }
        [Display(Name = "عنوان المكتب المصمم ")]
        public string AddressDesignerOffice { get; set; }
        [Display(Name = "هاتف المكتب المصمم")]
        public string PhoneNumberDesignerOffice { get; set; }
        [Display(Name = "اسم المهندس المشرف ")]
        public string NameSupervisingEngineer { get; set; }
        [Display(Name = "رقم هوية المهندس المشرف ")]
        public string IdentityNumberSupervisingEngineer { get; set; }
        [Display(Name = "عنوان المهندس المشرف")]
        public string AddressSupervisingEngineer { get; set; }
        [Display(Name = "هاتف المهندس المشرف")]
        public string PhoneNumberSupervisingEngineer { get; set; }

    }

    public enum ConstructionLicenseType
    {
        [Description("بناء جديد")]
        NewBuild,
        [Description("بناء إضافي")]
        AdditionalBuilding
    }
    /// <summary>
    /// جدول طلب اشتراك عداد المياه
    /// </summary>
    public class WaterMeterSubscriptionRequest : BaseTable
    {
        [Display(Name = "إسم المستفيد")]
        public string Name { get; set; }
        [Display(Name = "الموقع")]
        public string Location { get; set; }
        [Display(Name = "رقم الجوال")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Display(Name = "رقم الحوض")]
        public int BasinNumber { get; set; }
        [Display(Name = "رقم القطعة")]
        public int PieceNumber { get; set; }
        [Display(Name = "إقتراحات مسؤول المياه")]
        public string WaterOfficialSuggestions { get; set; }
        [Display(Name = "قرار البلدية")]
        public string MunicipalityDecision { get; set; }
    }
    /// <summary>
    /// جدول الشكاوي
    /// </summary>
    public class ComplaintForm : BaseTable
    {
        [Display(Name = "الاسم الرباعي")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "يجب ادخال الاسم الرباعي")]
        public string FullName { get; set; }
        [Display(Name = "رقم الجوال")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Display(Name = "رقم الهوية")]
        public string IdentificationNumber { get; set; }
        [Display(Name = "اسم المنطقة")]
        public string AreaName { get; set; }
        [Display(Name = "العنوان كاملا")]
        public string FullAddress { get; set; }
        [Display(Name = "نوع الشكوى")]
        public ComplaintType ComplaintType { get; set; }
        [Display(Name = "موضوع الشكوى")]
        public string SubjectComplaint { get; set; }
    }
    public enum ComplaintType
    {
        [Description("مياه")]
        Waters,
        [Description("تنظيم")]
        Organizing,
        [Description("صحة")]
        Health,
        [Description("نفايات")]
        Trash,
        [Description("ادارة")]
        Management,
        [Description("مالية")]
        Finance,
        [Description("طرق")]
        Ways,
        [Description("غير ذلك")]
        Other
    }
    /// <summary>
    /// جدول طلب اجازة
    /// </summary>
    public class VacationRequest : BaseTable
    {
        [Display(Name = "عدد ايام الاجازة")]
        public int DaysVacation { get; set; }
        [Display(Name = "تاريخ بداية الاجازة")]
        public DateTime StartVacationDate { get; set; }
        [Display(Name = "تاريخ نهاية الاجازة")]
        public DateTime EndVacationDate { get; set; }
        [Display(Name = "المدينة")]
        public string City { get; set; }
        [Display(Name = "الشارع")]
        public string Street { get; set; }
        [Display(Name = "رقم الهاتف")]
        public string PhoneNumber { get; set; }
        [Display(Name = "اسم الموكل اليه المهام")]
        public string NameAssignee { get; set; }
        [Display(Name = "الموافقة على طلب الاجازة")]
        public bool Agree { get; set; }
        [Display(Name = "نوع الاجازة")]
        public VacationType VacationType { get; set; }
    }
    public enum VacationType
    {
        [Description("سنوية")]
        Annual,
        [Description("عارضية")]
        Transverse,
        [Description("مرضية")]
        Satisfying,
        [Description("دراسية")]
        Study,
        [Description("بدون راتب")]
        WithoutSalary,
        [Description("امومة")]
        motherhood,
        [Description("حج")]
        Hajj
    }

    /// <summary>
    /// جدول شهادة الحرف والصناعة
    /// </summary>
    public class CraftAndIndustryLicense : BaseTable
    {
        [Display(Name = "اسم حامل الرخصة")]
        public string LicenseHolderName { get; set; }
        [Display(Name = "رقم الهوية")]
        public string IdentificationNumber { get; set; }
        [Display(Name = "نوع الحرفة او الصناعة")]
        public string CraftOrIndustryType { get; set; }
        [Display(Name = "صنفها في الذيل")]
        public string ClassifiedInTail { get; set; }
        [Display(Name = "عنوان المحل")]
        public string Address { get; set; }
        [Display(Name = "انتهائه")]
        public DateTime Ends { get; set; }
        [Display(Name = "رسوم الرخصة")]
        public decimal LicenseFee { get; set; }
        [Display(Name = "رقم الوصل ")]
        public string VoucherNumber { get; set; }
    }
}
