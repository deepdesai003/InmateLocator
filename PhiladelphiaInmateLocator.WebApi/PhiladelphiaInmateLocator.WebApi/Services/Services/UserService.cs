namespace PhiladelphiaInmateLocator.WebApi.Services.Services
{
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using PhiladelphiaInmateLocator.WebApi.Entities;
    using PhiladelphiaInmateLocator.WebApi.Models;
    using PhiladelphiaInmateLocator.WebApi.Services.Interface;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        public InmateDatabase _inmateDatabase;

        public UserService(InmateDatabase inmateDatabase)
        {
            _inmateDatabase = inmateDatabase;
        }



        public async Task<List<User>> GetAllUsers()
        {
            return await this._inmateDatabase.Users.ToListAsync().ConfigureAwait(false);
        }

        public async Task<User> Authenticate(string username, string password)
        {
            User user = this._inmateDatabase.Users.FirstOrDefault(u => u.Username.Equals(username));

            if(user == null)
            {
                return null;
            }        
            
            if(!user.Password.Equals(password))
            {
                return null;
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("your-256-bit-secret");
            ClaimsIdentity claimsIdentity = await this.CreateClaimsIdentities(user);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            try
            {
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return user;
        }


        public Task<ClaimsIdentity> CreateClaimsIdentities(User user)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Username));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, user.Role));
            claimsIdentity.AddClaim(new Claim("Location", user.Location));
            /*
            var roles = Enumerable.Empty<Role>(); // Not a real list.

            foreach(var role in roles)
            { claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.RoleName)); }
            */
            return Task.FromResult(claimsIdentity);
        }


    }
}
