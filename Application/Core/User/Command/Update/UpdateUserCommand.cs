using Application.Core.User.Command.Create;
using MediatR;

namespace Application.Core.User.Command.Update;
public record UpdateUserCommand(int id,
    string name,
    string family,
    string phoneNumber) : IRequest<UserDto>;
