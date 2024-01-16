using MediatR;
using Microsoft.AspNetCore.Identity;
using fastwin.Entities;
using FluentValidation;

namespace fastwin.Requests.Users.Commands.Authenticate
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, User>
    {
        private readonly UserManager<User> _userManager;
        private readonly IValidator<LoginReq> _authValidator;

        public AuthenticateUserCommandHandler(UserManager<User> userManager, IValidator<LoginReq> authValidator)
        {
            _userManager = userManager;
            _authValidator = authValidator;
        }

        public async Task<User> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            await _authValidator.ValidateAsync(request.LoginRequest);

            var identityUser = await _userManager.FindByEmailAsync(request.Email);

            if (identityUser == null || !await _userManager.CheckPasswordAsync(identityUser, request.Password))
            {
                return null;
            }

            return identityUser;
        }
    }
}
