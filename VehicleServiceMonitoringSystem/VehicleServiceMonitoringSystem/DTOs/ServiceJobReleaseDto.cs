using System.ComponentModel.DataAnnotations;

namespace VehicleServiceMonitoringSystem.DTOs
{
    public class ServiceJobReleaseDto
    {
        public int Id { get; set; }

        [Display(Name = "Service Number")]
        public string ServiceNumber { get; set; } = string.Empty;

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; } = string.Empty;

        [Display(Name = "Vehicle")]
        public string Vehicle { get; set; } = string.Empty;

        [Display(Name = "Plate Number")]
        public string PlateNumber { get; set; } = string.Empty;

        [Display(Name = "Service Type")]
        public string ServiceType { get; set; } = string.Empty;

        [Display(Name = "Check-in Date & Time")]
        public DateTime CheckInDateTime { get; set; }

        [Display(Name = "Actual Release Date & Time")]
        [DataType(DataType.DateTime)]
        public DateTime? ActualReleaseDateTime { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;

        [StringLength(500)]
        public string Remarks { get; set; } = string.Empty;
    }
}