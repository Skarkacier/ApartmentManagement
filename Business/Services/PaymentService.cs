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
    public interface IPaymentService
    {
        Task<int> CreatePayment(string type, decimal amount, int apartmentId, int userId, bool isPayed, DateTime paymentDate);
        Task<List<Payment>> GetAllPayments();
        Task UpdatePayment(int paymentId, string type, decimal amount, int apartmentId, int userId, bool isPayed, DateTime paymentDate);
        Task DeletePayment(int paymentId);
    }
    public class PaymentService : IPaymentService
    {
        private readonly ManagementContext _context;

        public PaymentService(ManagementContext context)
        {
            _context = context;
        }
        public async Task<int> CreatePayment(string type, decimal amount, int apartmentId, int userId, bool isPayed, DateTime paymentDate)
        {
            try
            {
                var paymentIdParam = new Microsoft.Data.SqlClient.SqlParameter("@PaymentID", System.Data.SqlDbType.Int);
                paymentIdParam.Direction = System.Data.ParameterDirection.Output;

                await _context.Database.ExecuteSqlRawAsync("EXEC [dbo].[Payment_Create] @PaymentID OUT, @Type, @Amount, @ApartmentId, @UserId, @IsPayed, @PaymentDate",
                    paymentIdParam,
                    new Microsoft.Data.SqlClient.SqlParameter("@Type", type),
                    new Microsoft.Data.SqlClient.SqlParameter("@Amount", amount),
                    new Microsoft.Data.SqlClient.SqlParameter("@ApartmentId", apartmentId),
                    new Microsoft.Data.SqlClient.SqlParameter("@UserId", userId),
                    new Microsoft.Data.SqlClient.SqlParameter("@IsPayed", isPayed),
                    new Microsoft.Data.SqlClient.SqlParameter("@PaymentDate", paymentDate));

                return (int)paymentIdParam.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Payment>> GetAllPayments()
        {
            try
            {
                return await _context.Payments.FromSqlRaw("EXEC [dbo].[Payment_ReadAll]").ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdatePayment(int paymentId, string type, decimal amount, int apartmentId, int userId, bool isPayed, DateTime paymentDate)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC [dbo].[Payment_Update] {paymentId}, {type}, {amount}, {apartmentId}, {userId}, {isPayed}, {paymentDate}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task DeletePayment(int paymentId)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC [dbo].[Payment_Delete] {paymentId}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
