using FluentValidation;

namespace Application.Core.User.Command.Update;
public class UpdateUserValidation : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidation()
    {
        RuleFor(i => i.name).NotEmpty().NotNull().WithMessage("first name is not valid");

        RuleFor(i => i.family).NotEmpty().NotNull().WithMessage("last name is not valid");

        RuleFor(i => i.phoneNumber).Must(ValidPhoneNumber).WithMessage("invalid phoneNumber");
    }

    private bool ValidPhoneNumber(string phone)
    {
        if (phone.Length is not 10)
            return false;

        if (!phone.StartsWith("09"))
            return false;

        return true;
    }
}
