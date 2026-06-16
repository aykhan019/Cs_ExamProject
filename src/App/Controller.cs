using System;
using System.Collections;
using System.Collections.Generic;
using ApplicationsExceptionNamespace;
using EntityNamespace;
using Helper;

namespace StartNamespace
{
    public class Controller
    {
        public static ArrayList GetUser()
        {
            int counter = 0;
            const int size = 2;
            ConsoleColor[] Set = new ConsoleColor[size] { ConsoleColor.Yellow, ConsoleColor.Yellow };
            Console.CursorVisible = false;

            while (true)
            {
                Console.Clear();
                Console.ResetColor();
                for (int i = 0; i < size; i++)
                {
                    if (i == counter)
                        Set[i] = ConsoleColor.Red;
                    else
                        Set[i] = ConsoleColor.Yellow;
                }

                VisualHelper.ShowHeadlineScriptOnMiddle();
                Console.ForegroundColor = Set[0];
                VisualHelper.ShowSignInScript();
                Console.ForegroundColor = Set[1];
                VisualHelper.ShowSignUpScript();

                Console.SetCursorPosition(0, 0);
                Console.WriteLine("");

                ConsoleKeyInfo key = Console.ReadKey();

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
                    Console.Clear();
                    Console.CursorVisible = true;
                    if (counter == 0) // Sign In
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        VisualHelper.ShowSignInHeadline();
                        try
                        {
                            ArrayList arrayList = SignIN.SignIn();

                            if (arrayList != null)
                            {
                                return arrayList;
                            }
                        }
                        catch (DetailedException ex)
                        {
                            FileHelper.WriteExceptionToFile(ex);
                        }
                        VisualHelper.ShowSignUpHeadline();
                    }
                    else if (counter == 1) // Sign Up
                    {
                        int counter2 = 0;
                        const int size2 = 3;
                        ConsoleColor[] Set2 = new ConsoleColor[size2] { ConsoleColor.Yellow, ConsoleColor.Yellow, ConsoleColor.Yellow };
                        while (true)
                        {
                            Console.CursorVisible = false;
                            Console.Clear();
                            Console.ResetColor();
                            for (int i = 0; i < size2; i++)
                            {
                                if (i == counter2)
                                    Set2[i] = ConsoleColor.Red;
                                else
                                    Set2[i] = ConsoleColor.Yellow;
                            }

                            VisualHelper.ShowHeadlineScriptOnMiddle();
                            Console.ForegroundColor = Set2[0];
                            VisualHelper.ShowWorkerScript();
                            Console.ForegroundColor = Set2[1];
                            VisualHelper.ShowEmployerScript();
                            Console.ForegroundColor = Set2[2];
                            VisualHelper.ShowBackScript();

                            Console.SetCursorPosition(0, 0);
                            Console.WriteLine("");

                            ConsoleKeyInfo key2 = Console.ReadKey();

                            if (key2.Key == ConsoleKey.UpArrow && counter2 != 0)
                            {
                                counter2--;
                            }
                            else if (key2.Key == ConsoleKey.DownArrow && counter2 != size2 - 1)
                            {
                                counter2++;
                            }
                            else if (key2.Key == ConsoleKey.Enter)
                            {
                                if (counter2 == 2)
                                    break;

                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                VisualHelper.ShowSignUpHeadline();
                                try
                                {
                                    Human currentUser = SignUP.SignUp();
                                    if (currentUser != null)
                                    {
                                        ArrayList arrayList = new ArrayList();
                                        if (counter2 == 1) // Is Employer
                                        {
                                            arrayList.Add(true);
                                            Employer employer = new Employer(currentUser);
                                            GlobalData.database.Employers.Add(employer);
                                        }
                                        else // Is Worker
                                        {
                                            arrayList.Add(false); 
                                            Worker worker = new Worker(currentUser);
                                            GlobalData.database.Workers.Add(worker);
                                        }
                                        arrayList.Add(currentUser);
                                        return arrayList;
                                    }
                                }
                                catch (DetailedException ex)
                                {
                                    FileHelper.WriteExceptionToFile(ex);
                                }
                                VisualHelper.ShowSignUpHeadline();
                            }
                        }
                    }
                    Console.CursorVisible = false;
                }
            }
        }

        public static void StartProgram()
        {
            Console.Title = "Boss.az";
            InitializeObjects();

            while (true)
            {
                try
                {
                    ArrayList arrayList = GetUser();

                    if (arrayList[0] is List<Vacancy>)
                    {
                        Employer currentEmployer = (Employer)arrayList[1];
                        currentEmployer.Vacancies = (List<Vacancy>)arrayList[0];
                        currentEmployer.ShowMenu();
                    }
                    else if (arrayList[0] is List<CV>)
                    {
                        Worker currentWorker = (Worker)arrayList[1];
                        currentWorker.CVs = (List<CV>)arrayList[0];
                        currentWorker.ShowMenu();
                    }
                    else if ((bool)arrayList[0])
                    {
                        Employer currentEmployer = new Employer((Human)arrayList[1]);
                        currentEmployer.ShowMenu();
                    }
                    else
                    {
                        Worker currentWorker = new Worker((Human)arrayList[1]);
                        currentWorker.ShowMenu();
                    }
                }
                catch (Exception ex)
                {
                    Warning.Message(ex.Message);
                    FileHelper.WriteExceptionToFile(ex);
                }
            }
        }

        private static void InitializeObjects()
        {
            CV cv1 = new CV
            {
                Speciality = "Programmer",
                Score = 97,
                School = "Landau School",
                CompaniesWorked = new Dictionary<string, Dictionary<string, string>>()
                {
                    {"Kapital Bank",
                        new Dictionary<string, string>()
                        {
                            {
                                DateTime.Now.AddYears(-5).ToLongDateString(),
                                DateTime.Now.AddYears(-2).AddDays(-234).ToLongDateString()
                            }
                        }
                    },

                    {"Turan Bank",
                        new Dictionary<string, string>()
                        {
                            {
                                DateTime.Now.AddYears(-10).AddDays(213).ToLongDateString(),
                                DateTime.Now.AddYears(-6).AddDays(-234).ToLongDateString()
                            }
                        }
                    },
                },
                Gitlink = "https://github.com/Drongo-J/Battleship_Cpp",
                Linkedin = "-",
                HasHonorsDiploma = true,
                Languages = new Dictionary<string, string>()
                {
                    { "English", "A"},
                    { "Russian", "B+"}
                },
                Skills = new List<string>()
                {
                    "C#",
                    "C++",
                    "Python"
                }
            };

            CV cv2 = new CV()
            {
                Speciality = "IT",
                Score = 97,
                School = "Oxford School",
                CompaniesWorked = new Dictionary<string, Dictionary<string, string>>()
                {
                    {"Oxford School",
                        new Dictionary<string, string>()
                        {
                            {
                                DateTime.Now.AddYears(-10).ToLongDateString(),
                                DateTime.Now.AddDays(-1).AddDays(-234).ToLongDateString()
                            }
                        }
                    },
                },

                Gitlink = "https://github.com/Drongo-J/FinalProject_Cpp_OOP",
                Linkedin = "https://www.linkedin.com/learning/search?trk=homepage-basic_intent-module-learning",
                HasHonorsDiploma = true,
                Languages = new Dictionary<string, string>()
                {
                    { "English", "C"},
                    { "Russian", "A+"},
                    { "Spanish", "C+"}
                },
                Skills = new List<string>()
                {
                    "Python"
                }
            };

            CV cv3 = new CV()
            {
                Speciality = "IT",
                Score = 97,
                School = "Landau School",
                CompaniesWorked = new Dictionary<string, Dictionary<string, string>>()
                {
                    {"Ata Bank",
                        new Dictionary<string, string>()
                        {
                            {
                                DateTime.Now.AddYears(-6).ToLongDateString(),
                                DateTime.Now.AddYears(-5).AddDays(-234).ToLongDateString()
                            }
                        }
                    },

                    {"Beynelxalq Bank",
                        new Dictionary<string, string>()
                        {
                            {
                                DateTime.Now.AddYears(-4).AddDays(-2).ToLongDateString(),
                                DateTime.Now.AddYears(-1).AddDays(213).ToLongDateString()
                            }
                        }
                    },
                },
                Gitlink = "https://github.com/Drongo-J/Product_Stock_Project_Cpp",
                Linkedin = "https://www.linkedin.com/pub/dir/+/+?trk=homepage-basic_guest_nav_menu_people",
                HasHonorsDiploma = true,
                Languages = new Dictionary<string, string>()
                {
                    { "Spanish", "C"},
                    { "Russian", "B"},
                    { "English", "A+"}
                },
                Skills = new List<string>()
                {
                    "Java",
                    "C++",
                    "C#",
                    "Python"
                }
            };

            Worker w1 = new Worker("John", "Johnlu", "john123", "123456", "New-York", "(099) 999 99 99", 25);
            w1.CVs.Add(cv1);
            w1.CVs.Add(cv3);

            Worker w2 = new Worker("Rafig", "Rafigli", "rafig123", "1234abc", "Baku", "(055) 555 55 55", 27);
            w2.CVs.Add(cv2);

            Vacancy v1 = new Vacancy(600, "Waitress", "Baku", "18-40", "Secondary", "1 year", "17 August, 2022", "23 September, 2022", "Anar", "(070) 286 - 04 - 04", "mirzeyevanar10012@gmail.com", "\n- 8 saatlıq iş rejimi \n- Dəyişən iş qrafiki\n- İş saatında yemək verilir\n- Istirahet gunu heftede 1 defe", "\n- Gülərüzlü olmaq\n- Şirkətin daxili qaydalarına riayət etmək\n- Qonaqlara yüksək səviyyləli xidmət göstərmək\n- Ən azından sifariş qəbul edəcək və xidmət göstərəcək səviyyədə ingilis dili biliyi");
            Vacancy v2 = new Vacancy(900, "Site administrator", "Baku", "24 - 50 years", "Higher", "from 3 to 5 years", "July 07, 2022", "August 06, 2022", "Vefa Qaragözlü", "(051) 207 - 23 - 24", "office@mgc.az", "\n- Management of the company's site/sites\n- Entering and updating information on the company's site/sites\n- To monitor the technical support and proper functioning of the existing site\n- Improvement of the existing site\n- Troubleshooting software errors\n- Performing other IT tasks", "\n- Technical higher education (education in the field of IT is desirable)\n- Must know JAVA PHP, Html5, WordPress, SSL, Figma, Cpanel\n- SQL knowledge\n- Web programming and session management\n- Understand processes and methodologies in existing software\n- Ability to analyze and work with pre-written codes");
            Vacancy v3 = new Vacancy(1200, "IT Engineer", "Baku", "23 - 35 years", "Higher", "from 1 to 3 years", "June 29, 2022", "July 29, 2022", "Metanet x.", "(070) 707-47-37", "info@dataline.az", "\n- Management of the company's site/sites\n- Entering and updating information on the company's site/sites\n- To monitor the technical support and proper functioning of the existing site\n- Improvement of the existing site\n- Troubleshooting software errors\n- Performing other IT tasks", "\n- High education\n- At least 1 year of work experience in the field of information technologies\n- The ability to apply innovations in one's work\n- Having appropriate knowledge of foreign languages\n- Responsible approach to work, ability to communicate");
            Vacancy v4 = new Vacancy(670, "Operator", "Baku", "18 - 24 years", "Higher", "from 1 to 3 years", "July 19, 2022", "August 18, 2022", "Aysel x", "(055) 200-55-77", "info@safari-group.az", "\n- Communication with drivers by phone and social networks\n- Processing complaints and requests and feedback to drivers\n- Respond to all incoming calls in a timely and efficient manner\n- Accurate and correct transmission of information related to the service and provision of technical support\n- Timely and accurate performance of tasks given by management", "\n- Should be fluent in Azerbaijani language\n- Ability to speak fluently and with clear intonation\n- Ability to communicate and listen on the phone\n- Ability to clearly express your opinion and presentatio\n- Fulfilling duties on time\n- Business and goal-oriented, ability to work in a team\n- Keeping data confidential in the work process\n- Neat, punctual, good looking\n- Knowledge of office programs: Word, Excel\n- Work experience in this field is a must");
            Vacancy v5 = new Vacancy(950, "Videographer-Photographer", "Baku", "18 - 30 years", "Secondary", "from 1 to 3 years", "July 19, 2022", "August 18, 2022", "Mehtiyeva Fidan", "(012) 594-27-35", "zn@jaluz.az", "\n- Working schedule \n- weekdays from 10:00 to 18:00\n- Making commercials, promo videos\n- Making long and short videos for social networks\n- Ability to work with a team\n- To put forward proposals and initiatives in order to increase the efficiency of the agency's work in the creative field\n- Software knowledge for video editing: Adobe Premiere, After Effects", "\n- Gender: Male\n- Knowing graphic programs\n- Motion animation (preferred)\n- Knowledge of Premiere, Photoshop, After Effects\n- At least 1 year of work experience in the relevant field\n- Creative and innovative thinking\n- Ability to work in a team");
            Vacancy v6 = new Vacancy(550, "Seller", "Baku", "20 - 35 years", "Secondary", "from 1 to 3 years", "July 14, 2022", "August 13, 2022", "Yegane x.", "(055) 478-55-13", "profi-yar@mail.ru", "\n- Saleswomen are required for Dalga market, located near Metro Insaatchilar\n- Work experience is a must\n- Working hours 9 hours, once a week off spring from the store\n- Salary between 350-400\n- Contact for more details", "\n- Must have experience in a grocery store");
            Vacancy v7 = new Vacancy(600, "Taxi Driver", "Baku", "25 - 50 years", "None", "from 1 to 3 years", "June 29, 2022", "July 29, 2022", "Elçin m", "(055) 950-10-01", "1taximmc@gmail.com", "\n- Our drivers will be given a Toyota Prius car\n- Those who want to join with their own car can also write. We offer them cooperation on favorable terms\n- The income varies between 20-40 manats per day depending on the driver's job\n- In addition, there are weekly and monthly bonuses and gifts\n- The driver is provided with a company number", "\n- Male candidate\n- 25-50 years old\n- BC category driver's license\n- At least 2-3 years of experience as a driver (taxi drivers are preferred)\n- Getting to know the city of Baku normally\n- Responsible, accurate, diligent, eager to win");
            Vacancy v8 = new Vacancy(1000, "Front-end Programmer", "Baku", "18 - 40 years", "None", "from 3 to 5 years", "July 19, 2022", "August 18, 2022", "Bilal Sadiqov", "(012) 310-26-27", "hr@optima.az", "\n- Working hours: 5 days a week, from 09:00 to 18:00\n- Workplace: 56 Ahmet Rajabli, Aynali Plaza, 6th floor\n- The salary will be determined according to the knowledge and skills of the candidate\n- Applications are accepted in the form of a CV.", "\n- HTML, CSS\n- Javascript\n- React.js\n- Redux, Redux-toolkit\n- Axios, Rest API, WebSocket\n- Ssss, less");
            Vacancy v9 = new Vacancy(870, "Registrar", "Baku", "18 - 50 years", "Secondary", "from 1 to 3 years", "July 19, 2022", "August 18, 2022", "Secda x.", "(055) 310-53-53", "yelloservice1@gmail.com", "\n- Salcano 'Registrar for the Baku depot department of MMC' is required. active vacancy \n- Working schedule: from 09:00 to 17:00\n- Saturday \n- Sunday rest", "\n- Madam\n- Age 18-50\n- Secondary education\n- Handwriting skills are a must\n- Written and oral communication skills in Azerbaijani are a must\n- Punctual\n- Responsible\n- Able to adapt to the collective");
            Vacancy v10 = new Vacancy(1500, "IT Engineer", "Baku", "25 - 35 years", "Higher", "from 1 to 3 years", "June 22, 2022", "July 22, 2022", "Rafiq m.", "(051) 232-83-74", "hr@konsis.az", "\n- Konsis company is recruiting an IT specialist with knowledge and experience in this field\n- Office work\n- IT specialist \n- A technical support representative who handles inquiries from product users or other support engineers. Depending on the type of application, this specialist resolves the issue himself or forwards it to colleagues for review\n- Excellent opportunities for professional development and self-realization", "\n- Ability to investigate and analyze system-wide problems\n- Knowledge of network and Windows operating system\n- Solving problems on the spot\n- Prepare technical documentation and report to management\n- Knowledge of Azerbaijani and Russian languages");

            Employer e1 = new Employer("Cavid", "Eliyev", "cavid123", "111222333", "Berlin", "(011) 111 11 11", 34);
            e1.Vacancies.Add(v1);
            e1.Vacancies.Add(v2);
            e1.Vacancies.Add(v3);
            e1.Vacancies.Add(v4);
            e1.Vacancies.Add(v5);

            Employer e2 = new Employer("Rza", "Asadli", "rza000", "010101", "London", "(066) 666 66 66", 44);
            e2.Vacancies.Add(v6);
            e2.Vacancies.Add(v7);
            e2.Vacancies.Add(v8);
            e2.Vacancies.Add(v9);
            e2.Vacancies.Add(v10);

            GlobalData.database.Employers.Add(e1);
            GlobalData.database.Employers.Add(e2);

            GlobalData.database.Workers.Add(w1);
            GlobalData.database.Workers.Add(w2);

            JsonSerialization.SerializeDatabase(GlobalData.database);
        }
    }
    public static class GlobalData 
    {
        public static Database database = new Database(); 
    };

}
