using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAddressBook
{
    public class UserEventArgs : EventArgs
    {
        public string Message { get; set; }
        public UserEventArgs(string message)
        {
            Message = message;
        }
    }

    public class AddressBook
    {
        public event EventHandler<UserEventArgs> UserAdded;
        public event EventHandler<UserEventArgs> UserRemoved;
        public event EventHandler<UserEventArgs> UserNotAdded;
        public event EventHandler<UserEventArgs> UserNotRemoved;
        private List<User> _users = new List<User>(); 

        public void AddUser(User user)
        {
            if (_users.Exists(u => u.FirstName == user.FirstName &&
                                           u.SecondName == user.SecondName &&
                                           u.Email == user.Email))
            {
                if (UserNotAdded != null)
                    UserNotAdded(this, new UserEventArgs("User exist. New user not added."));
            }
            else
            {
                _users.Add(user);
                if (UserAdded != null)
                    UserAdded(this, new UserEventArgs("User added."));
            }
        }

        public void RemoveUser(string firstName, string secondName, string email)
        {
            bool found = false;
            foreach (var user in _users)
            {
                if (user.FirstName == firstName && user.SecondName == secondName && user.Email == email)
                {
                    found = true;
                    _users.Remove(user);
                    break;
                }
                    
            }
            if (found)
            {
                if (UserRemoved != null) 
                    UserRemoved(this, new UserEventArgs("User removed."));
            }
            else 
                if (UserNotRemoved != null)
                    UserNotRemoved(this,new UserEventArgs("User not found and not removed."));
        }

        public IEnumerable<User> Checkbirthdate()
        {
            return from user in _users
                let dateNow = DateTime.Now
                where
                    user.Birthdate.Day == dateNow.Day && user.Birthdate.Month == dateNow.Month
                select user;
        }

    }
}
