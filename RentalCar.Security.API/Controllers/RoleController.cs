using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Security.Application.Commands.Request.Roles;
using RentalCar.Security.Application.Queries.Request.Roles;

namespace RentalCar.Security.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new FindAllRolesRequest(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetById(string Id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new FindRoleByIdRequest(Id), cancellationToken);
            if (result.Succeeded)
                return Ok(result);
            return NotFound(result.Message);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateRoleRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (result.Succeeded)
                return StatusCode(201, result);
            return BadRequest(result.Message);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(string Id, UpdateRoleRequest request, CancellationToken cancellationToken)
        {
            request.Id = Id;
            var result = await _mediator.Send(request, cancellationToken);
            if (result.Succeeded)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(string Id, CancellationToken cancellationToken)
        {
            // request, CancellationToken cancellationToken
            var result = await _mediator.Send(new DeleteRoleRequest(Id), cancellationToken);
            if (result.Succeeded)
                return Ok(result);
            return NotFound(result.Message);
        }

    }
}
