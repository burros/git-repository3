using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MyAddressBook;


namespace Notifier
{
    class Program
    {

        static void Main(string[] args)
        {
            AddressBook book = new AddressBook();

            // ЗМІНІТЬ ЯКОМУСЬ З КОРИТСУВАЧІ ДАТУ НА СЬОДНІШНЮ ЩО ПОБАЧИТИ ВІДПРАВКУ ЛИСТА.
            // АДРУСУ ПОЧТИ НЕ ЗМІНЮЙТЕ
            book.AddUser(new User("Joey", "Tribbiani", "binarystudiotask@gmail.com", new DateTime(1980, 05, 30), "New York", "Bedford 90", "+44 20 7946 0213", Gender.Male, new DateTime(2016, 05, 25)));
            book.AddUser(new User("Chandler", "Bing", "binarystudiotask@mail.com", new DateTime(1980, 05, 31), "New York", "Bedford 90", "+44 20 7946 0213", Gender.Male, new DateTime(2016, 05, 25)));
            book.AddUser(new User("Monica", "Geller", "binarystudiotask@gmail.com", new DateTime(1980, 06, 01), "New York", "Bedford 90", "+44 20 7946 0213", Gender.Famale, new DateTime(2016, 05, 25)));
            book.AddUser(new User("Rachel", "Green", "binarystudiotask@mail.com", new DateTime(1980, 06, 02), "Киеве", "Bedford 90", "+44 20 7946 0213", Gender.Famale, new DateTime(2016, 05, 25)));

            if (book.Checkbirthdate().Any())
            {
                Console.WriteLine("Сьогоднi є днi народження.");

                foreach (var user in book.Checkbirthdate())
                {
                    EmailSender(user);
                }
            }
            else
            {
                Console.WriteLine("Сьогоднi нiхто нi в кого немає дня народження(");
            }
            Console.ReadKey();
        }


        // Використовувати для відправки та отримання краще Gmail, інші можуть блокувати.
        // Для введеної адреси доступ є "binarystudiotask@gmail.com" 
        // при використанні інших потрібно натиснути Включити https://www.google.com/settings/security/lesssecureapps
        private static void EmailSender(User user)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                // Адреса почти та пароль правдиві, можна на почту глянути за листом)
                client.Credentials = new System.Net.NetworkCredential("binarystudiotask@gmail.com", "binarystudiotask12345");
                MailMessage mm = new MailMessage("binarystudiotask@gmail.com", user.Email, "Вітання.", "З днем народження " + user.FirstName + " " + user.SecondName);
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);
                Console.WriteLine("Повiдомлення вiдправлено.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Помилка вiдправки {0}",e);
            }
        }
    }
}
