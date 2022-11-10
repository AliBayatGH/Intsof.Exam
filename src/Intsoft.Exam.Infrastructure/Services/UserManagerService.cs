using AutoMapper;
using Intsoft.Exam.Application.Contracts;
using Intsoft.Exam.Application.Models;
using Intsoft.Exam.Domain.Entities;
using Intsoft.Exam.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Intsoft.Exam.Infrastructure.Services
{
    public class UserManagerService : IUserManagerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserManagerService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserModel> GetUserAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new Exception("User Not Exist");
            }

            var userModel = _mapper.Map<UserModel>(user);

            return userModel;
        }

        public async Task CreateUserAsync(CreateUserModel model, CancellationToken cancellationToken)
        {
            var isExistNationalCode = await _context.Users.AnyAsync(a => a.NationalCode == model.NationalCode, cancellationToken: cancellationToken);

            if (isExistNationalCode)
            {
                throw new Exception("NationalCode is Duplicate!!");
            }

            var userForAdd = _mapper.Map<User>(model);

           await _context.Users.AddAsync(userForAdd, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateUserAsync(UpdateUserModel model, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(model.Id);

            if (user == null)
            {
                throw new Exception("");
            }

            var isExistNationalCode = await _context.Users.AnyAsync(predicate: a => a.NationalCode == model.NationalCode && a.Id != user.Id, cancellationToken: cancellationToken);

            if (isExistNationalCode)
            {
                throw new Exception("NationalCode is Duplicate!!");
            }

            _mapper.Map(model, user, typeof(UpdateUserModel), typeof(Domain.Entities.User));

            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
