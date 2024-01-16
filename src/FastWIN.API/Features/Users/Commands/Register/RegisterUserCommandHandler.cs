using MediatR;
using Microsoft.AspNetCore.Identity;
using fastwin.Entities;
using FluentValidation;

namespace fastwin.Requests.Users.Commands.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, User>
    {
        private readonly UserManager<User> _userManager;
        private readonly IValidator<RegistrationRequest> _registerValidator;

        public RegisterUserCommandHandler(UserManager<User> userManager, IValidator<RegistrationRequest> registerValidator)
        {
            _userManager = userManager;
            _registerValidator = registerValidator;
        }

        public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var identityUser = new User
            {
                UserName = request.RegistrationRequest.UserName,
                Email = request.RegistrationRequest.Email
            };

            await _registerValidator.ValidateAndThrowAsync(request.RegistrationRequest);

            var result = await _userManager.CreateAsync(identityUser, request.RegistrationRequest.Password);

            return identityUser;
        }
    }
}
