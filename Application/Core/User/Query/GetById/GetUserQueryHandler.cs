using Application.Common;
using Application.Common.Exceptions;
using Application.Core.User.Command.Create;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Core.User.Query.GetById;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly ITestContext _context;

    public GetUserQueryHandler(ITestContext context)
    {
        _context = context;
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(i => i.Id == request.id, cancellationToken);

        if (user == null)
            throw new NotFoundException("user notfound");

        return new UserDto(user.FirstName, user.LastName);
    }
}
