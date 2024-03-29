﻿using fastwin.Infrastructure.JWT;
using fastwin.Requests;
using fastwin.Requests.Users.Commands.Register;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fastwin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMediator _mediator;

        public AuthController(IAuthService authService, IMediator mediator)
        {
            _authService = authService;
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegistrationRequest user, CancellationToken cancellationToken)
        {
            try
            {
                var registerCommand = new RegisterUserCommand(user);
                var result = await _mediator.Send(registerCommand, cancellationToken);

                if (result != null)
                {
                    // at this moment we don't have any email verification, so I'm logging user directly after registration
                    var loginReq = new LoginReq
                    {
                        Email = user.Email,
                        Password = user.Password
                    };

                    var tokenString = await _authService.GenerateJWTString(loginReq);

                    return Ok(new { accessToken = tokenString });
                }

                return BadRequest($"Failed to register {user.Email}");
            }
            catch (ValidationException ex)
            {
                return StatusCode(422, ex.Errors.Select(e => e.ErrorMessage));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred during registration. {ex.Message}\"");
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginReq user, CancellationToken cancellationToken)
        {
            try
            {
                var authenticateCommand = new AuthenticateUserCommand(user);
                var result = await _mediator.Send(authenticateCommand, cancellationToken);

                if (result != null)
                {
                    var tokenString = await _authService.GenerateJWTString(user);

                    return Ok(new { accessToken = tokenString });
                }

                return BadRequest($"Failed to login. Email or password is incorrect.");
            }
            catch (ValidationException ex)
            {
                return StatusCode(422, ex.Errors.Select(e => e.ErrorMessage));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred during login. {ex.Message}");
            }
        }

    }
}
