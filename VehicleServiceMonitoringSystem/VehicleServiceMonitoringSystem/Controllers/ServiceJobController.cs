using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleServiceMonitoringSystem.DTOs;
using VehicleServiceMonitoringSystem.Models;
using VehicleServiceMonitoringSystem.Repositories;

namespace VehicleServiceMonitoringSystem.Controllers
{
    [Authorize]
    public class ServiceJobController : Controller
    {
        private readonly IServiceJobRepository _serviceJobRepository;

        public ServiceJobController(
            IServiceJobRepository serviceJobRepository)
        {
            _serviceJobRepository = serviceJobRepository;
        }

        // =========================
        // SERVICE MONITORING
        // =========================

        [HttpGet]
        public IActionResult Index(string? search)
        {
            var serviceJobs = _serviceJobRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(search))
            {
                serviceJobs = serviceJobs
                    .Where(s =>
                        s.ServiceNumber.Contains(
                            search,
                            StringComparison.OrdinalIgnoreCase)

                        || s.CustomerName.Contains(
                            search,
                            StringComparison.OrdinalIgnoreCase)

                        || s.PlateNumber.Contains(
                            search,
                            StringComparison.OrdinalIgnoreCase)

                        || s.VehicleMake.Contains(
                            search,
                            StringComparison.OrdinalIgnoreCase)

                        || s.VehicleModel.Contains(
                            search,
                            StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            ViewBag.Search = search;

            return View(serviceJobs);
        }

        // =========================
        // CREATE / REGISTER VEHICLE
        // =========================

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ServiceJobCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var serviceJob = new ServiceJob
            {
                ServiceNumber = GenerateServiceNumber(),

                CustomerName = dto.CustomerName,

                ContactNumber = dto.ContactNumber,

                VehicleMake = dto.VehicleMake,

                VehicleModel = dto.VehicleModel,

                ModelYear = dto.ModelYear,

                PlateNumber = dto.PlateNumber,

                VehicleColor = dto.VehicleColor,

                ServiceType = dto.ServiceType,

                ServiceBay = dto.ServiceBay,

                CheckInDateTime = DateTime.Now,

                ExpectedReleaseDate = dto.ExpectedReleaseDate,

                Status = "Waiting",

                Remarks = dto.Remarks
            };

            _serviceJobRepository.Add(serviceJob);

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // EDIT VEHICLE - GET
        // =========================

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var serviceJob = _serviceJobRepository.GetById(id);

            if (serviceJob == null)
            {
                return NotFound();
            }

            var dto = new ServiceJobEditDto
            {
                Id = serviceJob.Id,

                CustomerName = serviceJob.CustomerName,

                ContactNumber = serviceJob.ContactNumber,

                VehicleMake = serviceJob.VehicleMake,

                VehicleModel = serviceJob.VehicleModel,

                ModelYear = serviceJob.ModelYear,

                PlateNumber = serviceJob.PlateNumber,

                VehicleColor = serviceJob.VehicleColor,

                ServiceType = serviceJob.ServiceType,

                ServiceBay = serviceJob.ServiceBay,

                ExpectedReleaseDate =
                    serviceJob.ExpectedReleaseDate,

                Status = serviceJob.Status,

                Remarks = serviceJob.Remarks
            };

            return View(dto);
        }

        // =========================
        // EDIT VEHICLE - POST
        // =========================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            int id,
            ServiceJobEditDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var existingJob = _serviceJobRepository.GetById(id);

            if (existingJob == null)
            {
                return NotFound();
            }

            existingJob.CustomerName = dto.CustomerName;

            existingJob.ContactNumber = dto.ContactNumber;

            existingJob.VehicleMake = dto.VehicleMake;

            existingJob.VehicleModel = dto.VehicleModel;

            existingJob.ModelYear = dto.ModelYear;

            existingJob.PlateNumber = dto.PlateNumber;

            existingJob.VehicleColor = dto.VehicleColor;

            existingJob.ServiceType = dto.ServiceType;

            existingJob.ServiceBay = dto.ServiceBay;

            existingJob.ExpectedReleaseDate =
                dto.ExpectedReleaseDate;

            existingJob.Status = dto.Status;

            existingJob.Remarks = dto.Remarks;

            _serviceJobRepository.Update(existingJob);

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // VIEW DETAILS
        // =========================

        [HttpGet]
        public IActionResult Details(int id)
        {
            var serviceJob = _serviceJobRepository.GetById(id);

            if (serviceJob == null)
            {
                return NotFound();
            }

            return View(serviceJob);
        }

        // =========================
        // RELEASE VEHICLE - GET
        // =========================

        [HttpGet]
        public IActionResult Release(int id)
        {
            var serviceJob = _serviceJobRepository.GetById(id);

            if (serviceJob == null)
            {
                return NotFound();
            }

            // Prevent releasing an already released vehicle
            if (serviceJob.Status == "Released")
            {
                return RedirectToAction(nameof(Details), new { id });
            }

            var dto = new ServiceJobReleaseDto
            {
                Id = serviceJob.Id,

                ServiceNumber = serviceJob.ServiceNumber,

                CustomerName = serviceJob.CustomerName,

                Vehicle =
                    $"{serviceJob.VehicleMake} {serviceJob.VehicleModel}",

                PlateNumber = serviceJob.PlateNumber,

                ServiceType = serviceJob.ServiceType,

                CheckInDateTime =
                    serviceJob.CheckInDateTime,

                ActualReleaseDateTime = null,

                Status = serviceJob.Status,

                Remarks = serviceJob.Remarks
            };

            return View(dto);
        }

        // =========================
        // RELEASE VEHICLE - POST
        // =========================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Release(
     int id,
     ServiceJobReleaseDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var serviceJob = _serviceJobRepository.GetById(id);

            if (serviceJob == null)
            {
                return NotFound();
            }

            if (serviceJob.Status == "Released")
            {
                return RedirectToAction(nameof(Index));
            }

            serviceJob.ActualReleaseDateTime = DateTime.Now;
            serviceJob.Status = "Released";

            _serviceJobRepository.Update(serviceJob);

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // GENERATE SERVICE NUMBER
        // =========================

        private string GenerateServiceNumber()
        {
            var serviceJobs = _serviceJobRepository.GetAll();

            int nextNumber = serviceJobs.Count + 1;

            return $"SV-{nextNumber:D4}";
        }
    }
}