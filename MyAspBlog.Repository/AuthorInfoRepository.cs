using MyAspBlog.IRepository;
using MyAspBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAspBlog.Repository
{
    public class AuthorInfoRepository:BaseRepository<AuthorInfo>, IAuthorInfoRepository
    {
    }
}
