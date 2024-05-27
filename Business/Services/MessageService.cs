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
    public interface IMessageService
    {
        Task<int> CreateMessage(string messageText, int senderId, int receiverId, bool isRead, DateTime messageDate);
        Task<List<Message>> GetAllMessages();
        Task UpdateMessage(int messageId, string messageText, int senderId, int receiverId, bool isRead, DateTime messageDate);
        Task DeleteMessage(int messageId);
    }
    public class MessageService : IMessageService
    {
        private readonly ManagementContext _context;

        public MessageService(ManagementContext context)
        {
            _context = context;
        }
        public async Task<int> CreateMessage(string messageText, int senderId, int receiverId, bool isRead, DateTime messageDate)
        {
            try
            {
                var messageIdParam = new Microsoft.Data.SqlClient.SqlParameter("@MessageID", System.Data.SqlDbType.Int);
                messageIdParam.Direction = System.Data.ParameterDirection.Output;

                await _context.Database.ExecuteSqlRawAsync("EXEC [dbo].[Message_Create] @MessageID OUT, @MessageText, @SenderId, @ReceiverId, @IsRead, @MessageDate",
                    messageIdParam,
                    new Microsoft.Data.SqlClient.SqlParameter("@MessageText", messageText),
                    new Microsoft.Data.SqlClient.SqlParameter("@SenderId", senderId),
                    new Microsoft.Data.SqlClient.SqlParameter("@ReceiverId", receiverId),
                    new Microsoft.Data.SqlClient.SqlParameter("@IsRead", isRead),
                    new Microsoft.Data.SqlClient.SqlParameter("@MessageDate", messageDate));

                return (int)messageIdParam.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Message>> GetAllMessages()
        {
            try
            {
                return await _context.Messages.FromSqlRaw("EXEC [dbo].[Message_ReadAll]").ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateMessage(int messageId, string messageText, int senderId, int receiverId, bool isRead, DateTime messageDate)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC [dbo].[Message_Update] {messageId}, {messageText}, {senderId}, {receiverId}, {isRead}, {messageDate}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task DeleteMessage(int messageId)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC [dbo].[Message_Delete] {messageId}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
