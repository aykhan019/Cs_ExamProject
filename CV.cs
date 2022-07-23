using Helper;
using System;
using System.Collections.Generic;

namespace EntityNamespace
{
    public class CV
    {
        public string Speciality { get; set; }
        public string School { get; set; } // the school graduated from
        public int Score { get; set; } // the score he or she was admitted to university with
        public List<string> Skills { get; set; } = new List<string>();
        public Dictionary<string, Dictionary<string, string>> CompaniesWorked { get; set; } = new Dictionary<string, Dictionary<string, string>>();
        public Dictionary<string, string> Languages { get; set; } = new Dictionary<string, string>();
        public bool HasHonorsDiploma { get; set; }
        public string Gitlink { get; set; }
        public string Linkedin { get; set; }
        public override string ToString()
        {
            string cv = string.Empty;
            cv += $"  Speciality         {Speciality}\n";
            cv += $"  School             {School}\n";
            cv += $"  Score              {Score}\n";

            cv += $"\n  Skills             ";
            if (Skills == null)
            {
                cv += "None";
            }
            else
            {
                bool first = true;
                foreach (var skill in Skills)
                {
                    if (first)
                    {
                        cv += $"{skill}\n";
                        first = false;
                    }
                    else
                    {
                        cv += $"                     {skill}\n";
                    }
                }
            }

            cv += $"\n  Companies          ";

            if (CompaniesWorked == null)
            {
                cv += "None";
            }
            else
            {
                bool first = true;
                foreach (var company in CompaniesWorked)
                {
                    if (first)
                    {
                        cv += $"{company.Key}\n";
                        first = false;
                    }
                    else
                    {
                        cv += $"\n                     {company.Key}\n";
                    }
                    foreach (var date in company.Value)
                    {
                        DateTime startTime;
                        DateTime endTime;
                        var a = DateTime.TryParse(date.Key, out startTime);
                        var b = DateTime.TryParse(date.Value, out endTime);

                        var experience = endTime - startTime;
                        float years = (float)(experience.TotalDays / 365);
                        string result = years.ToString("f1");

                        cv += $"                     Start Date : {date.Key}\n";
                        cv += $"                     Finish Date : {date.Value}\n";
                        cv += $"                     Experience : {result} years\n";
                    }
                }
            }

            cv += $"\n  Languages          ";
            if (Languages == null)
            {
                cv += "None";
            }
            else
            {
                bool first = true;
                foreach (var language in Languages)
                {
                    if (first)
                    {
                        cv += $"{language.Key} - {language.Value}\n";
                        first = false;
                    }
                    else
                    {
                        cv += $"                     {language.Key} - {language.Value}\n";
                    }
                }
            }

            cv += "\n  Has Honor Diploma  ";
            if (HasHonorsDiploma)
            {
                cv += "Yes\n";
            }
            else
            {
                cv += "No\n";
            }

            cv += $"  Git Link           ";
            if (string.IsNullOrEmpty(Gitlink))
            {
                cv += "-\n";
            }
            else
            {
                cv += $"{Gitlink}\n";
            }

            cv += $"  Linkedin           ";
            if (string.IsNullOrEmpty(Linkedin))
            {
                cv += "-\n";
            }
            else
            {
                cv += $"{Linkedin}\n";
            }

            return cv;
        }
    }
}

