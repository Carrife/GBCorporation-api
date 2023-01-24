using GB_Corporation.Data;
using GB_Corporation.Helpers;
using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Interfaces.Services;
using GB_Corporation.Models;
using GB_Corporation.Repositories;
using GB_Corporation.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql("Name=DefaultConnection");
});

builder.Services.AddCors();

using(AppDbContext context = new AppDbContext())
{
    context.Database.Migrate();

    if (!context.SuperDictionaries.Any())
    {
        context.SuperDictionaries.Add(new SuperDictionary { Id = 1, Name = "C#", DictionaryId = 1 });
        context.SuperDictionaries.Add(new SuperDictionary { Id = 2, Name = "C++", DictionaryId = 1 });
        context.SuperDictionaries.Add(new SuperDictionary { Id = 3, Name = "Java", DictionaryId = 1 });
        context.SuperDictionaries.Add(new SuperDictionary { Id = 4, Name = "Java Script", DictionaryId = 1 });

        context.SuperDictionaries.Add(new SuperDictionary { Id = 5, Name = "SDD1", DictionaryId = 2 });
        context.SuperDictionaries.Add(new SuperDictionary { Id = 6, Name = "SDD2", DictionaryId = 2 });
        context.SuperDictionaries.Add(new SuperDictionary { Id = 7, Name = "SDD3", DictionaryId = 2 });

        context.SuperDictionaries.Add(new SuperDictionary { Id = 8, Name = "English", DictionaryId = 3 });
        context.SuperDictionaries.Add(new SuperDictionary { Id = 9, Name = "French", DictionaryId = 3 });
        context.SuperDictionaries.Add(new SuperDictionary { Id = 10, Name = "German", DictionaryId = 3 });

        context.SuperDictionaries.Add(new SuperDictionary { Id = 11, Name = "Active", DictionaryId = 4 });
        context.SuperDictionaries.Add(new SuperDictionary { Id = 12, Name = "Fired", DictionaryId = 4 });

        context.SuperDictionaries.Add(new SuperDictionary { Id = 13, Name = "Active", DictionaryId = 5 });
        context.SuperDictionaries.Add(new SuperDictionary { Id = 14, Name = "Rejected", DictionaryId = 5 });
        context.SuperDictionaries.Add(new SuperDictionary { Id = 15, Name = "Hired", DictionaryId = 5 });
        context.SuperDictionaries.Add(new SuperDictionary { Id = 16, Name = "Paused", DictionaryId = 5 });
    }

    if(!context.Roles.Any())
    {
        context.Roles.Add(new Role { Id = 1, Title = "Developer" });
        context.Roles.Add(new Role { Id = 2, Title = "LineManager" });
        context.Roles.Add(new Role { Id = 3, Title = "Admin" });
        context.Roles.Add(new Role { Id = 4, Title = "TeamLeader" });
        context.Roles.Add(new Role { Id = 5, Title = "HR" });
    }

    context.SaveChanges();
}

builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<ITestCompetenciesService, TestCompetenciesService>();
builder.Services.AddScoped<IHiringService, HiringService>();
builder.Services.AddScoped<IApplicantService, ApplicantService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ISuperDictionaryService, SuperDictionaryService>();

// Ñonfiguring Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "GB Corporation", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

// Configure Authentication 
var key = Encoding.ASCII.GetBytes("rhjkdSFdsSdhjsEdjkQdjskTdj");
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
 {
     x.RequireHttpsMetadata = false;
     x.SaveToken = true;
     x.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuerSigningKey = true,
         IssuerSigningKey = new SymmetricSecurityKey(key),
         ValidateIssuer = false,
         ValidateAudience = false
     };
 });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => options
    .WithOrigins(new[] {"http://localhost:3000", "http://localhost:8080", "http://localhost:4200"})
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    );

app.UseAuthentication(); //
app.UseAuthorization();

app.MapControllers();

app.Run();
