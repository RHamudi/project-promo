using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Promocoes.Application.Input.Commands.BusinessContext;
using Promocoes.Application.Input.Commands.UserContext;
using Promocoes.Application.Output.DTOs;

namespace Promocoes.API.Controllers
{
    [ApiController]
    [Route("api/user/")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("insert")]
        public async Task<IActionResult> Insert([FromBody] UserCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match<IActionResult>(
                user => Ok(user),
                error => BadRequest(error)
                );
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new UserDTO());
            return Ok(result);
        }

        [HttpPatch("updateuser")]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match<IActionResult>(
                (user) => Ok(user),
                (error) => BadRequest(error));
        }

        [OutputCache(NoStore = true, Duration = 0)]
        [HttpPost("Authentication")]
        public async Task<IActionResult> Authentication([FromBody] AuthenticationCommand model)
        {
            var result = await _mediator.Send(model);
            return result.Match<IActionResult>(
                Ok,
                BadRequest
                );
        }

        [OutputCache(NoStore = true, Duration = 0)]
        [HttpGet("VerifyUser")]
        public async Task<IActionResult> VerifyUser([FromQuery] VerificationCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match<IActionResult>(
                Ok,
                BadRequest
            );
        }
    }
}