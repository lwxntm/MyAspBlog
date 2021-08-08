using MyAspBlog.IRepository;
using MyAspBlog.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAspBlog.Repository
{
    public class BlogNewsRepository : BaseRepository<BlogNews>, IBlogNewsRepository
    {
        public override async Task<List<BlogNews>> QueryAsync()
        {
            return await this.Context.Queryable<BlogNews>()
                .Mapper(c => c.TypeInfo, c => c.TypeId, c => c.TypeInfo.Id)
                .Mapper(c => c.AuthorInfo, c => c.AuthorId, c => c.AuthorInfo.Id)
                .ToListAsync();
        }
    }
}
