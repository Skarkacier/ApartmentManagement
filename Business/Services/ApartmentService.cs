using DataAccess.Context;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IApartmentService
    {
        Task<int> CreateApartment(string block, string floor, string number, bool status, int userId);
        Task DeleteApartment(int apartmentId);
        Task<List<Apartment>> GetAllApartments();
        Task UpdateApartment(int apartmentId, string block, string floor, string number, bool status, int userId);
    }

    public class ApartmentService : IApartmentService
    {
        private readonly ManagementContext _context;

        public ApartmentService(ManagementContext context)
        {
            _context = context;
        }

        public async Task<int> CreateApartment(string block, string floor, string number, bool status, int userId)
        {
            try
            {
                var apartmentIdParam = new Microsoft.Data.SqlClient.SqlParameter("@ApartmentID", System.Data.SqlDbType.Int);
                apartmentIdParam.Direction = System.Data.ParameterDirection.Output;

                await _context.Database.ExecuteSqlRawAsync("EXEC [dbo].[Apartment_Create] @ApartmentID OUT, @Block, @Floor, @Number, @Status, @UserID",
                    apartmentIdParam,
                    new Microsoft.Data.SqlClient.SqlParameter("@Block", block),
                    new Microsoft.Data.SqlClient.SqlParameter("@Floor", floor),
                    new Microsoft.Data.SqlClient.SqlParameter("@Number", number),
                    new Microsoft.Data.SqlClient.SqlParameter("@Status", status),
                    new Microsoft.Data.SqlClient.SqlParameter("@UserID", userId));

                return (int)apartmentIdParam.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Apartment>> GetAllApartments()
        {
            try
            {
                return await _context.Apartments.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateApartment(int apartmentId, string block, string floor, string number, bool status, int userId)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC [dbo].[Apartment_Update] {apartmentId}, {block}, {floor}, {number}, {status}, {userId}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteApartment(int apartmentId)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC [dbo].[Apartment_Delete] {apartmentId}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
