using System;
using System.Collections.Generic;
using System.Text;
using Helper;
using StartNamespace;

namespace EntityNamespace
{
    public class SearchEngineClass
    {
        private static bool IsSuffix(string name, string word, int word_length)
        {
            int l_name = name.Length;
            int l_word = word_length;
            int counter = 0;
            name = name.ToLower();
            word = word.ToLower();
            for (int x = 0; x < l_name - l_word; x++)
            {
                counter = 0;
                for (int y = 0; y < l_word; y++)
                {
                    if (name[x + y] == word[y])
                    {
                        counter++;
                    }
                }
                if (counter == l_word)
                {
                    return true;
                }
            }
            return false;
        }
        private static bool IsPrefixLetter(string name, string word, int word_length)
        {
            int l = word_length;
            name = name.ToLower();
            word = word.ToLower();
            for (int x = 0; x < l; x++)
            {
                if (name[x] != word[x])
                {
                    return false;
                }
            }
            return true;
        }
        private static int GetCountOfVacancies()
        {
            if (GlobalData.database.Employers == null)
                return 0;

            int counter = 0;
            foreach (var employer in GlobalData.database.Employers)
            {
                counter += employer.Vacancies.Count;
            }
            return counter;
        }
        private static List<Vacancy> GetVacancies()
        {
            if (GlobalData.database.Employers == null)
                return null;

            List<Vacancy> vacancies = new List<Vacancy>();

            foreach (var employer in GlobalData.database.Employers)
            {
                if (employer.Vacancies != null)
                {
                    foreach (var vacancy in employer.Vacancies)
                    {
                        vacancies.Add(vacancy);

                    }
                }
            }
            return vacancies;

        }
        public static bool IsLetter(string letter)
        {
            letter = letter.ToLower();
            string letters = "qwertyuiopasdfghjklzxcvbnm";
            if (letters.Contains(letter))
                return true;
            return false;
        }
        public static void SearchEngine()
        {
            StringBuilder word = new StringBuilder();
            int i = 0;
            Console.Clear();
            VisualHelper.ShowSearchVacancyHeadline();
            Console.WriteLine("\n  - - - Press Escape (ESC) to leave - - -");
            Console.WriteLine("\n  Search vacancy : ");
            Console.SetCursorPosition(19 + i, 9);
            Console.CursorVisible = true;
            int size = GetCountOfVacancies();
            bool[] hasShown = new bool[size]; // For displaying in alphabetical order
            List<Vacancy> vacancies = GetVacancies();
            while (true)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                    break;

                for (int x = 0; x < size; x++)
                {
                    hasShown[x] = false;
                }

                Console.SetCursorPosition(19 + i, 9);
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (key.Key == ConsoleKey.Backspace) // Backspace
                {
                    if (i > 0)
                    {
                        word = word.Remove(word.Length - 1, 1);
                        i--;
                    }
                }
                else
                {
                    if (IsLetter(key.Key.ToString()))
                    {
                        word = word.Append(key.KeyChar.ToString());
                        i++;
                    }
                }

                Console.Clear();
                VisualHelper.ShowSearchVacancyHeadline();
                Console.WriteLine("\n  - - - Press Escape (ESC) to leave - - -");
                Console.Write("\n  Search vacancy : ");
                Console.WriteLine(word);

                int rank = 1;
                Console.WriteLine();
                for (int x = 0; x < size; x++)
                {
                    if (IsPrefixLetter(vacancies[x].Name, word.ToString(), i))
                    {
                        if (!hasShown[x])
                        {
                            Console.WriteLine($"  {rank}) {vacancies[x].Name}");
                            rank++;
                            hasShown[x] = true;
                        }
                    }
                }
                for (int x = 0; x < size; x++)
                {
                    if (IsSuffix(vacancies[x].Name, word.ToString(), i))
                    {
                        if (!hasShown[x])
                        {
                            Console.WriteLine($"  {rank}) {vacancies[x].Name}");
                            rank++;
                            hasShown[x] = true;
                        }
                    }
                }
                if (rank == 1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("  No results match your search :(");
                }
                Console.SetCursorPosition(19 + i, 9);
            }
            Console.Clear();
            Console.CursorVisible = false;
        }
    }
}
