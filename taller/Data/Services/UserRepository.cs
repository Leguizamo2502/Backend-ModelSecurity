using Data.Interfaces.IDataImplement;
using Data.Repositoy;
using Entity.Domain.Models.Implements;
using Entity.DTOs.Default;
using Entity.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Utilities.Custom;

namespace Data.Services
{
    public class UserRepository: DataGeneric<User>, IUserRepository
    {

        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }



        public async Task<User?> FindEmail(string email)
        {
            var user = await _dbSet.Where(u => u.Email == email).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> ValidateUserAsync(LoginDto loginDto)
        {
            bool suceeded = false;

            var user = await _dbSet
                .FirstOrDefaultAsync(u =>
                            u.Email == loginDto.Email &&
                            u.Password == (loginDto.Password));

            suceeded = (user != null) ? true : throw new UnauthorizedAccessException("Credenciales inválidas");

            return user;
        }
    }
}
