using AutoMapper;
using Domain.Entities;

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
