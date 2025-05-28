using FluentValidation;

namespace LiveLib.Api.Models
{
    public class UserLoginValidation : AbstractValidator<UserLoginDto>
    {
        public UserLoginValidation()
        {
            RuleFor(u => u.Password)
                .NotEmpty()
                .MinimumLength(8)
                .WithMessage("Password must be greater then 8")
                .MaximumLength(100)
                .WithMessage("Password must be less then 100");

            RuleFor(u => u.Username)
                .NotEmpty()
                .WithMessage("Username must not be empty");
        }
    }
}
