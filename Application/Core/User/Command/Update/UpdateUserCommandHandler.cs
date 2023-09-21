using Application.Common;
using Application.Common.Exceptions;
using Application.Core.User.Command.Create;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Core.User.Command.Update;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly ITestContext _context;
    private readonly IValidator<UpdateUserCommand> _validator;
    public UpdateUserCommandHandler(ITestContext context, IValidator<UpdateUserCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var validation = _validator.Validate(request);
        if (!validation.IsValid)
            throw new BadRequestException(validation.Errors.First().ErrorMessage);

        var user = await _context.Users.FirstOrDefaultAsync(i => i.Id == request.id, cancellationToken);
        if (user is null)
            throw new NotFoundException("invalid id");

        user.UpdateInfo(request.name, request.family, request.phoneNumber);

        _context.Users.Update(user);

        await _context.SaveAsync(cancellationToken);

        return new UserDto(user.FirstName, user.LastName);
    }
}
