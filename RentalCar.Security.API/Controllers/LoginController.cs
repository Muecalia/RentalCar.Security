using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Security.Application.Commands.Request.Login;

namespace RentalCar.Security.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (result.Succeeded) 
                return Ok(result);
            return BadRequest(result);
        }
    }
}
