using Microsoft.AspNetCore.Mvc;
using NotatnikVeloce.Models;
using NotatnikVeloce.Services.Interfaces;

namespace NotatnikVeloce.Controllers
{

    [Route("api/Users")]
    [ApiController]
    [ValidateAntiForgeryToken]

    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userService.GetUsers();
            if(users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser([FromRoute]Guid id)
        {
            var user = _userService.GetUser(id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create([FromBody]UserDto userdto) 
        {
            var user = _userService.AddUser(userdto);
            if(user == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult EditUser([FromRoute]Guid id, [FromBody]UserDto userDto)
        {
            var user = _userService.UpdateUser(id, userDto);
            if(user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser([FromRoute]Guid id) 
        {
            var result = _userService.DeleteUser(id);
            if (result == false)
            {
                return BadRequest();
            }
            return Ok();   
        }
    }
}
