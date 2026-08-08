using System.ComponentModel.DataAnnotations;

namespace VehicleServiceMonitoringSystem.DTOs
{
    public class ServiceJobEditDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [Phone]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Display(Name = "Vehicle Make")]
        public string VehicleMake { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Display(Name = "Vehicle Model")]
        public string VehicleModel { get; set; } = string.Empty;

        [Required]
        [Range(1900, 2100)]
        [Display(Name = "Model Year")]
        public int ModelYear { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Plate Number")]
        public string PlateNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        [Display(Name = "Vehicle Color")]
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

        [Required]
        public string Status { get; set; } = string.Empty;

        [StringLength(500)]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; } = string.Empty;
    }
}