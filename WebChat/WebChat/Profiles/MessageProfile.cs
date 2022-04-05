using AutoMapper;
using WebChat.Entities;
using WebChat.Models;

namespace WebChat.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageToSendDTO>();
        }
    }
}
