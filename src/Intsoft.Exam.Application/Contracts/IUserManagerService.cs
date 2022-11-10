using Intsoft.Exam.Application.Models;

namespace Intsoft.Exam.Application.Contracts
{
    public interface IUserManagerService
    {
        Task<UserModel> GetUserAsync(int id,CancellationToken cancellationToken);
        Task CreateUserAsync(CreateUserModel model, CancellationToken cancellationToken);
        Task UpdateUserAsync(UpdateUserModel model, CancellationToken cancellationToken);
    }
}
