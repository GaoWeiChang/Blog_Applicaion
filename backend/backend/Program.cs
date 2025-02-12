using backend.Data;
using backend.Repositories.Implementation;
using backend.Repositories.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor(); // http context accessor

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogAppConnectionString"));
});

builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogAppConnectionString"));
});

// เพิ่มระบบ Identity เพื่อจัดการผู้ใช้
// เพิ่ม Role และ Token Provider สำหรับการยืนยันตัวตน
builder.Services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("CodePulse")
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

// การตั้งค่ารหัสผ่าน
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

// ตั้งค่า JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        AuthenticationType = "Jwt",
                        ValidateIssuer = true, // ตรวจสอบผู้ออก token
                        ValidateAudience = true, // ตรวจสอบผู้รับ token
                        ValidateLifetime = true, // ตรวจสอบอายุ token
                        ValidateIssuerSigningKey = true, // ตรวจสอบ key ที่ใช้เซ็น token

                        // ค่าที่กำหนดใน appsettings.json
                        ValidIssuer = builder.Configuration["Jwt:Issuer"], // ["Jwt:Issuer"] from appsettings.json
                        ValidAudience = builder.Configuration["Jwt:Audience"],

                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // คีย์ลับที่ใช้เข้ารหัส token
                    };
                });


// need to write this after created implementation and interface
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>(); // add dependency injection and create new instance in every HTTP request
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.AllowAnyHeader()
           .WithOrigins("http://localhost:4200")  // Only allow requests from port 4200
           .AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")), // indicated "Images" folder
    RequestPath = "/Images"
});

app.MapControllers();

app.Run();
