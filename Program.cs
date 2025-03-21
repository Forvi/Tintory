using COLOR.Data;
using COLOR.Data.Repositories;
using COLOR.Data.Repositories.Interfaces;
using COLOR.Services;
using COLOR.Services.Interfaces;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using COLOR.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

void ConfigureServices(IServiceCollection services)
{
    services.AddCors(); 
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
    
builder.Services.AddApiAuthentication(
    builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>()
);

builder.Services.AddScoped<IPaletteRepository, PaletteRepository>();
builder.Services.AddScoped<IColorRepository, ColorRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPaletteService, PaletteService>();
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Connect"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseRouting();
app.UseDeveloperExceptionPage();

app.UseAuthentication();
app.UseAuthorization();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();