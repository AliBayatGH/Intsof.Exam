using Application.Common;
using Application.Common.Exceptions;
using FluentValidation;
using MediatR;

namespace Application.Core.User.Command.Create;

public class AddUSerCommandHandler : IRequestHandler<AddUserCommand, UserDto>
{
    private readonly ITestContext _context;
    private readonly IValidator<AddUserCommand> _validator;

    public AddUSerCommandHandler(ITestContext context, IValidator<AddUserCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<UserDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var validation = _validator.Validate(request);

        if (!validation.IsValid)
            throw new BadRequestException(validation.Errors.First().ErrorMessage);

        var user = new Domain.User
        {
            FirstName = request.firstName,
            LastName = request.lastName,
            NationalCode = request.nationalCode,
            PhoneNumber = request.phoneNumber
        };

        await _context.Users.AddAsync(user, cancellationToken);

        await _context.SaveAsync(cancellationToken);

        return new UserDto(user.FirstName, user.LastName);
    }
}
