using BuildingLink_Driver.Models;
using BuildingLink_Driver.Services;
using Microsoft.AspNetCore.Mvc;

namespace BuildingLink_Driver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriversController : Controller
    {
        public readonly DriversService _driversService;

        public DriversController(DriversService driversService)
        {
            _driversService = driversService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_driversService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var driver = _driversService.Get(id);

            if (driver is null)
                return NotFound();

            return Ok(driver);
        }

        [HttpPost]
        public IActionResult Add(Driver driver)
        {
            int affectedRows = _driversService.Add(driver);
            if (affectedRows == 0)
                return Conflict();
            else if (affectedRows < 0)
                return BadRequest();

            return NoContent();
        }

        [HttpPatch]
        public IActionResult Update(Driver updatedDriver)
        {
            int affectedRows = _driversService.Update(updatedDriver);

            if (affectedRows == 0)
                return NotFound();
            else if (affectedRows < 0)
                return BadRequest();

            return Ok(updatedDriver);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int affectedRows = _driversService.Delete(id);

            if (affectedRows == 0)
                return NotFound();
            else if (affectedRows < 0)
                return BadRequest();

            return NoContent();
        }
    }
}