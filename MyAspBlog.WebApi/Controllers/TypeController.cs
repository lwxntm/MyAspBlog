using Microsoft.AspNetCore.Mvc;
using MyAspBlog.IService;
using MyAspBlog.Model;
using MyAspBlog.WebApi.Utility.ApiResult;
using System.Threading.Tasks;

namespace MyAspBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ITypeInfoService _iTypeInfoService;
        public TypeController(ITypeInfoService iTypeInfoService)
        {
            _iTypeInfoService = iTypeInfoService;
        }
        [HttpGet("Types")]
        public async Task<ApiResult> Types()
        {
            var types = await _iTypeInfoService.QueryAsync();
            if (types.Count == 0) return ApiResultHelper.Error("找不到更多类型");
            return ApiResultHelper.Success(types);
        }

        [HttpPost("Create")]
        public async Task<ApiResult> Create(string name)
        {
            #region 数据验证
            if (string.IsNullOrWhiteSpace(name)) return ApiResultHelper.Error("类型名不能为空");
            #endregion
            TypeInfo type = new TypeInfo
            {
                Type = name
            };
            bool b = await _iTypeInfoService.CreateAsync(type);
            if (!b) return ApiResultHelper.Error("创建类型失败");
            return ApiResultHelper.Success(b);
        }
        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(int id, string name)
        {
            var type = await _iTypeInfoService.FindAsync(id);
            type.Type = name;
            bool b = await _iTypeInfoService.EditAsync(type);
            if (!b) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success(b);
        }
        [HttpDelete("Delete")]
        public async Task<ApiResult> Delete(int id)
        {
            bool b = await _iTypeInfoService.DeleteAsync(id);
            if (!b) return ApiResultHelper.Error("删除失败");
            return ApiResultHelper.Success(b);
        }
    }
}
