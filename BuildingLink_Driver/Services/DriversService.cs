using BuildingLink_Driver.Data;
using BuildingLink_Driver.Models;

namespace BuildingLink_Driver.Services
{
    public class DriversService
    {
        private readonly ILogger<DriversService> _logger;
        private readonly IDriversRepository _driversRepository;

        public DriversService(IDriversRepository driversRepository, ILogger<DriversService> logger)
        {
            _driversRepository = driversRepository;
            _logger = logger;
        }

        /// <summary>
        /// Gets a list of all drivers.
        /// </summary>
        /// <returns>A list of drivers.</returns>
        public List<Driver> Get()
        {
            _logger.LogInformation("Getting all drivers...");

            var drivers = _driversRepository.Get();

            _logger.LogInformation("Got {DriverCount} drivers.", drivers.Count);

            return drivers;
        }

        /// <summary>
        /// Gets a driver by id.
        /// </summary>
        /// <param name="id">The id of the driver to get.</param>
        /// <returns>A driver or null if the driver does not exist.</returns>
        public Driver? Get(int id)
        {
            _logger.LogInformation("Getting driver with id {DriverId}...", id);

            var driver = _driversRepository.Get(id);

            if (driver is null)
                _logger.LogWarning("Driver with id {DriverId} does not exist.", id);
            else
                _logger.LogInformation("Got driver with id {DriverId}.", id);

            return driver;
        }

        /// <summary>
        /// Adds a new driver.
        /// </summary>
        /// <param name="driver">The driver to add.</param>
        /// <returns>The number of rows affected by the insert.</returns>
        public int Add(Driver driver)
        {
            _logger.LogInformation("Adding driver...");

            int affectedRows = _driversRepository.Add(driver);

            if (affectedRows > 0)
                _logger.LogInformation("Added driver.");
            else
                _logger.LogWarning("Driver couldn't be added.");

            return affectedRows;
        }

        /// <summary>
        /// Updates an existing driver.
        /// </summary>
        /// <param name="driver">The driver to update.</param>
        /// <returns>The number of rows affected by the update.</returns>
        public int Update(Driver driver)
        {
            _logger.LogInformation("Updating driver with id {DriverId}...", driver.Id);

            var affectedRows = _driversRepository.Update(driver);

            if (affectedRows > 0)
                _logger.LogInformation("Updated driver with id {DriverId}.", driver.Id);
            else
                _logger.LogWarning("Driver with id {DriverId} could not be updated.", driver.Id);

            return affectedRows;
        }

        /// <summary>
        /// Deletes a driver by id.
        /// </summary>
        /// <param name="id">The id of the driver to delete.</param>
        /// <returns>The number of rows affected by the delete.</returns>
        public int Delete(int id)
        {
            _logger.LogInformation("Deleting driver with id {DriverId}...", id);

            var affectedRows = _driversRepository.Delete(id);

            if (affectedRows > 0)
                _logger.LogInformation("Deleted driver with id {DriverId}.", id);
            else
                _logger.LogWarning("Driver with id {DriverId} could not be deleted.", id);

            return affectedRows;
        }
    }
}