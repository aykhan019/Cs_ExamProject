using StartNamespace;
using System.Collections.Generic;

namespace EntityNamespace
{
    public class Database
    {
        public List<Worker> Workers { get; set; } = new List<Worker>();
        public List<Employer> Employers { get; set; } = new List<Employer>();
        public Database(List<Worker> workers, List<Employer> employers)
        {
            Workers = workers;
            Employers = employers;
        }
        public Database()
        {

        }
        public static Employer GetEmployerByVacancyID(int vacancyId)
        {
            foreach (var employer in GlobalData.database.Employers)
            {
                foreach (var vacancy in employer.Vacancies)
                {
                    if (vacancy.ID == vacancyId)
                    {
                        return employer;
                    }
                }
            }
            return null;
        }
    }
}
