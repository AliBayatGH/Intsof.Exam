using FluentValidation;

namespace Application.Core.User.Command.Create;
public class AddUserValidation : AbstractValidator<AddUserCommand>
{
    public AddUserValidation()
    {
        RuleFor(i => i.firstName).NotEmpty().NotNull().WithMessage("first name is not valid");

        RuleFor(i => i.lastName).NotEmpty().NotNull().WithMessage("last name is not valid");

        RuleFor(i => i.nationalCode).Must(ValidNationalCode).WithMessage("invalid natioanlCode");

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

    private bool ValidNationalCode(string nationalCode)
    {
        try
        {
            switch (nationalCode)
            {
                case "0000000000":
                case "1111111111":
                case "22222222222":
                case "33333333333":
                case "4444444444":
                case "5555555555":
                case "6666666666":
                case "7777777777":
                case "8888888888":
                case "9999999999":
                    return false;
            }

            char[] chArray = nationalCode.ToCharArray();
            int[] numArray = new int[chArray.Length];
            for (int i = 0; i < chArray.Length; i++)
            {
                numArray[i] = (int)char.GetNumericValue(chArray[i]);
            }
            int num2 = numArray[9];

            int num3 = ((((((((numArray[0] * 10) + (numArray[1] * 9)) + (numArray[2] * 8)) + (numArray[3] * 7)) + (numArray[4] * 6)) + (numArray[5] * 5)) + (numArray[6] * 4)) + (numArray[7] * 3)) + (numArray[8] * 2);
            int num4 = num3 - ((num3 / 11) * 11);
            if ((((num4 == 0) && (num2 == num4)) || ((num4 == 1) && (num2 == 1))) || ((num4 > 1) && (num2 == Math.Abs((int)(num4 - 11)))))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }
}
