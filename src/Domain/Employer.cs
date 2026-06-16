using ApplicationsExceptionNamespace;
using Helper;
using StartNamespace;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace EntityNamespace
{
    public class Employer : Human
    {
        public List<Vacancy> Vacancies { get; set; } = new List<Vacancy>();
        public Employer(string name, string surname, string username, string password, string city, string phone, int age)
            : base(name, surname, age, username, password, city, phone)
        {
        }
        public Employer(Human currentUser)
            : base(currentUser.Name, currentUser.Surname, currentUser.Age, currentUser.Username, currentUser.Password, currentUser.City, currentUser.Phone)
        {

        }
        public void SeeVacancies()
        {
            VisualHelper.ShowMyVacanciesScript();
            if (Vacancies.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  THERE IS NO VACANCY HERE :(");
                Console.WriteLine("\n  Press Any Key To Continue . . .");
            }
            else
            {
                foreach (Vacancy vacancy in Vacancies)
                {
                    Console.WriteLine(vacancy);
                }
                Console.WriteLine("\nPress Any Key To Continue . . .");
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("");
            }
            Console.ReadKey();
        }
        private bool IsValidEmail(string email)
        {
            Regex regex = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");

            return regex.IsMatch(email);
        }
        private bool ValidInformations(int salary, bool convertedSalary, string name, string region, string ageRange, string education, string workExperience, bool dateConverted, DateTime deadlineDate, string contactName, string phoneNumber, string email, string jobDescription, string requirements)
        {
            StackFrame callStack = new StackFrame(1, true);
            string currentFile = new StackTrace(true).GetFrame(0).GetFileName();


            if (salary == 0 || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(region) || string.IsNullOrEmpty(ageRange) || string.IsNullOrEmpty(education)
                            || string.IsNullOrEmpty(workExperience) || string.IsNullOrEmpty(contactName) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(email)
                            || string.IsNullOrEmpty(jobDescription) || string.IsNullOrEmpty(requirements) || string.IsNullOrEmpty(deadlineDate.ToString()))
            {
                Warning.Message("ALL INFORMATIONS WERE NOT ENTERED!");
                throw new DetailedException("All Informations Were Not Entered!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
            }

            if (!convertedSalary)
            {
                Warning.Message("INCORRECT SALARY WAS ENTERED!");
                throw new DetailedException("Incorrect Salary Was Entered!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
            }
            if (!dateConverted)
            {
                Warning.Message("INCORRECT DATE WAS ENTERED!");
                throw new DetailedException("Incorrect Date Was Entered!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
            }

            if (deadlineDate < DateTime.Now)
            {
                Warning.Message("INCORRECT DEADLINE WAS ENTERED!");
                throw new DetailedException("Incorrect Deadline Date Was Entered!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
            }

            if (!SignUP.IsValidPhoneNumber(phoneNumber))
            {
                Warning.Message("INCORRECT PHONE NUMBER WAS ENTERED!");
                throw new DetailedException("Incorrect Phone Number Was Entered!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
            }
            if (!IsValidEmail(email))
            {
                Warning.Message("INCORRECT EMAIL WAS ENTERED!");
                throw new DetailedException("Incorrect Email Was Entered!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
            }
            return true;
        }
        public void AddVacancy()
        {
            Console.ForegroundColor = ConsoleColor.White;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(0, 10);
                Console.WriteLine(@"
                                ╔════════════════════════════════════════════════════╗
                                ║                                                    ║
                                ║            DO YOU WANT TO ADD VACANY ?             ║  
                                ║                                                    ║
                                ║               YES (ENTER) | NO (ESC)               ║      
                                ║                                                    ║
                                ╚════════════════════════════════════════════════════╝");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    Console.CursorVisible = true;
                    Console.Clear();
                    VisualHelper.ShowAddingVacancyScript();
                    int salary;
                    Console.Write("\n  ENTER SALARY FOR THE JOB ($) : ");
                    bool convertedSalary = int.TryParse(Console.ReadLine(), out salary);

                    Console.Write("\n  ENTER NAME OF THE JOB : ");
                    string name = Console.ReadLine();

                    Console.Write("\n  ENTER REGION : ");
                    string region = Console.ReadLine();

                    Console.Write("\n  ENTER AGE RANGE : ");
                    string ageRange = Console.ReadLine();

                    Console.Write("\n  ENTER EDUCATION REQUIREMENT : ");
                    string education = Console.ReadLine();

                    Console.Write("\n  ENTER WORK EXPERIENCE : ");
                    string workExperience = Console.ReadLine();

                    Console.Write("\n  ENTER DEADLINE DATE FOR VACANCY : ");
                    var date = Console.ReadLine();
                    DateTime deadlineDate;
                    bool convertedDate = DateTime.TryParse(date, out deadlineDate);

                    Console.Write("\n  ENTER CONTACT NAME : ");
                    string contactName = Console.ReadLine();

                    Console.WriteLine();
                    Console.WriteLine(@"  PHONE NUMBER EXAMPLES : 
    111 222 3344
    1234567890
    +1234567890
    +123-456-7890
    123-456-7890
    123-456-27890");

                    Console.Write("\n  ENTER PHONE NUMBER : ");
                    string phoneNumber = Console.ReadLine();

                    Console.Write("\n  ENTER EMAIL : ");
                    string email = Console.ReadLine();

                    Console.Write("\n  ENTER JOB DESCRIPTION : ");
                    string jobDescription = Console.ReadLine();

                    Console.Write("\n  ENTER REQUIREMENTS : ");
                    string requirements = Console.ReadLine();

                    if (ValidInformations(salary, convertedSalary, name, region, ageRange, education, workExperience, convertedDate, deadlineDate, contactName, phoneNumber, email, jobDescription, requirements))
                    {
                        Vacancy vacancy = new Vacancy(salary, name, region, ageRange, education, workExperience, DateTime.Now.ToLongDateString(), deadlineDate.ToLongDateString(), contactName, phoneNumber, email, jobDescription, requirements);
                        Vacancies.Add(vacancy);
                        JsonSerialization.SerializeDatabase(GlobalData.database);
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("\n  Vacancy Was Successfully Created And Added To Your Vacancies!");
                        Warning.PressAnyKey();
                    }
                    Console.CursorVisible = false;
                    break;
                }
            }
        }
        public void DeleteVacancy()
        {
            if (Vacancies.Count == 0)
            {
                VisualHelper.ShowDeleteVacancyHeadlineScript();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  THERE IS NO VACANCY HERE :(");
                Warning.PressAnyKey();
            }
            else
            {
                int size = Vacancies.Count + 1;
                ConsoleColor[] Set = new ConsoleColor[size];
                int counter = 0;
                while (true)
                {
                    Console.Clear();
                    Console.CursorVisible = false;
                    VisualHelper.ShowDeleteVacancyHeadlineScript();
                    for (int x = 0; x < size; x++)
                    {
                        if (counter == x)
                        {
                            Set[x] = ConsoleColor.Yellow;
                        }
                        else
                        {
                            Set[x] = ConsoleColor.White;
                        }
                    }

                    for (int y = 0; y < size - 1; y++)
                    {
                        string frame = Vacancies[y].Name.FrameTheWord();

                        Console.ForegroundColor = Set[y];
                        Console.WriteLine(frame);
                    }
                    Console.ForegroundColor = Set[size - 1];
                    string back = "BACK";
                    string backFrame = back.FrameTheWord();
                    Console.WriteLine(backFrame);

                    Console.SetCursorPosition(0, counter * 2);
                    Console.WriteLine("");

                    var key = Console.ReadKey();

                    if (key.Key == ConsoleKey.UpArrow && counter != 0) // Up
                    {
                        counter--;
                    }
                    else if (key.Key == ConsoleKey.DownArrow && counter != size - 1) // Down
                    {
                        counter++;
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        if (counter == size - 1)
                            break;

                        Console.Clear();
                        bool deleted = DeleteVacancyOrNot(counter);
                        if (deleted)
                            size--;
                    }
                }
            }
        }
        private bool DeleteVacancyOrNot(int index)
        {
            int size = 2;
            ConsoleColor[] Set = new ConsoleColor[size];
            int counter = 0;
            while (true)
            {
                VisualHelper.ShowDeleteVacancyHeadlineScript();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(Vacancies[index].ShowVacancyDetails());
                Console.CursorVisible = false;

                if (counter == 0)
                {
                    Set[0] = ConsoleColor.Green;
                }
                else
                {
                    Set[0] = ConsoleColor.Yellow;
                }
                if (counter == 1)
                {
                    Set[1] = ConsoleColor.Red;
                }
                else
                {
                    Set[1] = ConsoleColor.Yellow;
                }

                Console.ForegroundColor = Set[0];
                VisualHelper.ShowBackScriptOnLeft();
                Console.ForegroundColor = Set[1];
                VisualHelper.ShowDeleteVacancyScript2();

                var key = Console.ReadKey();

                if (key.Key == ConsoleKey.UpArrow && counter != 0) // Up
                {
                    counter--;
                }
                else if (key.Key == ConsoleKey.DownArrow && counter != size - 1) // Down
                {
                    counter++;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    if (counter == 0)
                        return false;

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"\n  Your Vacancy Was Deleted (Vacancy Name : {Vacancies[index].Name})");
                    Vacancies.RemoveAt(index);

                    JsonSerialization.SerializeDatabase(GlobalData.database);

                    Warning.PressAnyKey();
                    return true;
                }
            }
        }
        public void HireWorker()
        {
            if (Vacancies.Count == 0)
            {
                Console.Clear();
                VisualHelper.ShowHireWorkerHeadlineScript();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  THERE IS NO VACANCY HERE :(");
                Warning.PressAnyKey();
            }
            else
            {
                int size = Vacancies.Count + 1;
                ConsoleColor[] Set = new ConsoleColor[size];
                int counter = 0;
                while (true)
                {
                    Console.Clear();
                    Console.CursorVisible = false;
                    VisualHelper.ShowHireWorkerHeadlineScript();
                    for (int x = 0; x < size; x++)
                    {
                        if (counter == x)
                        {
                            Set[x] = ConsoleColor.Yellow;
                        }
                        else
                        {
                            Set[x] = ConsoleColor.White;
                        }
                    }

                    for (int y = 0; y < size - 1; y++)
                    {
                        string frame = Vacancies[y].Name.FrameTheWord();

                        Console.ForegroundColor = Set[y];
                        Console.WriteLine(frame);
                    }
                    Console.ForegroundColor = Set[size - 1];
                    string back = "BACK";
                    string backFrame = back.FrameTheWord();
                    Console.WriteLine(backFrame);

                    Console.SetCursorPosition(0, counter * 2);
                    Console.WriteLine("");

                    var key = Console.ReadKey();

                    if (key.Key == ConsoleKey.UpArrow && counter != 0) // Up
                    {
                        counter--;
                    }
                    else if (key.Key == ConsoleKey.DownArrow && counter != size - 1) // Down
                    {
                        counter++;
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        if (counter == size - 1)
                            break;

                        bool hired = ShowVacancyAppliers(Vacancies[counter]);
                        if (hired)
                            size--;
                    }
                }
            }
        }
        public static bool ShowVacancyAppliers(Vacancy vacancy)
        {
            if (vacancy.Appliers.Count == 0)
            {
                Console.Clear();
                VisualHelper.ShowVacancyAppliersScript();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  THERE IS NO APPLIER HERE :(");
                Warning.PressAnyKey();
                return false;
            }
            else
            {
                int size = vacancy.Appliers.Count + 1;
                int counter = 0;
                while (true)
                {
                    size = vacancy.Appliers.Count + 1;
                    ConsoleColor[] Set = new ConsoleColor[size];
                    Console.Clear();
                    Console.CursorVisible = false;
                    VisualHelper.ShowVacancyAppliersScript();
                    for (int x = 0; x < size; x++)
                    {
                        if (counter == x)
                        {
                            Set[x] = ConsoleColor.Yellow;
                        }
                        else
                        {
                            Set[x] = ConsoleColor.White;
                        }
                    }

                    for (int y = 0; y < size - 1; y++)
                    {
                        string fullname = $"{vacancy.Appliers[y].Name} {vacancy.Appliers[y].Surname}";
                        string frame = fullname.FrameTheWord();

                        Console.ForegroundColor = Set[y];
                        Console.WriteLine(frame);
                    }
                    Console.ForegroundColor = Set[size - 1];
                    string back = "BACK";
                    string backFrame = back.FrameTheWord();
                    Console.WriteLine(backFrame);

                    Console.SetCursorPosition(0, counter * 2);
                    Console.WriteLine("");

                    var key = Console.ReadKey();

                    if (key.Key == ConsoleKey.UpArrow && counter != 0) // Up
                    {
                        counter--;
                    }
                    else if (key.Key == ConsoleKey.DownArrow && counter != size - 1) // Down
                    {
                        counter++;
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        if (counter == size - 1)
                            break;

                        bool accepted = SeeApplierCVs(vacancy.Appliers[counter], vacancy);
                        if (accepted)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
        private static bool SeeApplierCVs(Worker applier, Vacancy vacancy)
        {
            int size = applier.CVs.Count;
            if (size == 0)
            {
                StackFrame callStack = new StackFrame(1, true);
                string currentFile = new StackTrace(true).GetFrame(0).GetFileName();
                Warning.Message($"APPLIER {applier.Name.ToUpper()} {applier.Surname.ToUpper()} DOESN'T HAVE ANY CV. PLEASE, CREATE CV.");
                return false;
            }
            else
            {
                size++;
                ConsoleColor[] Set = new ConsoleColor[size];
                int counter = 0;
                while (true)
                {
                    Console.Clear();
                    Console.CursorVisible = false;
                    VisualHelper.ShowMyCVsScript();
                    for (int i = 0; i < size; i++)
                    {
                        if (i == counter)
                        {
                            Set[i] = ConsoleColor.Yellow;
                        }
                        else
                        {
                            Set[i] = ConsoleColor.White;
                        }
                    }

                    for (int x = 0; x < size - 1; x++)
                    {
                        Console.ForegroundColor = Set[x];
                        string frame = $"CV {x + 1}".FrameTheWord();
                        Console.WriteLine(frame);
                    }
                    Console.ForegroundColor = Set[size - 1];
                    Console.WriteLine("Back".FrameTheWord());

                    Console.SetCursorPosition(0, counter * 2);
                    Console.WriteLine("");

                    var key = Console.ReadKey();

                    if (key.Key == ConsoleKey.UpArrow && counter != 0)
                    {
                        counter--;
                    }
                    else if (key.Key == ConsoleKey.DownArrow && counter != size - 1)
                    {
                        counter++;
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        if (counter == size - 1)
                            break;

                        Console.Clear();
                        bool accepted = AcceptApplierOrNot(applier, applier.CVs[counter], vacancy);
                        if (accepted)
                            return true;
                        return false;
                    }
                }
                return false; // Error verirdi, error vermesin deye yazdim
            }
        }
        public static bool AcceptApplierOrNot(Worker applier, CV cv, Vacancy vacancy)
        {
            bool firsttime = true;
            int size = 3;
            ConsoleColor[] Set = new ConsoleColor[size];
            int counter = 0;
            while (true)
            {
                Console.Clear();
                Console.CursorVisible = false;
                VisualHelper.ShowApplierCVScript();
                for (int x = 0; x < size; x++)
                {
                    if (counter == x)
                    {
                        Set[x] = ConsoleColor.Green;
                    }
                    else
                    {
                        Set[x] = ConsoleColor.Yellow;
                    }
                }

                Console.WriteLine(cv);

                Console.ForegroundColor = Set[0];
                VisualHelper.ShowBackScriptOnLeft();
                Console.ForegroundColor = Set[1];
                VisualHelper.ShowDeclineWorkerScript();
                Console.ForegroundColor = Set[2];
                VisualHelper.ShowHireWorkerScript();

                if (firsttime)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("");
                    firsttime = false;
                }

                var key = Console.ReadKey();

                if (key.Key == ConsoleKey.UpArrow && counter != 0) // Up
                {
                    counter--;
                }
                else if (key.Key == ConsoleKey.DownArrow && counter != size - 1) // Down
                {
                    counter++;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    if (counter == 0)
                        return false;

                    Console.Clear();
                    Employer employer = Database.GetEmployerByVacancyID(vacancy.ID);
                    string from = $"{employer.Name} {employer.Surname}";
                    if (counter == 1)
                    {
                        Notification newNotification = new Notification($"Hello {applier.Name} {applier.Surname}, You Were Declined! (Job : {vacancy.Name})", from, DateTime.Now);
                        applier.Notifications.Add(newNotification);
                        vacancy.Appliers.Remove(applier);
                        JsonSerialization.SerializeDatabase(GlobalData.database);
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"Message Was Sent To Applier {applier.Name} {applier.Surname}.");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Applier Was Declined.");
                        Warning.PressAnyKey();
                        return false;
                    }

                    // Hire Worker
                    // Send Message To Worker
                    employer.Vacancies.Remove(vacancy);
                    Notification newNotification2 = new Notification($"Hello {applier.Name} {applier.Surname}, You Were Accepted To Job! ({vacancy.Name})", from, DateTime.Now);
                    applier.Notifications.Add(newNotification2);
                    JsonSerialization.SerializeDatabase(GlobalData.database);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"Message Was Sent To Applier {applier.Name} {applier.Surname}. Applier Was Accepted To Job.");
                    Warning.PressAnyKey();
                    return true;
                }
            }
        }
    }
}