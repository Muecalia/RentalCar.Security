using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Security.Application.Commands.Request.Users;
using RentalCar.Security.Application.Queries.Request.Users;

namespace RentalCar.Security.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new FindAllUsersRequest(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("getById/{Id}")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<ActionResult> GetById(string Id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new FindUserByIdRequest(Id), cancellationToken);
            if (result.Succeeded)
                return Ok(result);
            return NotFound(result.Message);
        }

        [HttpPost]
        [AllowAnonymous]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<ActionResult> Create(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (result.Succeeded)
                return StatusCode(201, result);
            return BadRequest(result.Message);
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<ActionResult> Update(string Id, UpdateUserRequest request, CancellationToken cancellationToken)
        {
            request.Id = Id;
            var result = await _mediator.Send(request, cancellationToken);
            if (result.Succeeded)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpPut("changePassword/{Id}")]
        public async Task<ActionResult> ChangePassword(string Id, ChangePasswordUserRequest request, CancellationToken cancellationToken)
        {
            request.Id = Id;
            var result = await _mediator.Send(request, cancellationToken);
            if (result.Succeeded)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<ActionResult> Delete(string Id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteUserRequest(Id), cancellationToken);
            if (result.Succeeded)
                return Ok(result);
            return NotFound(result.Message);
        }

    }
}
