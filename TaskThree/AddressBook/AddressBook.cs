using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
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

        // Property extension method task 2
        public List<User> Users
        {
            get { return _users;}
        }  

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


        //1
        //Выбрать (использовать LINQ - method syntax): пользователей, у которых Email-адрес имеет домен “gmail.com”;
        public  IEnumerable<User> WhereGmail()
        {
            return _users.Where(u => u.Email.Contains("gmail.com"));
        }

        //2  ВІДПОВІДЬ В ПАПЦІ Extention\MyExtention.cs
        //Выбрать (написать метод расширения, используя yield): пользователей, 
        //которым больше 18-ти лет и которые проживают в Киеве;

        //3
        //Выбрать (использовать LINQ - query syntax): пользователей, 
        //которые являются девушками и были добавлены за последние 10 дней;
        public IEnumerable<User> WhereGirlAnd10Days()
        {
            return from user in _users
                where user.Gender == Gender.Famale && DateTime.Now - user.TimeAdded <= new TimeSpan(10, 0, 0, 0)
                select user;
        }

        //4
        //Выбрать (использовать LINQ - method syntax): список пользователей, 
        //которые родились в январе, и при этом имеют заполненые поля адреса и - телефона. 
        //Список должен быть отсортирован по фамилии пользователя в обратном порядке.
        public IEnumerable<User> WhereJanuaryAddressPhone()
        {
            return _users.Where(
                u => u.Birthdate.Month == 1 && u.Address != string.Empty && u.PhoneNumber != String.Empty)
                .OrderByDescending(u => u.SecondName);
        }

        //5
        //Выбрать (использовать LINQ - method syntax): словарь, имеющий два ключа “man” и “woman”. 
        //По каждому из ключей словарь должен содержать список пользователей, которые соответствуют ключу словаря;
        public IEnumerable<IGrouping<Gender,User>> WhereKeyGender()
        {
            return _users.GroupBy(u=>u.Gender);
        }

        //6
        //Выбрать (использовать LINQ - method syntax): пользователей, передавая произвольное условие (лямбда - выражение) 
        //и два параметра - с какого элемента выбирать и по какой (paging).
        public IEnumerable<User> WhereLambdaPaging(Func<User, bool> predicate, int firstPage, int lastPage)
        {
            return _users.Where(predicate).Skip(firstPage).Take(lastPage);;
        } 

        //7
        //Выбрать (использовать LINQ - query syntax): количество пользователей, 
        //из города (передать в параметрах), у которых сегодня день рождения.
        public int WhereCityBirthdate(string cityName)
        {
            return (from user in _users
                         let dateNow = DateTime.Now
                         where
                             user.City == cityName && user.Birthdate.Day == dateNow.Day && user.Birthdate.Month == dateNow.Month
                         select user).Count();
        }
    }
}
