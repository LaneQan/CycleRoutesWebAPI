using System.Security.Cryptography;
using System.Text;

namespace CycleRoutesCore.Domain.Helpers
{
    public static class PasswordHasher
    {
        public static string HashingPassword(this string password)
        {
            return Encoding.UTF8.GetString(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}