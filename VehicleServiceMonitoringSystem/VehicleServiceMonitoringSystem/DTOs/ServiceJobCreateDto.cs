using System.ComponentModel.DataAnnotations;

namespace VehicleServiceMonitoringSystem.DTOs
{
    public class ServiceJobCreateDto
    {
        [Required]
        [Display(Name = "Customer Name")]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Contact Number")]
        [Phone]
        public string ContactNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Vehicle Make")]
        [StringLength(50)]
        public string VehicleMake { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Vehicle Model")]
        [StringLength(50)]
        public string VehicleModel { get; set; } = string.Empty;

        [Required]
        [Range(1900, 2100)]
        [Display(Name = "Model Year")]
        public int ModelYear { get; set; }

        [Required]
        [Display(Name = "Plate Number")]
        [StringLength(20)]
        public string PlateNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Vehicle Color")]
        [StringLength(30)]
        public string VehicleColor { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Service Type")]
        public string ServiceType { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Service Bay")]
        public string ServiceBay { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Expected Release Date")]
        public DateTime ExpectedReleaseDate { get; set; }

        [Display(Name = "Remarks")]
        [StringLength(500)]
        public string Remarks { get; set; } = string.Empty;
    }
}