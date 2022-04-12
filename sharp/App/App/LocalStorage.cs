using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;

namespace App
{
    public class User {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }


    };
    public class LocalStorage : IDisposable
    {
        private FileStream currentUsersFileStream;

        private FileStream usersFileStream;
        public LocalStorage(string filePath)
        {
            currentUsersFileStream = new FileStream("person.xml", FileMode.OpenOrCreate);
            usersFileStream = new FileStream("persons.xml", FileMode.OpenOrCreate);
        }

        public void Dispose()
        {
            currentUsersFileStream.Dispose();
            usersFileStream.Dispose();
        }

        public User LoadCurrentUser()
        {
            if (currentUsersFileStream.Length == 0)
            {
                return null;
            }
            XmlSerializer formatter = new XmlSerializer(typeof(User));
            return (User) formatter.Deserialize(currentUsersFileStream);
        }

        public IEnumerable<User> LoadUsersFromStorage()
        {
            if (usersFileStream.Length == 0)
            {
                return null;
            }
            XmlSerializer formatter = new XmlSerializer(typeof(List<User>));
            return (IEnumerable<User>)formatter.Deserialize(usersFileStream);
        }

        public void SaveCurrentUser(User user)
        {
            currentUsersFileStream.SetLength(0);
            XmlSerializer formatter = new XmlSerializer(typeof(User));
            formatter.Serialize(currentUsersFileStream, user);

        }
        public void SaveUsers(IEnumerable<User> users)
        {
            usersFileStream.SetLength(0);
            XmlSerializer formatter = new XmlSerializer(typeof(List<User>));
            formatter.Serialize(usersFileStream, users.ToList());
        }
    }

    public class UserStorage
    {
        private IEnumerable<User> Users;

        public User CurrentUser { get; private set; }

        private readonly LocalStorage storage;

        public UserStorage(LocalStorage localStorage)
        {
            storage = localStorage;
            CurrentUser = localStorage.LoadCurrentUser();
            Users = localStorage.LoadUsersFromStorage() ?? new List<User>();
        }

        public bool FindByUserName(string userName, out User user)
        {
            var findUser = Users.Where((x) => x.UserName == userName).FirstOrDefault();
            if (findUser != null)
            {
                user = findUser;
                return true;
            }

            user = null;
            return false;
        }

        public bool LogIn(string userName, string password, out User user)
        {
            if (!FindByUserName(userName, out var foundedUser))
            {
                user = null;
                return false;
            }
            if (!foundedUser.Password.Equals(password))
            {
                user = null;
                return false;
            }
            user = CurrentUser = foundedUser;
            storage.SaveCurrentUser(user);
            return true;
        }

        public bool Register(string userName, string password, string firstName, string lastName)
        {
            if (FindByUserName(userName, out var foundedUser))
            {
                return false;
            }

            var user = new User { 
                FirstName = firstName,
                UserName = userName,
                Password = password,
                LastName = lastName
            };

            Users = Users.Append(user);

            storage.SaveUsers(Users);

            return true;
        }

        public bool LogOut()
        {
            if (CurrentUser == null)
            {
                return false;
            }

            CurrentUser = null;
            storage.SaveCurrentUser(null);
            return true;

        }
    }
}
