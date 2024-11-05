using DormitoryServer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataDormitoryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DormitoryContext")));



builder.Services.AddScoped<RoleService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var roleService = builder.Services.BuildServiceProvider().GetRequiredService<RoleService>();
var roles = await roleService.GetRolesFromDatabaseAsync();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Student", policy => policy.RequireClaim("Roles", "Student"));
    options.AddPolicy("ManagerOrStudent", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim("Roles", "Manager") || context.User.HasClaim("Roles", "Student")));
    foreach (var role in roles)
    {
        options.AddPolicy(role, policy => policy.RequireClaim("Roles", role));
    }
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//using (var scope = app.Services.CreateScope())
//{
//    var roleService = scope.ServiceProvider.GetRequiredService<RoleService>();
//    var roles = await roleService.GetRolesFromDatabaseAsync();

//    var authorizationOptions = app.Services.GetRequiredService<IOptions<AuthorizationOptions>>().Value;
//    foreach (var role in roles)
//    {
//        authorizationOptions.AddPolicy(role, policy => policy.RequireRole(role));
//    }
//}


app.MapControllers();

app.Run();

public class RoleService
{
    private readonly DataDormitoryContext _context;

    public RoleService(DataDormitoryContext context)
    {
        _context = context;
    }

    public async Task<List<string>> GetRolesFromDatabaseAsync()
    {
        return await _context.Roles.Select(r => r.RoleName).ToListAsync();
    }
}

