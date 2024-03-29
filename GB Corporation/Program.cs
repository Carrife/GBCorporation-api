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

using(AppDbContext context = new())
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
        context.SuperDictionaries.Add(new SuperDictionary { Id = 14, Name = "InProgress", DictionaryId = 5 });
        context.SuperDictionaries.Add(new SuperDictionary { Id = 15, Name = "Hired", DictionaryId = 5 });

        context.SuperDictionaries.Add(new SuperDictionary { Id = 16, Name = "Open", DictionaryId = 6 });
        context.SuperDictionaries.Add(new SuperDictionary { Id = 17, Name = "Closed", DictionaryId = 6 });

        context.SuperDictionaries.Add(new SuperDictionary { Id = 18, Name = "Accountant", DictionaryId = 7 });
        context.SuperDictionaries.Add(new SuperDictionary { Id = 19, Name = "Business Analyst", DictionaryId = 7 });
        context.SuperDictionaries.Add(new SuperDictionary { Id = 20, Name = "Junior Software Developer", DictionaryId = 7 });
        context.SuperDictionaries.Add(new SuperDictionary { Id = 21, Name = "Middle Software Developer", DictionaryId = 7 });
        context.SuperDictionaries.Add(new SuperDictionary { Id = 22, Name = "Senior Software Developer", DictionaryId = 7 });
        context.SuperDictionaries.Add(new SuperDictionary { Id = 23, Name = "HR", DictionaryId = 7 });
        context.SuperDictionaries.Add(new SuperDictionary { Id = 24, Name = "Administrator", DictionaryId = 7 });
        context.SuperDictionaries.Add(new SuperDictionary { Id = 25, Name = "Line Manager", DictionaryId = 7 });
        context.SuperDictionaries.Add(new SuperDictionary { Id = 26, Name = "CEO", DictionaryId = 7 });
        context.SuperDictionaries.Add(new SuperDictionary { Id = 27, Name = "Chief Accountant", DictionaryId = 7 });

        context.SuperDictionaries.Add(new SuperDictionary { Id = 16, Name = "Open", DictionaryId = 8 });
        context.SuperDictionaries.Add(new SuperDictionary { Id = 17, Name = "Closed", DictionaryId = 8 });
    }

    if(!context.Roles.Any())
    {
        context.Roles.Add(new Role { Id = 1, Title = "Developer" });
        context.Roles.Add(new Role { Id = 2, Title = "LineManager" });
        context.Roles.Add(new Role { Id = 3, Title = "Admin" });
        context.Roles.Add(new Role { Id = 4, Title = "TeamLeader" });
        context.Roles.Add(new Role { Id = 5, Title = "HR" });
        context.Roles.Add(new Role { Id = 6, Title = "Accountant" });
        context.Roles.Add(new Role { Id = 7, Title = "CEO" });
        context.Roles.Add(new Role { Id = 8, Title = "BA" });
        context.Roles.Add(new Role { Id = 9, Title = "ChiefAccountant" });
    }

    if(!context.Employees.Any())
    {
        context.Employees.Add(new Employee { 
            Id = 1, 
            NameRu = "�����", 
            SurnameRu = "���������", 
            PatronymicRu = "�������������", 
            NameEn = "Pavel", 
            SurnameEn = "Nikimarov",
            Login = "pav",
            Phone = "+375336473809",
            Email = "pav@mail.com",
            Password = BCrypt.Net.BCrypt.HashPassword("rtt12tt"),
            DepartmentId = 5,
            RoleId = 3,
            StatusId = 11,
            PositionId = 24,
        });
    }

    context.SaveChanges();
}

builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<ITestCompetenciesService, TestCompetenciesService>();
builder.Services.AddScoped<IHiringService, HiringService>();
builder.Services.AddScoped<IApplicantService, ApplicantService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ISuperDictionaryService, SuperDictionaryService>();

// �onfiguring Swagger/OpenAPI
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
