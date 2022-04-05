using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebChat.Entities;
using WebChat.Models;
using WebChat.Services.Messages;

namespace WebChat.Controllers
{
    [ApiController]
    [Route("/api/chat")]
    public class Chat : ControllerBase
    {
        private readonly IMessagesRepository _messagesRepository;
        private readonly IMapper _mapper;

        public Chat(IMessagesRepository messagesRepository, IMapper mapper)
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
        public ActionResult<MessageToSendDTO> GetFirstMessages()
        {
            var messages = _messagesRepository.GetFirstMessages();
            return Ok(_mapper.Map<IEnumerable<MessageToSendDTO>>(messages));
        }
    }
}
