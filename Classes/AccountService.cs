using TodoWebAPI.Models;

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
    }
}
