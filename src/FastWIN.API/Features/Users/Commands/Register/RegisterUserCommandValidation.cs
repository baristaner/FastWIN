using fastwin.Requests;
using FluentValidation;

namespace fastwin.Features.Users.Commands.Register
{
    public class RegisterUserCommandValidation : AbstractValidator<RegistrationRequest>
    {
        public RegisterUserCommandValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().Length(4,12).WithMessage("Username must be 4 to 12 characters.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Please enter a valid email adress");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
        }
    }
}
