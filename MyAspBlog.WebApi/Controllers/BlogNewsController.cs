using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAspBlog.IService;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult> GetBlogNews()
        {
            var data =await _iBlogNewsService.QueryAsync();
            return Ok(data);
        }
    }
}
