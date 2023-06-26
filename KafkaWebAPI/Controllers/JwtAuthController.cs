using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using HISDB.Models;
using HISDB.Data;
using Microsoft.EntityFrameworkCore;

namespace KafkaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtAuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<JwtAuthController> _logger;
        private readonly HisdbContext _context;

        public JwtAuthController(IConfiguration config, ILogger<JwtAuthController> logger, HisdbContext context)
        {
            _config = config;
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        [AllowAnonymous]
        //
        public IActionResult LoginGetJwt([FromBody] UserDto userDto)
        {
            _logger.LogWarning(2001, DateTime.Now.ToLongTimeString() + $", JwtAuth控制器POST方法被呼叫, 收到的資料:{JsonConvert.SerializeObject(userDto)}");

            IActionResult response = Unauthorized();

            //驗證使用者帳號是否確
            var user = AuthenticateUser(userDto);

            if (user != null)
            {
                string tokenString = GenerateJsonWebToken(user);
                response = Ok(new { token = tokenString });

                _logger.LogWarning(2001, DateTime.Now.ToLongTimeString() + $", JWT Token核發成功 : {tokenString}");
            }

            return response;
        }

        //這個方法是用HardCode驗證使用者帳號與密碼
        //正式環境中應改為DB資料庫驗證, 撰寫資料存取程式
        private UserDto? AuthenticateUser(UserDto userDto)
        {
            //var user = _context.Employees.FirstOrDefaultAsync(x =>
            //               x.Account.ToUpper() == userDto.Username.ToUpper()
            //               && x.Password == userDto.Password);

            var user = _context.Employees.Where(x =>
                                                x.Account.ToUpper() == userDto.Username.ToUpper()
                                                && x.Password == userDto.Password).FirstOrDefault();

            if(user == null)
            {
               return null;
            }

            ////驗證帳密, 請勿使用ToUpper()或ToLower()方法, 請維持原狀
            //if (userDto.Username == "Kevin" && userDto.Password == "12345")
            //{
            //    userDto = new UserDto { Username = "凱文", EmailAddress = "kevin@gmail.com" };
            //}
            return userDto;
        }

        private string GenerateJsonWebToken(UserDto userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

