using System;
using System.Collections.Generic;

namespace MyAddressBook.Extention
{

    public static class MyExtention
    {
        //2
        //Выбрать (написать метод расширения, используя yield): пользователей, 
        //которым больше 18-ти лет и которые проживают в Киеве;
        public static IEnumerable<User> ExtentionWithYield(this IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                if (DateTime.Now >= user.Birthdate.AddYears(18) && user.City == "Киеве")
                {
                    yield return user;
                }
            }
            
        } 
    }
}
