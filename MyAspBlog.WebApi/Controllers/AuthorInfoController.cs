using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAspBlog.IService;
using MyAspBlog.Model.DTO;
using MyAspBlog.WebApi.Utility._MD5;
using MyAspBlog.WebApi.Utility.ApiResult;
using System;
using System.Threading.Tasks;

namespace MyAspBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorInfoController : ControllerBase
    {
        private readonly IAuthorInfoService _iAuthorInfoService;
        public AuthorInfoController(IAuthorInfoService iAuthorInfoService)
        {
            _iAuthorInfoService = iAuthorInfoService;
        }
        [HttpPost("Create")]
        public async Task<ApiResult> Create(string name, string username, string passwd)
        {
            if (await _iAuthorInfoService.FindAsync(u => u.UserName == username) != null)
                return ApiResultHelper.Error("用户名已存在");
            var authorInfo = new Model.AuthorInfo
            {
                Name = name,
                UserName = username,
                UserPassWd = MD5Helper.MD5Encrypt32(passwd)
            };
            if (!await _iAuthorInfoService.CreateAsync(authorInfo))
                return ApiResultHelper.Error("创建失败，服务器错误");
            return ApiResultHelper.Success(authorInfo);
        }
        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(string name)
        {
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            var authorInfo = await _iAuthorInfoService.FindAsync(id);
            authorInfo.Name = name;
            if (!await _iAuthorInfoService.EditAsync(authorInfo))
                return ApiResultHelper.Error("修改失败，服务器错误");
            return ApiResultHelper.Success(authorInfo);
        }
        [HttpGet("Get")]
        public async Task<ApiResult> Get([FromServices] IMapper imapper, int id)
        {
            var authorInfo = await _iAuthorInfoService.FindAsync(id);
            if (authorInfo == null) return ApiResultHelper.Error("找不到该用户");
            var authorDTO = imapper.Map<AuthorInfoDTO>(authorInfo);
            return ApiResultHelper.Success(authorDTO);
        }
    }
}
