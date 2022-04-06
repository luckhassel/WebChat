using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebChat.Entities;
using WebChat.Models;
using WebChat.Services.Messages;

namespace WebChat.Controllers
{
    [ApiController]
    [Route("/api/chat/message")]
    public class ChatController : ControllerBase
    {
        private readonly IMessagesRepository _messagesRepository;
        private readonly IMapper _mapper;

        public ChatController(IMessagesRepository messagesRepository, IMapper mapper)
        {
            _messagesRepository = messagesRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddMessage([FromBody] Message message)
        {
            _messagesRepository.AddMessage(message);
            _messagesRepository.Save();
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<MessageToSendDTO>> GetFirstMessages(
            [FromQuery(Name = "room")] string room, [FromQuery(Name = "amount")] int amount = 50)
        {
            var messages = await _messagesRepository.GetFirstMessages(room, amount);
            return Ok(_mapper.Map<IEnumerable<MessageToSendDTO>>(messages));
        }
    }
}
