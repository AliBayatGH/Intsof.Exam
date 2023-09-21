using MediatR;

namespace Application.Core.User.Command.Create;
public record AddUserCommand(string firstName,
    string lastName,
    string nationalCode,
    string phoneNumber) : IRequest<UserDto>;
