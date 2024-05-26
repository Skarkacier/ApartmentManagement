using DataAccess.Context;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Business.Services
{
    public interface IUserService
    {
        Task<int> CreateUser(string fullName, string identityNum, string email, string password, string phoneNumber, string carPlate, bool isAdmin);
        Task<List<User>> GetAllUsers();
        Task UpdateUser(int userId, string fullName, string identityNum, string email, string password, string phoneNumber, string carPlate, bool isAdmin);
        Task DeleteUser(int userId);
    }
    public class UserService : IUserService
    {
        private readonly ManagementContext _context;

        public UserService(ManagementContext context)
        {
            _context = context;
        }
        public async Task<int> CreateUser(string fullName, string identityNum, string email, string password, string phoneNumber, string carPlate, bool isAdmin)
        {
            try
            {
                var userIdParam = new Microsoft.Data.SqlClient.SqlParameter("@UserID", System.Data.SqlDbType.Int);
                userIdParam.Direction = System.Data.ParameterDirection.Output;

                await _context.Database.ExecuteSqlRawAsync("EXEC [dbo].[User_Create] @UserID OUT, @FullName, @IdentityNum, @Email, @Password, @PhoneNumber, @CarPlate, @IsAdmin",
                    userIdParam,
                    new Microsoft.Data.SqlClient.SqlParameter("@FullName", fullName),
                    new Microsoft.Data.SqlClient.SqlParameter("@IdentityNum", identityNum),
                    new Microsoft.Data.SqlClient.SqlParameter("@Email", email),
                    new Microsoft.Data.SqlClient.SqlParameter("@Password", password),
                    new Microsoft.Data.SqlClient.SqlParameter("@PhoneNumber", phoneNumber),
                    new Microsoft.Data.SqlClient.SqlParameter("@CarPlate", carPlate),
                    new Microsoft.Data.SqlClient.SqlParameter("@IsAdmin", isAdmin));

                return (int)userIdParam.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                return await _context.Users.FromSqlRaw("EXEC [dbo].[User_ReadAll]").ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateUser(int userId, string fullName, string identityNum, string email, string password, string phoneNumber, string carPlate, bool isAdmin)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC [dbo].[User_Update] {userId}, {fullName}, {identityNum}, {email}, {password}, {phoneNumber}, {carPlate}, {isAdmin}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task DeleteUser(int userId)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC [dbo].[User_Delete] {userId}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
