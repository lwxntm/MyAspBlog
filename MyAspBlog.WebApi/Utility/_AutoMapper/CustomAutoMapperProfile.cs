using AutoMapper;
using MyAspBlog.Model;
using MyAspBlog.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspBlog.WebApi.Utility._AutoMapper
{
    public class CustomAutoMapperProfile:Profile
    {
        public CustomAutoMapperProfile()
        {
            base.CreateMap<AuthorInfo, AuthorInfoDTO>();
        }
    }
}
