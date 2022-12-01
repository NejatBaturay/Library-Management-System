using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LibraryManagement.Models;

namespace WebApplication1
{
    public class JWTmanager
    {


        private static string SecretImportInformation = "APNDASL8546564ads0129319832UVmt7";
        public static string GenerateToken(YetkiliBilgi yetkiliBilgi)
        {


            KutuphaneContext db = new KutuphaneContext();
            //yetkiliBilgi.SicilNo == yetkili.SicilNo && yetkiliBilgi.sifre == yetkili.sifre

            foreach (var yetkili in db.Yetkililer.ToList())
            {
                if (yetkili.SicilNo == yetkiliBilgi.SicilNo && yetkili.sifre==yetkiliBilgi.sifre)
                {

                    byte[] key = Convert.FromBase64String(SecretImportInformation);
                    SymmetricSecurityKey secureityKey = new SymmetricSecurityKey(key);
                    SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
                    {
                        Issuer = "http://www.localhost",
                        Subject = new ClaimsIdentity(new[]{
                        new Claim(ClaimTypes.Role,"User")}),
                        Expires = DateTime.Now.AddMinutes(2),
                        SigningCredentials = new SigningCredentials(secureityKey, SecurityAlgorithms.HmacSha256Signature)
                    };
                    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                    JwtSecurityToken securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);
                    return tokenHandler.WriteToken(securityToken);

                }
            }

            return "Error";




        }


        public static ClaimsPrincipal GetPrincipal(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtSecurityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
            if (jwtSecurityToken == null)
            {
                return null;
            }
            byte[] key = Convert.FromBase64String(SecretImportInformation);
            TokenValidationParameters parameters = new TokenValidationParameters()
            {

                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
            SecurityToken securityToken;
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, parameters, out securityToken);
            return principal;



        }
        public static string ValidateToken(string token)
        {
            string username = null;
            ClaimsPrincipal principal = GetPrincipal(token);
            if (principal == null)
            {
                return null;
            }
            ClaimsIdentity claimsIdentity = null;
            claimsIdentity = (ClaimsIdentity)principal.Identity;
            Claim usernameClaim = claimsIdentity.FindFirst(ClaimTypes.Name);
            username = usernameClaim.Value;

            return username;
        }
    }
}

