using System.ComponentModel.DataAnnotations;

namespace AvtoHubProject.Models
{
    public class AvtoHubRegisterModel
    {
        [Required(ErrorMessage ="Email adresiniz boş qala bilməz")]
        [Display(Name ="E-Poçt Adresiniz")]
        [EmailAddress(ErrorMessage ="Keçərli bir email adresi daxil edin")]
        public string Email { get; set; } = string.Empty;

        //[Display(Name = "Mobil Nömrə")]
        //[Required(ErrorMessage ="Mobil nömrəniz boş qala bilməz")]
        //[Phone(ErrorMessage ="Keçərli bir mobil nömrə daxil edin")]
        //public string PhoneNumber {  get; set; } =string.Empty;

        [Display(Name = "Şifrə")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Şifə boş qala bilməz")]
        public string Password {  get; set; } = string.Empty;


        [Display(Name = "Şifrə Təkrarı")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Şifə təkrarı boş qala bilməz")]
        [Compare("Password",ErrorMessage ="Şifrələr uyğun deyil")]
        public string ConfirmPassword {  get; set; } = string.Empty;
    }
}
