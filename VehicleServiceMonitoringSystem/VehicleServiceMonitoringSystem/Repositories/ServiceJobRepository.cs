using VehicleServiceMonitoringSystem.Models;

namespace VehicleServiceMonitoringSystem.Repositories
{
    public class ServiceJobRepository : IServiceJobRepository
    {
        private static readonly List<ServiceJob> ServiceJobs = new();

        public List<ServiceJob> GetAll()
        {
            return ServiceJobs;
        }

        public ServiceJob? GetById(int id)
        {
            return ServiceJobs.FirstOrDefault(s => s.Id == id);
        }

        public void Add(ServiceJob serviceJob)
        {
            serviceJob.Id = ServiceJobs.Count + 1;

            ServiceJobs.Add(serviceJob);
        }

        public void Update(ServiceJob serviceJob)
        {
            var existingJob = GetById(serviceJob.Id);

            if (existingJob == null)
            {
                return;
            }

            existingJob.CustomerName = serviceJob.CustomerName;

            existingJob.ContactNumber = serviceJob.ContactNumber;

            existingJob.VehicleMake = serviceJob.VehicleMake;

            existingJob.VehicleModel = serviceJob.VehicleModel;

            existingJob.ModelYear = serviceJob.ModelYear;

            existingJob.PlateNumber = serviceJob.PlateNumber;

            existingJob.VehicleColor = serviceJob.VehicleColor;

            existingJob.ServiceType = serviceJob.ServiceType;

            existingJob.ServiceBay = serviceJob.ServiceBay;

            existingJob.ExpectedReleaseDate =
                serviceJob.ExpectedReleaseDate;

            existingJob.ActualReleaseDateTime =
                serviceJob.ActualReleaseDateTime;

            existingJob.Status = serviceJob.Status;

            existingJob.Remarks = serviceJob.Remarks;
        }

        public void Release(int id)
        {
            var serviceJob = GetById(id);

            if (serviceJob == null)
            {
                return;
            }

            serviceJob.Status = "Released";

            serviceJob.ActualReleaseDateTime = DateTime.Now;
        }
    }
}