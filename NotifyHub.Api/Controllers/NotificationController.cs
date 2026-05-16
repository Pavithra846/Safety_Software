using Microsoft.AspNetCore.Mvc;
using NotifyHub.Api.Source.Application.Interface;

namespace NotifyHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;

        public NotificationController(INotificationService service)
        {
            _service = service;
        }

        [HttpGet("unread/{userId}")]
        public async Task<IActionResult> GetUnread(Guid userId)
        {
            var result = await _service.GetUnread(userId);

            return Ok(result);
        }

        [HttpPut("mark-read/{notifyId}")]
        public async Task<IActionResult> MarkAsRead(Guid notifyId)
        {
            await _service.MarkAsRead(notifyId);

            return Ok();
        }
    }
}
