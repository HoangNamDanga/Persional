using DemoCtyLamHai.Application.Command.Handler;
using DemoCtyLamHai.Application.Command.Model;
using DemoCtyLamHai.Application.Query.Handler;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoCtyLamHai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCtyLamHaiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserCtyLamHaiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserCreateCommandModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(new CreateUserCommand { UserCreate = model });
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UserUpdateCommandModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(new UpdateUserCommand { UpdateCommand = model });
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteUserCommand { Id = id });
            return Ok(result);
        }

        [HttpGet("Get-All")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllUserQuery());

            if (result == null || result.Count == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }
    }
}
