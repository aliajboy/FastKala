using System.ComponentModel.DataAnnotations;

namespace FastKala.Application.ViewModels.Global;

public class LoginViewModel
{
    [EmailAddress(ErrorMessage = "لطفا آدرس ایمیل معتبر وارد نمایید!")]
    [Required(ErrorMessage = "لطفا ایمیل خود را وارد نمایید!")]
    public required string Email { get; set; }
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "لطفا کلمه عبور را وارد نمایید!")]
    public required string Password { get; set; }
    public bool RememberMe { get; set; } = false;
}