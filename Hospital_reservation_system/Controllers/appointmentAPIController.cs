using Hospital_reservation_system.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_reservation_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class appointmentAPIController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;

        public appointmentAPIController(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet]
        public IActionResult GetPoliclinics()
        {
            var policlinics = _databaseContext.Policlinics.ToList();
            return Ok(policlinics);
        }
    }
}
