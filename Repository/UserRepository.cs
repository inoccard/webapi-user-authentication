using System.Collections.Generic;
using System.Linq;
using ShopAPI.Entities;

namespace ShopAPI.Repository
{
    /// <summary>
    /// Repositório
    /// </summary>
    public class UserRepository
    {
        /// <summary>
        /// Obtém o usuário
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static User GetUser(string username, string password)
        {
            var users = new List<User> {
                new User {Id = 1, Username = "inocencio", Password="123"}, 
                new User {Id = 2, Username = "cardoso", Password="123"}
            };

            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password.ToLower() == password.ToLower()).FirstOrDefault();
        }
    }
}