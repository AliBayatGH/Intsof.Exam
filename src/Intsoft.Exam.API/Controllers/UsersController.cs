using Intsoft.Exam.Application.Contracts;
using Intsoft.Exam.Application.Models;
using Microsoft.AspNetCore.Mvc;


namespace Intsoft.Exam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserManagerService _userManagerService;

        public UsersController(IUserManagerService userManagerService)
        {
            _userManagerService = userManagerService;
        }

        
        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> Get(int id,CancellationToken cancellationToken)
        {
            return Ok( await _userManagerService.GetUserAsync(id, cancellationToken));
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateUserModel model, CancellationToken cancellationToken)
        {
            await  _userManagerService.CreateUserAsync(model,cancellationToken);
            return Ok();
        }

        // PUT api/<UsersController>/5
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateUserModel model, CancellationToken cancellationToken)
        {
            await _userManagerService.UpdateUserAsync(model, cancellationToken);
            return Ok();
        }

    }
}
