﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorUI.Data
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private HttpContext HttpContext { get; }

        public CustomAuthenticationStateProvider(IHttpContextAccessor httpContextAccessor)
        {
            HttpContext = httpContextAccessor.HttpContext;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity claimsIdentity;
            ClaimsPrincipal claimsPrincipal;

            if (HttpContext.Request.Cookies.TryGetValue("UserToken", out string token) && !string.IsNullOrEmpty(token))
            {
                JwtSecurityToken jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                claimsIdentity = new ClaimsIdentity(
                    jwtSecurityToken.Claims, CookieAuthenticationDefaults.AuthenticationScheme);

                claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                return Task.FromResult(new AuthenticationState(claimsPrincipal));
            }

            claimsIdentity = new ClaimsIdentity();
            claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            return Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        public void MarkUserAsAuthenticated(ClaimsIdentity identity)
        {
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public void MarkUserAsLoggedOut()
        {
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}
