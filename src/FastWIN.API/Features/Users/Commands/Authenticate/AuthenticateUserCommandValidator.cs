using fastwin.Requests;
using FluentValidation;

namespace fastwin.Features.Users.Commands.Authenticate
{
    public class AuthenticateUserCommandValidator : AbstractValidator<LoginReq>
    {
        public AuthenticateUserCommandValidator() {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Please enter a valid email adress");
        }
    }
}
