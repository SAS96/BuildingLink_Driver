using Bogus;
using BuildingLink_Driver.Data;
using BuildingLink_Driver.Models;

namespace BuildingLink_Driver.Services
{
    public class HelperService
    {
        private readonly ILogger<HelperService> _logger;
        private readonly IDriversRepository _driversRepository;

        public HelperService(IDriversRepository driversRepository, ILogger<HelperService> logger)
        {
            _driversRepository = driversRepository;
            _logger = logger;
        }

        /// <summary>
        /// Creates a list of random drivers using the Bogus library.
        /// </summary>
        /// <param name="count">The number of drivers to create.</param>
        /// <returns>A list of random drivers.</returns>
        public void CreateRandomDrivers(int count)
        {
            _logger.LogInformation("Creating {count} random drivers...", count);

            List<Driver> drivers = new();

            for (int i = 0; i < count; i++)
            {
                Faker faker = new();
                drivers.Add(new Driver
                {
                    FirstName = faker.Person.FirstName,
                    LastName = faker.Person.LastName,
                    Email = faker.Person.Email,
                    PhoneNumber = faker.Phone.PhoneNumberFormat()
                });
            }

            _driversRepository.BulkInsert(drivers);

            _logger.LogInformation("Created {driverCount} random drivers.", drivers.Count);
        }

        /// <summary>
        /// Sorts the driver's name alphabetically.
        /// </summary>
        /// <param name="driver">The driver that will have it's name sorted.</param>
        /// <returns>A string of the driver's sorted name.</returns>
        public string Alphabetize(Driver driver)
        {
            _logger.LogInformation("Alphabetizing {driver}...", $"{driver.FirstName} {driver.LastName}");

            string alphabetizedFirstName = new(driver.FirstName
                .ToCharArray()
                .OrderBy(c => c.ToString(), StringComparer.OrdinalIgnoreCase)
                .ToArray());

            string alphabetizedLastName = new(driver.LastName
                .ToCharArray()
                .OrderBy(c => c.ToString(), StringComparer.OrdinalIgnoreCase)
                .ToArray());

            _logger.LogInformation("Alphabetized {driver}: {alphabetizedName}",
                $"{driver.FirstName} {driver.LastName}",
                $"{alphabetizedFirstName} {alphabetizedLastName}");

            return $"{alphabetizedFirstName} {alphabetizedLastName}";
        }

        /// <summary>
        /// Sort all the drivers' names.
        /// </summary>
        /// <returns>A list of all the drivers' sorted name.</returns>
        public List<string> Alphabetize()
        {
            List<string> alphabetizedNames = new();

            foreach (Driver driver in _driversRepository.Get())
            {
                alphabetizedNames.Add(Alphabetize(driver));
            }

            return alphabetizedNames;
        }
    }
}