using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Utilities;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Services
{
    public class AccessHandler
    {
        protected UnitOfWork Unit;
        public AccessHandler(UnitOfWork unit)
        {
            Unit = unit;
        }
        public User Check(string username, string password)
        {
            User user = (Unit.Users.Get(u => u.Username == username)).FirstOrDefault();
            if (user == null || user.Password != username.HashWith(password)) user = null;
            return user;
        }
        public string GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Crypto.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim("sub", user.Id.ToString()),
                    new Claim("name", user.Name),
                    new Claim("role", user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            return tokenHandler.WriteToken(tokenHandler.CreateJwtSecurityToken(tokenDescriptor));
        }
        public AuthenticationTicket CheckToken(string parameter, string scheme, IHeaderDictionary headers)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(parameter).Payload.ToArray();
            var claims = new[]
            {
                new Claim("sub", token.FirstOrDefault(c => c.Key == "sub").Value.ToString()),
                new Claim("name", token.FirstOrDefault(c => c.Key == "name").Value.ToString()),
                new Claim("role", token.FirstOrDefault(c => c.Key == "role").Value.ToString()),
            };
            headers.Add("IsAuthenticated", "true");
            headers.Add("UserId", claims[0].Value);
            headers.Add("UserName", claims[1].Value);
            headers.Add("UserRole", claims[2].Value);
            var identity = new ClaimsIdentity(claims, scheme);
            var principal = new ClaimsPrincipal(identity);
            return new AuthenticationTicket(principal, scheme);
        }
    }
}