using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TaskManager.Server.Entity;

public class AutOptions
{
    public const string ISSUER = "MyAuthServer"; // издатель токена
    public const string AUDIENCE = "MyAuthClient"; // потребитель токена
    const string KEY = "MySecretKey!_1234";   // ключ для шифрации
    public const int LIFETIME = 5;
    public static SymmetricSecurityKey GetSymmetricSecurityKey() => 
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}