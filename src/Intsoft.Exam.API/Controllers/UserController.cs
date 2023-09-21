using Application.Core.User.Command.Create;
using Application.Core.User.Command.Update;
using Application.Core.User.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Intsoft.Exam.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken ct)
    => Ok(await _mediator.Send(new GetUserQuery(id), ct));

    [HttpPost]
    public async Task<IActionResult> Add(AddUserCommand command, CancellationToken ct)
    => Ok(await _mediator.Send(command, ct));


    [HttpPut]
    public async Task<IActionResult> Update(UpdateUserCommand command, CancellationToken ct)
    => Ok(await _mediator.Send(command, ct));
}
