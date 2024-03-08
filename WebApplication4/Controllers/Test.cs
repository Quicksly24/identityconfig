using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication4.auth;
using WebApplication4.Dataccess;
using WebApplication4.models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebApplication4.Controllers
{
    [Authorize]
    [ApiController]
    public class Test : Controller
    {
        private readonly Idata _data;
        private readonly Gentoken token;
        private readonly Identity identity;

        public Test(Idata data,Gentoken token,Identity identity)
        {
            _data = data;
            this.token=token;
            this.identity = identity;
        }

        //[AllowAnonymous]
        [HttpGet("api/login")]
        public ActionResult Login()
        {
            var props = new AuthenticationProperties { RedirectUri = "/signin-google" };
            return Challenge(props, GoogleDefaults.AuthenticationScheme);
            //var auth = _data.Login(user, password);
            //var response = "success - " + token.gentoken(user, password);
            //return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("signin-google")]
        public async Task<ActionResult> Google()
        {
           // var response = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var token = await HttpContext.GetTokenAsync(CookieAuthenticationDefaults.AuthenticationScheme, "access_token");

            //if (response.Principal == null) return BadRequest();

            return Ok(token);
        }

        [AllowAnonymous]
        [HttpGet("api/Getresources")]
        public ActionResult getall()
        {
            var response = _data.usersall();
            return Ok(response);
            
        }

        [AllowAnonymous]
        [HttpGet("api/Getsingle")]
        public ActionResult get()
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            var email = User.FindFirstValue(ClaimTypes.Email);

            return Ok($"{email} {name}");
        }
    }
}

