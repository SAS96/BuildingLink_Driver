using BuildingLink_Driver.Models;
using BuildingLink_Driver.Services;
using Microsoft.AspNetCore.Mvc;

namespace BuildingLink_Driver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FakeController : Controller
    {
        public readonly HelperService _helperService;
        public readonly DriversService _driversService;

        public FakeController(HelperService generatorService, DriversService driversService)
        {
            _helperService = generatorService;
            _driversService = driversService;
        }

        [HttpPost]
        [Route("GenerateDrivers/{count}")]
        public IActionResult GenerateDrivers(int count)
        {
            _helperService.CreateRandomDrivers(count);
            return NoContent();
        }

        [HttpGet]
        [Route("GetAlphabetized")]
        public IActionResult GetAlphabetized()
        {
            return Ok(_helperService.Alphabetize());
        }

        [HttpGet]
        [Route("GetAlphabetized/{id}")]
        public IActionResult GetAlphabetized(int id)
        {
            Driver? driver = _driversService.Get(id);
            if (driver is null)
                return NotFound();

            return Ok(_helperService.Alphabetize(driver));
        }
    }
}