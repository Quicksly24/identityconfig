using jwtIdentityconfig.auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebApplication4.auth;
using WebApplication4.Dataccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Googlekey>(builder.Configuration.GetSection("Googlekey"));

builder.Services.AddSingleton<Idata, Dataservice>();
builder.Services.AddScoped<Gentoken>();
builder.Services.AddScoped<Identity>();

builder.Services.AddControllers();

builder.Services.AddAuthentication(options=>{
    //defaultScheme: JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                          
                 })
                .AddCookie()
                .AddGoogle(googleOptions =>
                 {
                     var auth = builder.Configuration.GetSection("Googlekey").Get<Googlekey>();

                     googleOptions.ClientId = auth.ClientId;
                     googleOptions.ClientSecret = auth.ClientSecret;

                 });
//.AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
//{

//    ValidIssuer = "solo",
//    ValidAudience = "post",
//    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication-")),
//    ValidateIssuer = true,
//    ValidateLifetime = true,
//    ValidateIssuerSigningKey = true,
//    ValidateAudience = true,

//})


builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.MapControllers();

app.Run();
