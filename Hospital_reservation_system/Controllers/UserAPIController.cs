using Hospital_reservation_system.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_reservation_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAPIController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public UserAPIController(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
        }
        // GET: api/<UserAPIController>
        [HttpGet]
        public List<User> Get()
        {
            var yazarlar = _databaseContext.Users.ToList();
            // normalde json formatına cevirip gondermem lazım  [ApiController] bunu otomatik yapıyor
            return yazarlar;
        }

        // GET api/<UserAPIController>/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var yazar = _databaseContext.Users.FirstOrDefault(z => z.Id == id.ToString());
            if (yazar == null)
            {
                return NotFound();
            }
            return yazar;
        }

        // POST api/<UserAPIController>
        [HttpPost]
        public IActionResult Post([FromBody] User y)
        {
            //if (ModelState.IsValid)  [ApiController] doğrulamayı yapoıypr
            _databaseContext.Users.Add(y);
            _databaseContext.SaveChanges();
            return Ok(y);
        }

        // PUT api/<UserAPIController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int? id, [FromBody] User y)
        {
            if (id is null)
            {
                return NotFound();
            }
            var user = _databaseContext.Users.FirstOrDefault(z => z.Id == id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            user.Username = y.Username;
            user.Username = y.Username  ;
            _databaseContext.Update(user);
            _databaseContext.SaveChanges();
            return Ok(user);
        }

        // DELETE api/<UserAPIController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var user = _databaseContext.Users.FirstOrDefault(z => z.Id == id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            _databaseContext.Users.Remove(user);
            _databaseContext.SaveChanges();
            return Ok(user);
        }

    }
}
