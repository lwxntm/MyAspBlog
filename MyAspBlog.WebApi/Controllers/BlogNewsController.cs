using Microsoft.AspNetCore.Mvc;
using MyAspBlog.IService;
using MyAspBlog.Model;
using MyAspBlog.WebApi.Utility.ApiResult;
using System.Threading.Tasks;

namespace MyAspBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogNewsController : ControllerBase
    {
        private readonly IBlogNewsService _iBlogNewsService;
        public BlogNewsController(IBlogNewsService iBlogNewsService)
        {
            this._iBlogNewsService = iBlogNewsService;
        }
        [HttpGet("BlogNews")]
        public async Task<ActionResult<ApiResult>> GetBlogNews()
        {
            var data = await _iBlogNewsService.QueryAsync();
            if (data == null) return ApiResultHelper.Error("没有更多的文章");
            return ApiResultHelper.Success(data);
        }
        /// <summary>
        /// 添加文章
        /// </summary>
        [HttpPost("Create")]
        public async Task<ActionResult<ApiResult>> Create(string title, string content, int typeId)
        {
            //数据验证。。。省略

            BlogNews blogNews = new BlogNews
            {
                BrowseCount = 0,
                Title = title,
                Content = content,
                LastModifiedTime = System.DateTime.Now,
                LikeCount = 0,
                TypeId = typeId,
                AuthorId = 1,
            };
            bool b = await _iBlogNewsService.CreateAsync(blogNews);
            if (!b) return ApiResultHelper.Error("创建失败，服务器发生错误");
            return ApiResultHelper.Success(blogNews);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<ApiResult>> Delete(int id)
        {
            bool b = await _iBlogNewsService.DeleteAsync(id);
            if (!b) return ApiResultHelper.Error("删除，服务器发生错误");
            return ApiResultHelper.Success(b);
        }
        [HttpPut("Edit")]
        public async Task<ActionResult<ApiResult>> Edit(int id, string title, string content, int typeid)
        {
            BlogNews blogNews =await _iBlogNewsService.FindAsync(id);
            if (blogNews == null) return ApiResultHelper.Error("未找到该文章");
            blogNews.Title = title;
            blogNews.Content = content;
            blogNews.TypeId = typeid;
            bool b =await _iBlogNewsService.EditAsync(blogNews);
            if (!b) return ApiResultHelper.Error("修改失败，服务器错误");
            return ApiResultHelper.Success(blogNews);
        }
    }
}

