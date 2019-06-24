using System;
using AutoMapper;
using System.Collections.Generic;
using System.Text;
using Api.Database.Entity;
using Api.DTO;

namespace Api.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, Comment>();
            CreateMap<Meme, MemeDto>();
            CreateMap<MemeDto, Meme>();
        }
    }
}
