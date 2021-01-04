using System;
using System.Text;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ShopAPI.Entities;
using Microsoft.IdentityModel.Tokens;

namespace ShopAPI
{
    /// <summary>
    /// Serviço para receber usuário e gerar token
    /// </summary>
    public class TokenService
    {
        /// <summary>
        /// Gera token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GerenateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)

                }),
                Expires = DateTime.UtcNow.AddHours(2), //por questões de segurança, usar somente 2Hrs
                // usa chave simétrica para encriptar
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                  
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}