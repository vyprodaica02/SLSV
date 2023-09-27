using Design.Entity;
using Infrastructure.DataEx;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using projectQLSV;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer("Server=localhost;Integrated Security=true;Initial Catalog=QLSINHVIENSS;MultipleActiveResultSets=True;encrypt=true;trustservercertificate=true");
});
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(SinhVienServices<>));
builder.Services.AddScoped(typeof(MonHocServices<>));
builder.Services.AddScoped(typeof(LopServices<>));
builder.Services.AddScoped(typeof(KhoaMonServices<>));
builder.Services.AddScoped(typeof(KhoaServices<>));
builder.Services.AddScoped(typeof(KetQuaServices<>));

// Cấu hình xác thực JWT (JSON Web Token)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),
    };
});

// Cấu hình phân quyền (Authorization)

// Đăng ký dịch vụ HttpContextAccessor để truy cập thông tin về HTTP context trong các dịch vụ và middleware.
builder.Services.AddHttpContextAccessor();

// Cấu hình chính sách CORS cho phép bất kỳ origin nào có thể gửi yêu cầu có credentials (cookies, token) đến API.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials()
               .SetIsOriginAllowed(_ => true);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Sử dụng routing
app.UseRouting();

// Sử dụng chính sách CORS
app.UseCors();

// Sử dụng HTTPS redirection trước
app.UseHttpsRedirection();

// Sử dụng middleware xác thực trước
app.UseAuthentication();

// Sử dụng middleware phân quyền sau middleware xác thực
app.UseAuthorization();

// Sử dụng các controllers
app.MapControllers();

// Cuối cùng, sử dụng middleware Routing
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
