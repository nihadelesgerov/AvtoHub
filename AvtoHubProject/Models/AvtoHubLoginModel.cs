using System.ComponentModel.DataAnnotations;

namespace AvtoHubProject.Models
{
    public class AvtoHubLoginModel
    {
        [Required(ErrorMessage = "Email adresiniz boş qala bilməz")]
        [Display(Name = "E-Poçt Adresiniz")]
        [EmailAddress(ErrorMessage = "Keçərli bir email adresi daxil edin")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Şifrə")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifə boş qala bilməz")]
        public string Password { get; set; } = string.Empty;


        public bool RememberMe { get; set; }    
    }
}
