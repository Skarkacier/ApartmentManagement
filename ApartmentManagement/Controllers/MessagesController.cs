using Business.Services;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] Message message)
        {
            try
            {
                var messageId = await _messageService.CreateMessage(message.MessageText, message.SenderId, message.ReceiverId, message.IsRead, message.MessageDate);
                return Ok(messageId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMessages()
        {
            try
            {
                var messages = await _messageService.GetAllMessages();
                return Ok(messages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessage(int id, [FromBody] Message message)
        {
            try
            {
                await _messageService.UpdateMessage(id, message.MessageText, message.SenderId, message.ReceiverId, message.IsRead, message.MessageDate);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            try
            {
                await _messageService.DeleteMessage(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
