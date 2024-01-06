using System.ComponentModel.DataAnnotations;

namespace FastKala.Application.ViewModels.Global;
public enum ImageType
{
    [Display(Name = "محصولات")]
    Product = 1,
    [Display(Name = "محصولات")]
    ProductImage = 2,
    [Display(Name = "لیست تصاویر محصولات")]
    Article = 3,
    [Display(Name = "مقاله")]
    Slider = 4,
    [Display(Name = "اسلایدر")]
    ProductType = 5,
    [Display(Name = "گروه مقالات")]
    ArticleType = 6,
    [Display(Name = "کاربر")]
    User = 7,
    [Display(Name = "برند")]
    Brand = 8,
    [Display(Name = "سایت")]
    Site = 9,
    [Display(Name = "مشتریان ویژه")]
    SpecialCustomer = 10,
    [Display(Name = " تخفیف")]
    Discount = 11,
    [Display(Name = " نظرسنجی")]
    Poll = 12,
    [Display(Name = " رزرو")]
    Reserve = 13,
    [Display(Name = "عکس پروفایل")]
    UserProfileImages = 14,
    [Display(Name = "شخص حقیقی")]
    RealPersonUploads = 15,
    [Display(Name = "شخص حقوقی")]
    LegalPersonUploads = 16,
    [Display(Name = "مدارک ملکی")]
    CitizenProperty = 17,
    [Display(Name = "مدارک خودرو")]
    CitizenCarDocument = 18,
    [Display(Name = "تیکت")]
    TicketUploads = 19,
    [Display(Name = "دفتر")]
    Office = 20,
    [Display(Name = "باجه فروش")]
    BoxOffice = 21,
    [Display(Name = "گروه استفاده کننده")]
    Group = 22,
    [Display(Name = "ایستگاه بازی")]
    Station = 23,
    [Display(Name = "بلیط")]
    Ticket = 24,
    [Display(Name = "گروه کاربری")]
    UserGroup = 25
}