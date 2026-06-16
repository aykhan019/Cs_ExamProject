using System;
using System.Collections.Generic;
using EntityNamespace;
using ApplicationsExceptionNamespace;
using System.Diagnostics;
using System.Collections;
using StartNamespace;

namespace EntityNamespace
{
    public class SignIN
    {
        public static ArrayList GetUser(string username, string password)
        {
            StackFrame callStack = new StackFrame(1, true);
            string currentFile = new StackTrace(true).GetFrame(0).GetFileName();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Warning.Message("ALL INFORMATIONS WERE NOT ENTERED!");
                throw new DetailedException("All Informations Were Not Entered!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
            }

            if (GlobalData.database.Workers != null)
            {
                foreach (Worker worker in GlobalData.database.Workers)
                {
                    if (worker.Username == username && worker.Password == password)
                    {
                        ArrayList arrayList = new ArrayList();
                        arrayList.Add(worker.CVs);
                        arrayList.Add(worker);
                        return arrayList;
                    }
                }
            }
            if (GlobalData.database.Employers != null)
            {
                foreach (Employer employer in GlobalData.database.Employers)
                {
                    if (employer.Username == username && employer.Password == password)
                    {
                        ArrayList arrayList = new ArrayList();
                        arrayList.Add(employer.Vacancies);
                        arrayList.Add(employer);
                        return arrayList;
                    }
                }
            }
            // Show to user
            Warning.Message("INCORRECT USERNAME OR PASSWORD! USER WAS NOT FOUND!");
            throw new DetailedException($"The user '{username}' with password '{password}' was not found!", DateTime.Now, callStack.GetFileLineNumber(), currentFile);
        }
        public static ArrayList SignIn()
        {
            while (true)
            {
                Console.Write("\n ENTER YOUR USERNAME : ");
                string username = Console.ReadLine().Trim();

                Console.Write("\n ENTER YOUR PASSWORD : ");
                string password = Console.ReadLine().Trim();

                ArrayList user = GetUser(username, password);

                return user;
            }
        }
    }
}
