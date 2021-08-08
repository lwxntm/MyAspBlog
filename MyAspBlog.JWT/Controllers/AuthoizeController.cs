using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyAspBlog.IService;
using MyAspBlog.JWT.Utility._MD5;
using MyAspBlog.JWT.Utility.ApiResult;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyAspBlog.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthoizeController : ControllerBase
    {
        private readonly IAuthorInfoService _iAuthorInfoService;
        public AuthoizeController(IAuthorInfoService iAuthorInfoService)
        {
            _iAuthorInfoService = iAuthorInfoService;
        }
        [HttpPost("Login")]
        public async Task<ApiResult> Login(string username, string passwd)
        {
            var author = await _iAuthorInfoService.FindAsync(
                u => u.UserName == username && u.UserPassWd == MD5Helper.MD5Encrypt32(passwd));
            if (author == null)
                return ApiResultHelper.Error("用户名或者密码错误");

            //copy来的代码
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, author.Name),
                new Claim("Id", author.Id.ToString()),
                new Claim("UserName", author.UserName ),
                //不能放敏感信息，里面的内容会被解密
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-VF"));
            //issuer代表颁发Token的Web应用程序，audience是Token的受理者
            var token = new JwtSecurityToken(
                issuer: "http://localhost:6060",
                audience: "http://localhost:5000",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return ApiResultHelper.Success(jwtToken);

        }
    }
}
