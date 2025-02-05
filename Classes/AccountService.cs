using TodoWebAPI.Models;
using BCrypt.Net;
using System.Text;
using System.Security.Cryptography;

namespace TodoWebAPI.Classes
{
    public class AccountService
    {
        public static bool CheckIfAccountExists(List<User> users,string userName)
        {
            if (users.Where(u => u.SUsername != null && u.SUsername.ToUpper() == userName.ToUpper()).Any())
            {
                return true;
            }

            return false;
        }

        public static string HashPassword(string password)
        {
            return getHashSha256(password);
        }

        public static string getHashSha256(string text)
        {
            byte[] hash;
            string hashString = String.Empty;
            using (SHA256 s256 = new SHA256Managed())
            {
                hash = s256.ComputeHash(Encoding.UTF8.GetBytes(text));
                s256.Clear();
            }

            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }

            return hashString;
        }
    }
}
