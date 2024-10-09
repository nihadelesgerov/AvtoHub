using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AvtoHubProject.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Ban növü")]
        [Required(ErrorMessage = "Ban növünü daxil edin")]
        public string BanType { get; set; } = string.Empty;

        [Display(Name = "Şəhər")]
        [Required(ErrorMessage = "Şəhər seçin")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Avtomobilin Markasını seçin")]
        [Display(Name = "Marka")]
        public string Marka { get; set; } = string.Empty;

        [Required(ErrorMessage = "Modeli seçin")]
        [Display(Name = "Model")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "NV - nin rəngini seçin")]
        [Display(Name = "Rəng")]
        public string Color { get; set; } = string.Empty;

        [Display(Name = "Detallı Məlumat")]
        public string DetailedInformation { get; set; } = string.Empty;

        [Display(Name = "Yürüş")]
        [Required(ErrorMessage = "NV - nin yürüş məsafəsini daxil edin")]
        public long MileAge { get; set; }

        [Required(ErrorMessage = "NV - nin istehsal ilini seçin")]
        [Display(Name = "İstehsal Tarixi")]
        public int ProductionYear { get; set; }

        [Required(ErrorMessage = "NV - nin Mühərrik gücünü seçin")]
        [Display(Name = "Mühərrik Gücü")]
        public int EnginePower { get; set; } 

        [Required(ErrorMessage = "NV - nin ötürücü tipini seçin")]
        [Display(Name = "Sürətlər Qutusu")]
        public string GearBoxType { get; set; } = string.Empty;

        [Required(ErrorMessage = "NV - nin Qiymətini daxil edin")]
        [Display(Name = "Qiymət")]
        public long Price { get; set; }

        [Required(ErrorMessage = "NV - nin yanacaq növünü seçin")]
        [Display(Name = "Yanacaq Növü")]
        public string OilType { get; set; } = string.Empty;

        [NotMapped]
        public List<IFormFile?> FormFile { get; set; }

        public string? ImagePath { get; set; }

        public string UserId { get; set; }

        public AvtoHubUser AvtoHubUser { get; set; }
    }
}
