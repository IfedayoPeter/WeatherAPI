using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WeatherAPI.Helpers;
using WeatherAPI.Repositories;

namespace WeatherAPI.services
{
    public class UserService : IUserService
    {
        public User Get(UserLogin user)
        {
            var userFound = UserRepository.
                Users.FirstOrDefault(o => 
                o.Username.Equals(user.Username, StringComparison.OrdinalIgnoreCase) 
                && o.Password.Equals(user.Password));

            return userFound;
        }

        public string Login(UserLogin userLogin)
        {
            if (string.IsNullOrEmpty(userLogin.Username) ||
                string.IsNullOrEmpty(userLogin.Password)) return null;
            var loggedInUser = Get(userLogin);
            if (loggedInUser is null) return null;

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, loggedInUser.Username),
                new Claim(ClaimTypes.Email, loggedInUser.Email),
                new Claim(ClaimTypes.GivenName, loggedInUser.FirstName),
                new Claim(ClaimTypes.Surname, loggedInUser.LastName),
                new Claim(ClaimTypes.Role, loggedInUser.Role)
            };

            var token = new JwtSecurityToken
            (
                issuer: ConfigurationManagerHelper.AppSettings["Jwt:Issuer"],
                audience: ConfigurationManagerHelper.AppSettings["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(24),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManagerHelper.AppSettings["Jwt:key"] 
                                                                    ?? string.Empty)),
                    SecurityAlgorithms.HmacSha256)
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
}
