using Helper;
using System;
using System.Collections.Generic;

namespace EntityNamespace
{
    public class Human : object
    {
        public static int StaticID { get; set; } = 0;
        public int MyId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public List<Notification> Notifications { get; set; } = new List<Notification>();
        public Human(string name, string surname, int age, string username, string password, string city, string phone)
        {
            MyId = StaticID++;
            Name = name;
            Surname = surname;
            Username = username;
            Password = password;
            City = city;
            Phone = phone;
            Age = age;
        }
        public void SeeNotifications()
        {
            VisualHelper.ShowMyNotificationsScript();
            if (Notifications.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  THERE IS NO NOTIFICATION HERE :(");
                Console.WriteLine("\n  Press Any Key To Continue . . .");
            }
            else
            {
                int rank = 1;
                foreach (Notification notification in Notifications)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"  Message {rank}");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(notification);
                    rank++;
                }
                Console.WriteLine("\nPress Any Key To Continue . . .");
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("");
            }
            Console.ReadKey();
        }
    }
}
