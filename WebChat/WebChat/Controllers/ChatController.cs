using AutoMapper;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebChat.Controllers
{
    [ApiController]
    [Route("/api/chat/message")]
    public class ChatController : ControllerBase
    {
        private readonly IMessagesRepositoryService _messagesRepository;
        private readonly IMapper _mapper;

        public ChatController(IMessagesRepositoryService messagesRepository, IMapper mapper)
        {
            _messagesRepository = messagesRepository ?? throw new ArgumentNullException(nameof(messagesRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddMessage([FromBody] Message message)
        {
            if (_messagesRepository.AddMessage(message))
            {
                _messagesRepository.Save();
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<MessageToSendDTO>> GetFirstMessages(
            [FromQuery(Name = "room")] string room, [FromQuery(Name = "amount")] int amount = 50)
        {
            var messages = await _messagesRepository.GetFirstMessages(room, amount);
            if (messages != null)
            {
                return Ok(_mapper.Map<IEnumerable<MessageToSendDTO>>(messages));
            }
            return BadRequest();
        }
    }
}
