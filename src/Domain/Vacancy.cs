using System;
using System.Collections.Generic;

namespace EntityNamespace
{
    public class Vacancy
    {
        public static int StaticID { get; set; } = 1;
        public int ID { get; set; }
        public int Salary { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string AgeRange { get; set; }
        public string Education { get; set; }
        public string WorkExperience { get; set; }
        public string VacancyDate { get; set; }
        public string DeadlineDate { get; set; }
        public string RelevantPerson { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string JobDescription { get; set; }
        public string Requirements { get; set; }
        public List<Worker> Appliers { get; set; } = new List<Worker>();    
        public Vacancy(int salary, string name, string city, string ageRange, string education, string workExperience, string vacancyDate, string deadlineDate,
                       string relevantPerson, string phone, string email, string jobDescription, string requirements)
        {
            ID = StaticID++;
            Salary = salary;
            Name = name;
            City = city;
            AgeRange = ageRange;
            Education = education;
            WorkExperience = workExperience;
            VacancyDate = vacancyDate;
            DeadlineDate = deadlineDate;
            RelevantPerson = relevantPerson;
            Phone = phone;
            Email = email;
            JobDescription = jobDescription.Replace("- ", "  - ");
            Requirements = requirements.Replace("- ", "  - ");
        }
        public string ShowVacancyDetails()
        {
            return $@"════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════
  {Name}
  {Salary} $
  
  Region         {City}
  Age            {AgeRange}
  Education      {Education}
  Experience     {WorkExperience}
  Published On   {VacancyDate}
  Expired On     {DeadlineDate}
  Contact        {RelevantPerson}
  Phone          {Phone}
  Email          {Email}";
        }
        public override string ToString()
        {
            if (!JobDescription.Trim().StartsWith("-"))
            {
                string a = "\n  - ";
                a += JobDescription;
                JobDescription = a;
            }
            if (!Requirements.Trim().StartsWith("-"))
            {
                string a = "\n  - ";
                a += Requirements;
                Requirements = a;
            }

            return $@"════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════
  {Name}
  {Salary} $
  
  Region         {City}
  Age            {AgeRange}
  Education      {Education}
  Experience     {WorkExperience}
  Published On   {VacancyDate}
  Expired On     {DeadlineDate}
  Contact        {RelevantPerson}
  Phone          {Phone}
  Email          {Email}
  
  Job Description
 {JobDescription}
 

  Requirements
 {Requirements}


  Applier Count : {Appliers.Count}

════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

";
        }
    }
}
