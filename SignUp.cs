using ApplicationsExceptionNamespace;
using StartNamespace;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace EntityNamespace
{
    public class SignUP
    {
        private static bool ValidInfos(string name, string surname, string age, string username, string password, string city, string phoneNumber)
        {
            StackFrame callStack = new StackFrame(1, true);
            string currentFile = new StackTrace(true).GetFrame(0).GetFileName();
            // Check null or empty
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(age)
             || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(city)
             || string.IsNullOrEmpty(phoneNumber))
            {
                Warning.Message("ALL INFORMATIONS WERE NOT ENTERED!");
                throw new DetailedException("All Informations Were Not Entered!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
            }

            // Check username
            if (UsernameWasTaken(username))
            {
                Warning.Message("USERNAME WAS TAKEN!");
                throw new DetailedException("Username Was Taken!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
            }

            // Check Age
            if (IsValidAge(age) == -1)
            {
                Warning.Message("INCORRECT AGE WAS ENTERED!");
                throw new DetailedException("Incorrect Age Was Entered!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
            }

            if (!IsValidPhoneNumber(phoneNumber))
            {
                Warning.Message("INCORRECT PHONE NUMBER WAS ENTERED!");
                throw new DetailedException("Incorrect Phone Number Was Entered!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
            }

            return true;
        }
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            Regex regex = new Regex(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4}$");

            return regex.IsMatch(phoneNumber);
        }
        private static bool UsernameWasTaken(string username)
        {
            if (GlobalData.database.Workers != null)
            {
                foreach (Worker worker in GlobalData.database.Workers)
                {
                    if (worker.Username == username)
                    {
                        return true;
                    }
                }
            }
            if (GlobalData.database.Employers != null)
            {
                foreach (Employer employer in GlobalData.database.Employers)
                {
                    if (employer.Username == username)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private static int IsValidAge(string age)
        {
            int intAge;
            bool converted = int.TryParse(age, out intAge);
            if (converted)
            {
                if (intAge >= 0)
                {
                    return intAge;
                }
            }
            return -1;
        }
        public static Human SignUp()
        {
            Console.CursorVisible = true;
            Console.Write("\n ENTER YOUR NAME : ");
            string name = Console.ReadLine().Trim();

            Console.Write("\n ENTER YOUR SURNAME : ");
            string surname = Console.ReadLine().Trim();

            Console.Write("\n ENTER YOUR AGE : ");
            string age = Console.ReadLine().Trim();

            Console.Write("\n ENTER YOUR USERNAME : ");
            string username = Console.ReadLine().Trim();

            Console.Write("\n ENTER YOUR PASSWORD : ");
            string password = Console.ReadLine().Trim();

            Console.Write("\n ENTER YOUR CITY : ");
            string city = Console.ReadLine().Trim();

            Console.WriteLine();
            Console.WriteLine(@" PHONE NUMBER EXAMPLES : 
    111 222 3344
    1234567890
    +1234567890
    +123-456-7890
    123-456-7890
    123-456-27890");

            Console.Write("\n ENTER YOUR PHONE NUMBER : ");
            string phone = Console.ReadLine().Trim();

            Console.CursorVisible = false;
            if (ValidInfos(name, surname, age, username, password, city, phone))
            {
                int.TryParse(age, out int age2);
                return new Human(name, surname, age2, username, password, city, phone);
            }
            return null;
        }
    }
}