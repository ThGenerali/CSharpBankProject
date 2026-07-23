using CSharpBankProject.src.BankConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBankProject.src.BankConsole.Data
{
    //UserRepository class is responsible for managing user accounts in the banking application. It maintains a collection of user accounts and provides methods to add, retrieve, and manage user data.
    internal class UserRepository
    {
        public Dictionary<Guid, User> Users { get; private set; }
        public UserRepository()
        {
            Users = new Dictionary<Guid, User>();
        }

        // Adds a new user account to the repository.
        public void AddUser(Guid Id, User user)
        {
            Users.Add(Id, user);
        }
                
        //Verify password method checks if the provided password matches the stored password for a specific user account. It retrieves the user account based on the unique identifier (Guid) and compares the provided password with the stored password.
        public bool VerifyPassword(string username, string password)
        {   
            var user = Users.Values.FirstOrDefault(u => u.Username == username); // Retrieve the user based on the provided username
            if (user != null)
            {
                return user.Password == password;
            }                                                      
            return false;
        }

        //VeryfyUsername method checks if the provided username exists in the user repository. It iterates through the collection of user accounts and compares the provided username with the usernames of existing users.

        public bool VerifyUsername(string username)
        {
            //Iterate through the collection of user accounts and check if the provided username exists
            foreach (var user in Users.Values) 
            {
                if (user.Username == username)
                {
                    return true;
                }
            }
            return false;
        }

        //VerifyRegisterInfo method checks if the provided registration information (name, username, and password) is valid for creating a new user account. It verifies that the name and username are not empty and that the password meets certain criteria (e.g., minimum length).
        public bool VerifyRegisterPassword(string password, string confirmPassword)
        {
            if (confirmPassword != password) { return false; }
            return true;
        }

        public List<string> GetUserInfo(Guid Id)
        {
            if (Users.TryGetValue(Id, out User user))
            {
                List<string> userInfo = new List<string>
                {
                    $"Name: {user.Name}",
                    $"User fullname: {user.Username}",
                    $"Password: {user.Password}"
                };
                return userInfo;
            } else {
                throw new KeyNotFoundException("User not found.");
            }
        }

    }
}
                                                