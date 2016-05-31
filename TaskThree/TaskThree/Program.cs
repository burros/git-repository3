using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAddressBook;
using MyAddressBook.Extention;
using MyLogger;

namespace TaskThree
{
    class Program
    {
        static void Main(string[] args)
        {
            AddressBook book = new AddressBook();


            book.AddUser(new User("Joey", "Tribbiani", "jtr@gmail.com", new DateTime(1980, 02, 10), "New York", "Bedford 90", "+44 20 7946 0213", Gender.Male, new DateTime(2016, 05, 25)));
            book.AddUser(new User("Chandler", "Bing", "chb@mail.com", new DateTime(1980, 01, 10), "New York", "Bedford 90", "+44 20 7946 0213", Gender.Male, new DateTime(2016, 05, 25)));
            book.AddUser(new User("Monica", "Geller", "moge@gmail.com", new DateTime(1980, 02, 10), "New York", "Bedford 90", "+44 20 7946 0213", Gender.Famale, new DateTime(2016, 05, 25)));
            book.AddUser(new User("Rachel", "Green", "rgr@mail.com", new DateTime(1980, 05, 31), "Киеве", "Bedford 90", "+44 20 7946 0213", Gender.Famale, new DateTime(2016, 05, 25)));
            book.AddUser(new User("Phoebe", "Buffay", "phb@mail.com", new DateTime(1999, 05, 31), "Киеве", "Bedford 90", "+44 20 7946 0213", Gender.Famale, new DateTime(2016, 05, 20)));
            book.AddUser(new User("Ross", "Geller", "rg@mail.com", new DateTime(1980, 01, 10), "New York", "Bedford 90", "+44 20 7946 0213", Gender.Male, new DateTime(2016, 05, 25)));
            book.AddUser(new User("John", "Smith", "jh@mail.com", new DateTime(1980, 01, 10), "New York", "Bedford 90", "+44 20 7946 0213", Gender.Male, new DateTime(2016, 05, 25)));
            book.AddUser(new User("Ivan", "Potapov", "jh@mail.com", new DateTime(1980, 01, 10), "New York", "", "", Gender.Male, new DateTime(2016, 05, 25)));

            
            // Демонтрація відкладеного виконання LINQ
            Console.WriteLine("Demo");
            // Якби лінк запит виконався вже, він би містив в собі два елемент
            var query = book.Users.Where(user => user.City == "Киеве");
            // Додаємо ще одного користувача
            book.AddUser(new User("Rachel", "Brown", "rgr@mail.com", new DateTime(2000, 05, 31), "Киеве", "Bedford 90", "+44 20 7946 0213", Gender.Famale, new DateTime(2016, 05, 25)));
            // Виводимо кількість користувачів
            Console.WriteLine(query.Count());
            // Вивилось не 2, а 3 та як запит виконався під час виклику Count()
            // LINQ запити виконується негайно у випадку коли повертають атомарне значення наприклад Count(), Average(), First() і т.д.
            // або коли викликаємо ToArray<T>(), ToList<T>(), ToDictionary() 
            // В загальному ж випадку запит не виконується, а зміна якій присвоєно запит містить лише набір інструкцій (команд)
            book.RemoveUser("Rachel", "Brown", "rgr@mail.com");
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Task 1");
            foreach (var item in book.WhereGmail())
            {
                Console.WriteLine("{0} {1} {2} {3} {4} {5}", item.FirstName, item.SecondName, item.City, item.Email, item.Birthdate, item.Gender);
            }
            Console.WriteLine();

            Console.WriteLine("Task 2");
            foreach (var item in book.Users.ExtentionWithYield())
            {
                Console.WriteLine("{0} {1} {2} {3} {4} {5}", item.FirstName, item.SecondName, item.City, item.Email, item.Birthdate, item.Gender);
            }
            Console.WriteLine();

            Console.WriteLine("Task 3");
            foreach (var item in book.WhereGirlAnd10Days())
            {
                Console.WriteLine("{0} {1} {2} {3} {4} {5}", item.FirstName, item.SecondName, item.City, item.Email, item.Birthdate, item.Gender);
            }
            Console.WriteLine();

            Console.WriteLine("Task 4");
            foreach (var item in book.WhereJanuaryAddressPhone())
            {
                Console.WriteLine("{0} {1} {2} {3} {4} {5}", item.FirstName, item.SecondName, item.City, item.Email, item.Birthdate, item.Gender);
            }
            Console.WriteLine();

            Console.WriteLine("Task 5");
            foreach (var userKey in book.WhereKeyGender())
            {
                Console.WriteLine("Key: {0}", userKey.Key);
                foreach (var user in userKey)
                {
                    Console.WriteLine("{0} {1} {2} ", user.FirstName, user.SecondName, user.Gender);
                }
            }
            Console.WriteLine();

            Console.WriteLine("Task 6");
            foreach (var item in book.WhereLambdaPaging(u => u.City == "New York", 2,2))
            {
                Console.WriteLine("{0} {1} {2} {3} {4} {5}", item.FirstName, item.SecondName, item.City, item.Email, item.Birthdate, item.Gender);
            }
            Console.WriteLine();

            Console.WriteLine("Task 7");
            Console.WriteLine("Count: {0}", book.WhereCityBirthdate("Киеве"));
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
