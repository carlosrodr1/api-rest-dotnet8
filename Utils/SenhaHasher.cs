using BCrypt.Net;

namespace UserAuthService.API.Utils
{
    public static class SenhaHasher
    {
        public static string GerarHash(string senha) => BCrypt.Net.BCrypt.HashPassword(senha);
    }
}
