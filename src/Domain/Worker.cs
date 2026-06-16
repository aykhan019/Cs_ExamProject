using ApplicationsExceptionNamespace;
using Helper;
using StartNamespace;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EntityNamespace
{
    public class Worker : Human
    {
        public List<CV> CVs { get; set; } = new List<CV>();
        public Worker(string name, string surname, string username, string password, string city, string phone, int age)
            : base(name, surname, age, username, password, city, phone)
        {
        }
        public Worker(Human currentUser)
            : base(currentUser.Name, currentUser.Surname, currentUser.Age, currentUser.Username, currentUser.Password, currentUser.City, currentUser.Phone)
        {

        }
        public void ApplyForJob(Worker worker)
        {
            List<Vacancy> vacancies = new List<Vacancy>();
            foreach (var employer in GlobalData.database.Employers)
            {
                vacancies.AddRange(employer.Vacancies);
            }

            int size = vacancies.Count + 1;
            ConsoleColor[] Set = new ConsoleColor[size];
            int counter = 0;
            while (true)
            {
                //Console.SetWindowPosition(0, 6 + counter + 5);
                Console.Clear();
                Console.CursorVisible = false;
                VisualHelper.ShowVacanciesScript();
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
                    string frame = vacancies[x].Name.FrameTheWord();
                    Console.ForegroundColor = Set[x];
                    Console.WriteLine(frame);
                }
                Console.ForegroundColor = Set[size - 1];
                string backFrame = "BACK".FrameTheWord();
                Console.WriteLine(backFrame);

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

                    try
                    {
                        Console.Clear();
                        vacancies[counter].ShowVacancy(worker);
                    }
                    catch (DetailedException ex)
                    {
                        FileHelper.WriteExceptionToFile(ex);
                    }
                }
            }
        }
        public void SeeMyCVs()
        {
            try
            {
                int size = CVs.Count;
                if (size == 0)
                {
                    StackFrame callStack = new StackFrame(1, true);
                    string currentFile = new StackTrace(true).GetFrame(0).GetFileName();
                    Warning.Message("YOU DON'T HAVE ANY CV. PLEASE, CREATE CV.");
                    throw new DetailedException($"{Name} {Surname} does not have CV.", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
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
                            string cvFrame = $"CV {x + 1}".FrameTheWord();
                            Console.WriteLine(cvFrame);
                        }

                        Console.ForegroundColor = Set[size - 1];
                        string backFrame = "BACK".FrameTheWord();
                        Console.WriteLine(backFrame);

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
                            VisualHelper.ShowMyCVScript();
                            Console.WriteLine(CVs[counter]);
                            Warning.PressAnyKey();
                        }
                    }
                }
            }
            catch (DetailedException ex)
            {
                FileHelper.WriteExceptionToFile(ex);
            }
        }
        public void AddCV()
        {
            Console.ForegroundColor = ConsoleColor.White;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(0, 10);
                Console.WriteLine(@"
                                ╔════════════════════════════════════════════════════╗
                                ║                                                    ║
                                ║              DO YOU WANT TO ADD CV ?               ║  
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
                    VisualHelper.ShowAddingCVScript();

                    StackFrame callStack = new StackFrame(1, true);
                    string currentFile = new StackTrace(true).GetFrame(0).GetFileName();

                    Console.Write("\n  ENTER SPECIALITY : ");
                    string speciality = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(speciality))
                    {
                        Warning.Message("SPECIALITY WAS NOT FILLED!");
                        throw new DetailedException("While Filling CV, Speciality Was Not Filled!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
                    }

                    Console.Write("\n  ENTER SCHOOL NAME : ");
                    string school = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(school))
                    {
                        Warning.Message("SCHOOL WAS NOT FILLED!");
                        throw new DetailedException("While Filling CV, School Was Not Filled!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
                    }

                    Console.Write("\n  ENTER YOUR SCORE : ");
                    int score;
                    bool converted = int.TryParse(Console.ReadLine(), out score);

                    if (converted)
                    {
                        if (score < 0 || score > 100)
                        {
                            Warning.Message("INCORRECT SCORE WAS ENTERED! (SCORE SHOULD BE BETWEEEN 0 AND 100)");
                            throw new DetailedException("While Filling CV, Incorrect Score Was Entered!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
                        }
                    }
                    else
                    {
                        Warning.Message("INCORRECT SCORE WAS ENTERED!");
                        throw new DetailedException("While Filling CV, Incorrect Score Was Entered!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
                    }

                    Console.Write("\n  ENTER YOUR NUMBER OF SKILLS: ");
                    int numberOfSkils;
                    converted = int.TryParse(Console.ReadLine(), out numberOfSkils);
                    List<string> skills = new List<string>();
                    if (converted)
                    {
                        for (int x = 0; x < numberOfSkils; x++)
                        {
                            Console.Write($"  ENTER YOUR SKILL {x + 1} : ");
                            string skill = Console.ReadLine();
                            if (string.IsNullOrEmpty(skill))
                            {
                                Warning.Message("SKILL WAS NOT FILLED!");
                                throw new DetailedException("While Filling CV, Skill Was Not Filled!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
                            }
                            skills.Add(skill);
                        }
                    }
                    else
                    {
                        Warning.Message("INCORRECT NUMBER OF SKILLS WAS ENTERED!");
                        throw new DetailedException("While Filling CV, Incorrect Number Of Skills Was Entered!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
                    }

                    int numberOfCompanies;
                    Console.Write("\n  ENTER NUMBER OF COMPANIES YOU WORKED : ");
                    converted = int.TryParse(Console.ReadLine(), out numberOfCompanies);
                    Dictionary<string, Dictionary<string, string>> CompaniesWorked = new Dictionary<string, Dictionary<string, string>>();
                    if (converted)
                    {
                        for (int y = 0; y < numberOfCompanies; y++)
                        {
                            Console.Write($"  ENTER NAME OF COMPANY {y + 1}: ");
                            string companyName = Console.ReadLine();
                            if (string.IsNullOrEmpty(companyName))
                            {
                                Warning.Message("COMPANY NAME WAS NOT FILLED!");
                                throw new DetailedException("While Filling CV, Company Name Was Not Filled!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
                            }
                            DateTime startDate;
                            Console.Write($"  ENTER START DATE OF WORK IN {companyName.ToUpper()} : ");
                            string startDateString = Console.ReadLine();
                            converted = DateTime.TryParse(startDateString, out startDate);
                            if (converted)
                            {
                                DateTime endDate;
                                Console.Write($"  ENTER END DATE OF WORK IN {companyName.ToUpper()} : ");
                                string endDateString = Console.ReadLine();
                                converted = DateTime.TryParse(endDateString, out endDate);

                                if (converted)
                                {
                                    if (startDate < endDate)
                                    {
                                        var dates = new Dictionary<string, string>();
                                        dates.Add(startDate.ToLongDateString(), endDate.ToLongDateString()); ;
                                        CompaniesWorked.Add(companyName, dates);
                                    }
                                    else
                                    {
                                        Warning.Message("INCORRECT DATES WERE ENTERED (THE END DATE COMES BEFORE THE START DATE)!");
                                        throw new DetailedException("While Filling CV, Incorrect Dates Was Entered (The end date comes before the start date)!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
                                    }
                                }
                                else
                                {
                                    Warning.Message("INCORRECT END OF WORK WAS ENTERED!");
                                    throw new DetailedException("While Filling CV, Incorrect End Date Of Work Was Entered!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
                                }
                            }
                            else
                            {
                                Warning.Message("INCORRECT START DATE OF WORK WAS ENTERED!");
                                throw new DetailedException("While Filling CV, Incorrect Start Date Of Work Was Entered!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
                            }
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Warning.Message("INCORRECT NUMBER OF COMPANIES WAS ENTERED!");
                        throw new DetailedException("While Filling CV, Incorrect Number Of Companies Was Entered!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
                    }

                    Console.Write("\n  ENTER NUMBER OF LANGUAGES YOU KNOW : ");
                    int numberOfLanguages;
                    converted = int.TryParse(Console.ReadLine(), out numberOfLanguages);
                    Dictionary<string, string> languages = new Dictionary<string, string>();
                    if (converted)
                    {
                        for (int z = 0; z < numberOfLanguages; z++)
                        {
                            Console.Write("  ENTER LANGUAGE : ");
                            string language = Console.ReadLine();
                            if (string.IsNullOrEmpty(language))
                            {
                                Warning.Message("LANGUAGE WAS NOT FILLED!");
                                throw new DetailedException("While Filling CV, Language Was Not Filled!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
                            }
                            Console.Write("  ENTER LEVEL : ");
                            string languageLevel = Console.ReadLine();
                            if (string.IsNullOrEmpty(languageLevel))
                            {
                                Warning.Message("LANGUAGE LEVEL WAS NOT FILLED!");
                                throw new DetailedException("While Filling CV, Language Level Was Not Filled!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
                            }
                            languages.Add(language, languageLevel);
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Warning.Message("INCORRECT NUMBER OF LANGUAGES WAS ENTERED!");
                        throw new DetailedException("While Filling CV, Incorrect Number Of Languages Was Entered!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
                    }

                    Console.Write("\n  DO YOU HAVE HONOR DIPLOMA ? (YES (1) | NO (2)): ");
                    string hasHonorDiplomaString = Console.ReadLine();
                    hasHonorDiplomaString = hasHonorDiplomaString.Trim();
                    bool hasHonorDiploma = false;
                    if (hasHonorDiplomaString == "1")
                    {
                        hasHonorDiploma = true;
                    }
                    else if (hasHonorDiplomaString == "2")
                    {
                        hasHonorDiploma = false;
                    }
                    else
                    {
                        Warning.Message("INCORRECT INPUT WAS ENTERED");
                        throw new DetailedException("Incorrect Input Was Entered While Answering The Question About Honor Diploma", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
                    }

                    Console.Write("\n  ENTER YOUR GITLINK (OPTIONAL) : ");
                    string gitlink = Console.ReadLine();
                    Console.Write("\n  ENTER YOUR LINKEDIN (OPTIONAL) : ");
                    string linkedin = Console.ReadLine();

                    CV cv = new CV()
                    {
                        Speciality = speciality,
                        School = school,
                        Score = score,
                        Skills = skills,
                        CompaniesWorked = CompaniesWorked,
                        Languages = languages,
                        HasHonorsDiploma = hasHonorDiploma,
                        Gitlink = gitlink,
                        Linkedin = linkedin
                    };

                    CVs.Add(cv);
                    JsonSerialization.SerializeDatabase(GlobalData.database);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\n  CV Was Successfully Created And Added To Your CVs!");
                    Warning.PressAnyKey();
                    Console.CursorVisible = false;
                    break;
                }
            }
        }
    }
}
