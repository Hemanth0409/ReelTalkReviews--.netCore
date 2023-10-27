using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ReelTalkReviews.Models;
using ReelTalkReviews.UtilitService;
using System.Text;
using static Org.BouncyCastle.Math.EC.ECCurve;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configure the Sql Server Database ConnectionStrings
builder.Services.AddDbContext<ReelTalkReviewsContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("RTRConnectionString")));
builder.Services.AddCors();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});
builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});
//builder.Services.AddScoped<IMailKitProvider, YourMailKitProviderImplementation>();
builder.Services.AddScoped<IEmailService, EmailService>();


builder.Services.AddAuthentication(authenticate =>
{
    authenticate.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authenticate.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;

    //Use to validate all the issuer and audience using TokenValidationParameter
    x.TokenValidationParameters = new TokenValidationParameters
    {
        //Both should be check to valid the user
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GnXXMmNjWUkjXQyJmoBesXgSRXEica7n")),
        ValidateAudience = false,
        ValidateIssuer = false ,
        ClockSkew = TimeSpan.Zero

    }; 
 

});

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//        .AddJwtBearer(options =>
//        {
//            options.TokenValidationParameters = new TokenValidationParameters
//            {
//                ValidateIssuer = true,
//                ValidateAudience = true,
//                ValidateLifetime = true,
//                ValidateIssuerSigningKey = true,
//                ValidIssuer = _config.AuthenticationSettings.TokenAuthority,
//                ValidAudience = _config.AuthenticationSettings.TokenAuthority,
//                LifetimeValidator = TokenLifetimeValidator.Validate,
//                IssuerSigningKey = new SymmetricSecurityKey(
//                    Encoding.UTF8.GetBytes(_config.AuthenticationSettings.SecurityKey))
//            };
//        });
builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin");
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();
