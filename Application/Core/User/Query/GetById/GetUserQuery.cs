using Application.Core.User.Command.Create;
using MediatR;

namespace Application.Core.User.Query.GetById;
public record GetUserQuery(int id) : IRequest<UserDto>;
