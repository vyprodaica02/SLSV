using Design.DTO;
using Design.Entity;
using Infrastructure.DataEx;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace projectQLSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        private readonly AppDbContext dbContext;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
            dbContext = new AppDbContext();
        }

        [HttpPost("DangNhap")]
        public IActionResult DangNhap([FromBody] LoginModal loginModal)
        {
            var sinhvien = dbContext.sinhViens.FirstOrDefault(x => x.Email == loginModal.Email);

            if (sinhvien == null)
            {
                return BadRequest("Email khong ton tai!");
            }
            else
            {
                if (sinhvien.Password == loginModal.password)
                {
                    string token = GenerateTocken(sinhvien);
                    // Tạo một cookie mới với tên "jwtToken" để lưu trữ token
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true, // Cho phép cookie chỉ được truy cập qua HTTP (không thông qua JavaScript)
                        Expires = DateTime.Now.AddDays(1), // Đặt thời gian hết hạn của cookie
                    };
                    // Thêm token vào cookie
                    Response.Cookies.Append("jwtToken", token, cookieOptions);
                    return Ok(token);
                }
                else
                {
                    return BadRequest("Mat khau khong dung!");
                }
            }
        }

        //creat tokken
        private string GenerateTocken(SinhVien sinhVien)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("phatuid", sinhVien.Id.ToString()),
                new Claim(ClaimTypes.Role ,sinhVien.roll),//tạo ra roll admin
            };
            var sercuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(sercuritykey, SecurityAlgorithms.HmacSha256);
            var tocken = new JwtSecurityToken(_configuration["JwtSettings:Issuer"], _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(tocken);
        }
    }
}
