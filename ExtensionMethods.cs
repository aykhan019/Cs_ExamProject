using System;
using System.Diagnostics;
using ApplicationsExceptionNamespace;
using EntityNamespace;
using StartNamespace;

namespace Helper
{
    public static class Extensions
    {
        // Practise olmasi ucun extension method kimi yazdim
        public static void ShowMenu(this Employer employer)
        {
            const int size = 6;
            ConsoleColor[] Set = new ConsoleColor[size] { ConsoleColor.Yellow, ConsoleColor.Yellow, ConsoleColor.Yellow, ConsoleColor.Yellow, ConsoleColor.Yellow, ConsoleColor.Yellow };
            int counter = 0;
            while (true)
            {
                Console.CursorVisible = false;
                Console.Clear();
                VisualHelper.ShowHeadlineScriptOnLeft();
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

                Console.ForegroundColor = Set[0];
                Console.SetCursorPosition(27, 7);
                VisualHelper.ShowSeeVacanciesScript();
                Console.ForegroundColor = Set[1];
                Console.SetCursorPosition(27, 15);
                VisualHelper.ShowAddVacancyScript();
                Console.ForegroundColor = Set[2];
                Console.SetCursorPosition(27, 23);
                VisualHelper.ShowDeleteVacancyScript();
                Console.ForegroundColor = Set[3];
                VisualHelper.ShowHireWorkerScript(70, 7);
                Console.ForegroundColor = Set[4];
                VisualHelper.ShowSeeNotificationsScript(70, 15);
                Console.ForegroundColor = Set[5];
                VisualHelper.ShowBackScriptLongVersion(70, 23);

                Console.SetCursorPosition(0, 0);
                Console.WriteLine("");

                var key = Console.ReadKey();

                if (key.Key == ConsoleKey.UpArrow && counter != 0 && counter != size / 2) // Up
                {
                    counter--;
                }
                else if (key.Key == ConsoleKey.DownArrow && counter != size - 1 && counter != size / 3) // Down
                {
                    counter++;
                }
                else if (key.Key == ConsoleKey.LeftArrow && counter >= size / 2) // Left
                {
                    counter -= 3;
                }
                else if (key.Key == ConsoleKey.RightArrow && counter < size / 2) // Right
                {
                    counter += 3;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    if (counter == 0) // See Vacancies
                    {
                        Console.Clear();
                        employer.SeeVacancies();
                    }
                    else if (counter == 1) // Add Vacancy
                    {
                        try
                        {
                            Console.Clear();
                            employer.AddVacancy();
                        }
                        catch (DetailedException ex)
                        {
                            FileHelper.WriteExceptionToFile(ex);
                        }
                    }
                    else if (counter == 2) // Delete Vacancy
                    {
                        Console.Clear();
                        employer.DeleteVacancy();
                    }
                    else if (counter == 3) // Hire Worker
                    {
                        employer.HireWorker();
                    }
                    else if (counter == 4) // See Notifications
                    {
                        Console.Clear();
                        employer.SeeNotifications();
                    }
                    else if (counter == 5) // break
                    {
                        break;
                    }
                }
            }
        }
        public static void ShowMenu(this Worker worker)
        {
            const int size = 6;
            ConsoleColor[] Set = new ConsoleColor[size] { ConsoleColor.Yellow, ConsoleColor.Yellow, ConsoleColor.Yellow, ConsoleColor.Yellow, ConsoleColor.Yellow, ConsoleColor.Yellow };
            int counter = 0;
            while (true)
            {
                Console.CursorVisible = false;
                Console.Clear();
                VisualHelper.ShowHeadlineScriptOnLeft();
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

                Console.ForegroundColor = Set[0];
                Console.SetCursorPosition(27, 7);
                VisualHelper.ShowApplyForJobScript();

                Console.ForegroundColor = Set[1];
                Console.SetCursorPosition(27, 15);
                VisualHelper.ShowSeeMyCVsScript();

                Console.ForegroundColor = Set[2];
                Console.SetCursorPosition(27, 23);
                VisualHelper.ShowAddCVScript();

                Console.ForegroundColor = Set[3];
                VisualHelper.ShowSearchVacancyScript(70, 7);

                Console.ForegroundColor = Set[4];
                VisualHelper.ShowSeeNotificationsScript(70, 15);

                Console.ForegroundColor = Set[5];
                VisualHelper.ShowBackScriptLongVersion(70, 23);

                Console.SetCursorPosition(0, 0);
                Console.WriteLine("");

                var key = Console.ReadKey();

                if (key.Key == ConsoleKey.UpArrow && counter != 0 && counter != size / 2) // Up
                {
                    counter--;
                }
                else if (key.Key == ConsoleKey.DownArrow && counter != size - 1 && counter != size / 3) // Down
                {
                    counter++;
                }
                else if (key.Key == ConsoleKey.LeftArrow && counter >= size / 2) // Left
                {
                    counter -= 3;
                }
                else if (key.Key == ConsoleKey.RightArrow && counter < size / 2) // Right
                {
                    counter += 3;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    if (counter == 0) // Apply For Job // See Vacancies In A Sorted Form
                    {
                        worker.ApplyForJob(worker);
                    }
                    else if (counter == 1) // See My CVs
                    {
                        worker.SeeMyCVs();
                    }
                    else if (counter == 2) // Add CV
                    {
                        try
                        {
                            Console.Clear();
                            worker.AddCV();
                        }
                        catch (DetailedException ex)
                        {
                            FileHelper.WriteExceptionToFile(ex);
                        }
                    }
                    else if (counter == 3) // Search Vacancy
                    {
                        SearchEngineClass.SearchEngine();
                    }
                    else if (counter == 4) // Notifications
                    {
                        Console.Clear();
                        worker.SeeNotifications();
                    }
                    else if (counter == 5) // break
                    {
                        break;
                    }
                }
            }
        }
        public static void ShowVacancy(this Vacancy vacancy, Worker worker)
        {
            bool firstEntry = true;
            int size = 2;
            ConsoleColor[] Set = new ConsoleColor[size];
            int counter = 0;
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(vacancy);
                Console.CursorVisible = false;

                for (int x = 0; x < size; x++)
                {
                    if (x == counter)
                    {
                        Set[x] = ConsoleColor.Green;
                    }
                    else
                    {
                        Set[x] = ConsoleColor.Yellow;
                    }
                }

                Console.ForegroundColor = Set[0];
                VisualHelper.ShowBackScriptOnLeft();
                Console.ForegroundColor = Set[1];
                VisualHelper.ShowSendBIDScript();

                if (firstEntry)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("");
                    firstEntry = false;
                }

                var key = Console.ReadKey();

                if (key.Key == ConsoleKey.UpArrow && counter != 0) // Up
                {
                    Console.SetCursorPosition(0, 30);
                    Console.WriteLine("");
                    counter--;
                }
                else if (key.Key == ConsoleKey.DownArrow && counter != size - 1) // Down
                {
                    Console.SetCursorPosition(0, 30);
                    Console.WriteLine("");
                    counter++;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    if (counter == 0)
                        break;

                    StackFrame callStack = new StackFrame(1, true);
                    string currentFile = new StackTrace(true).GetFrame(0).GetFileName();
                    Console.Clear();
                    if (!vacancy.Appliers.Contains(worker))
                    {
                        DateTime deadlineDate;
                        DateTime.TryParse(vacancy.DeadlineDate, out deadlineDate);

                        if (DateTime.Now < deadlineDate)
                        {
                            vacancy.Appliers.Add(worker);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n  Your BID Was Successfully Added To Vacancy!");
                            // Send Message To Employer
                            Employer employer = Database.GetEmployerByVacancyID(vacancy.ID);
                            string from = $"{worker.Name} {worker.Surname}";
                            Notification notification = new Notification($"{worker.Name} {worker.Surname} Bidded For Your Vacancy With The ID {vacancy.ID} (Vacancy Name : {vacancy.Name})!",from,  DateTime.Now);
                            employer.Notifications.Add(notification);
                            JsonSerialization.SerializeDatabase(GlobalData.database);
                        }
                        else
                        {
                            Warning.Message($"YOU CANNOT BID, BECAUSE THE VACANCY HAS EXPIRED\nTODAY : {DateTime.Now.ToShortDateString().ToUpper()}\nVACANCY EXPIRED ON : {deadlineDate}");
                            throw new DetailedException($"Cannot Bid, Because The Vacancy Has Expired\n        Today : {DateTime.Now.ToShortDateString().ToUpper()}\n        Vacancy Expired On : {deadlineDate}", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
                        }
                    }
                    else
                    {
                        Warning.Message($"YOU HAVE ALREADY BIDDED THIS VACANCY!");
                        throw new DetailedException($"{worker.Username} Has Already Bidded Vacancy (Vacancy Id : {vacancy.ID})", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
                    }
                    Warning.PressAnyKey();
                    break;
                }
            }
        }
        public static string FrameTheWord(this string word)
        {
            string frame = string.Empty;
            int space = Console.BufferWidth;
            space -= 28;
            space /= 2;

            for (int x = 0; x < space; x++)
            {
                frame += " ";
            }
            frame += "╔";

            frame += "════════════════════════════";
            frame += "╗";
            frame += "\n";
            for (int x = 0; x < space; x++)
            {
                frame += " ";
            }
            frame += "║";

            int gap = (28 - word.Length) / 2;
            for (int i = 0; i < gap; i++)
            {
                frame += " ";
            }
            frame += word.ToUpper();

            if ((28 - word.Length) % 2 == 1)
            {
                gap += 1;
            }
            for (int i = 0; i < gap; i++)
            {
                frame += " ";
            }
            frame += "║";
            frame += "\n";

            for (int x = 0; x < space; x++)
            {
                frame += " ";
            }
            frame += "╚";
            frame += "════════════════════════════";
            frame += "╝";
            frame += "\n";
            return frame;
        }
    }
}
