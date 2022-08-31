using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ToDoApp.Data.Entities;

namespace ToDoApp.Helper
{
    internal static class Auth
    {
        private static bool ByteArraysEqual(byte[] b1, byte[] b2)
        {
            // https://stackoverflow.com/questions/1389570/c-sharp-byte-array-comparison
            if (b1 == b2) return true;
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;

            for (int i = 0; i < b1.Length; i++)
                if (b1[i] != b2[i]) return false;

            return true;
        }

        public static string HashPassword(string password)
        {
            // Хеширование пароля
            // shorturl.at/jyBEI
            byte[] salt;
            byte[] buffer2;

            if (password == null)
                throw new ArgumentNullException("password");

            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }

            byte[] dst = new byte[0x31];

            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);

            return Convert.ToBase64String(dst);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            // Проверка пароля
            // shorturl.at/jyBEI
            byte[] buffer4;

            if (hashedPassword == null)
                return false;

            if (password == null)
                throw new ArgumentNullException("password");

            byte[] src = Convert.FromBase64String(hashedPassword);

            if ((src.Length != 0x31) || (src[0] != 0))
                return false;

            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);

            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);

            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
                buffer4 = bytes.GetBytes(0x20);

            return ByteArraysEqual(buffer3, buffer4);
        }

        public static string GenerateJwtToken(this IConfiguration configuration, User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["JWT:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
