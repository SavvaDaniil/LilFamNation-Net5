using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Components
{
    public class AuthUserJWTOptions
    {
        public const string ISSUER = "LilFamNATION"; // издатель токена
        public const string AUDIENCE = "User"; // потребитель токена
        const string KEY = "XXXXXXXXXXXXXXXXXXXXXXXXXXX";   // ключ для шифрации
        public const int LIFETIME = 60 * 24 * 365; // время жизни токена в минутах
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
