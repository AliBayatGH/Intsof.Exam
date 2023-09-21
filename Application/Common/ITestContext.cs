using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Common;
public interface ITestContext
{
    DbSet<User> Users { get; set; }

    Task SaveAsync(CancellationToken ct);
}
