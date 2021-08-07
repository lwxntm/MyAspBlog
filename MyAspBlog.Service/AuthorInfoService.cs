using MyAspBlog.IRepository;
using MyAspBlog.IService;
using MyAspBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAspBlog.Service
{
    public class AuthorInfoService :BaseService<AuthorInfo> , IAuthorInfoService
    {
        private readonly IAuthorInfoRepository _iAuthorInfoRepository;
        public AuthorInfoService(IAuthorInfoRepository iAuthorInfoRepository)
        {
            base._iBaseRepository = iAuthorInfoRepository;
            _iAuthorInfoRepository = iAuthorInfoRepository;
        }
    }
}
