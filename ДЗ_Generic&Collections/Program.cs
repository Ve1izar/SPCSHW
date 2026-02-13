using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ДЗ_Generic_Collections
{
    // ---- Клас словника ----
    class EngUkrDictionary
    {
        private Dictionary<string, List<string>> _dictionary;

        public EngUkrDictionary()
        {
            _dictionary = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
        }

        public void AddTranslation(string englishWord, string ukrainianWord)
        {
            if (_dictionary.ContainsKey(englishWord))
            {
                if (!_dictionary[englishWord].Contains(ukrainianWord))
                {
                    _dictionary[englishWord].Add(ukrainianWord);
                    Console.WriteLine($"Додано варіант перекладу '{ukrainianWord}' до слова '{englishWord}'.");
                }
                else
                {
                    Console.WriteLine("Такий варіант перекладу вже існує.");
                }
            }
            else
            {
                _dictionary.Add(englishWord, new List<string> { ukrainianWord });
                Console.WriteLine($"Слово '{englishWord}' успішно додано.");
            }
        }

        public void RemoveWord(string englishWord)
        {
            if (_dictionary.Remove(englishWord))
            {
                Console.WriteLine($"Слово '{englishWord}' видалено зі словника.");
            }
            else
            {
                Console.WriteLine("Слово не знайдено.");
            }
        }

        public void RemoveTranslationVariant(string englishWord, string ukrVariant)
        {
            if (_dictionary.ContainsKey(englishWord))
            {
                if (_dictionary[englishWord].Remove(ukrVariant))
                {
                    Console.WriteLine($"Варіант '{ukrVariant}' видалено для слова '{englishWord}'.");
                    if (_dictionary[englishWord].Count == 0)
                    {
                        _dictionary.Remove(englishWord);
                        Console.WriteLine($"У слова '{englishWord}' не залишилось перекладів, тому його теж видалено.");
                    }
                }
                else
                {
                    Console.WriteLine($"Варіант перекладу '{ukrVariant}' не знайдено.");
                }
            }
            else
            {
                Console.WriteLine($"Слово '{englishWord}' не знайдено.");
            }
        }

        public void ChangeWord(string oldEnglishWord, string newEnglishWord)
        {
            if (_dictionary.ContainsKey(oldEnglishWord))
            {
                if (_dictionary.ContainsKey(newEnglishWord))
                {
                    Console.WriteLine($"Слово '{newEnglishWord}' вже існує. Об'єднайте їх вручну.");
                    return;
                }

                List<string> translations = _dictionary[oldEnglishWord];
                _dictionary.Remove(oldEnglishWord);
                _dictionary.Add(newEnglishWord, translations);
                Console.WriteLine($"Слово '{oldEnglishWord}' змінено на '{newEnglishWord}'.");
            }
            else
            {
                Console.WriteLine("Старе слово не знайдено.");
            }
        }

        public void ChangeTranslationVariant(string englishWord, string oldUkr, string newUkr)
        {
            if (_dictionary.ContainsKey(englishWord))
            {
                int index = _dictionary[englishWord].IndexOf(oldUkr);
                if (index != -1)
                {
                    _dictionary[englishWord][index] = newUkr;
                    Console.WriteLine("Варіант перекладу успішно змінено.");
                }
                else
                {
                    Console.WriteLine($"Переклад '{oldUkr}' не знайдено.");
                }
            }
            else
            {
                Console.WriteLine($"Слово '{englishWord}' не знайдено.");
            }
        }

        public void FindTranslation(string englishWord)
        {
            if (_dictionary.TryGetValue(englishWord, out List<string> translations))
            {
                Console.WriteLine($"Переклад слова '{englishWord}': {string.Join(", ", translations)}");
            }
            else
            {
                Console.WriteLine($"Слово '{englishWord}' не знайдено у словнику.");
            }
        }

        public void ShowAll()
        {
            Console.WriteLine("\n--- Вміст словника ---");
            if (_dictionary.Count == 0) Console.WriteLine("Словник порожній.");
            foreach (var item in _dictionary)
            {
                Console.WriteLine($"{item.Key}: {string.Join(", ", item.Value)}");
            }
            Console.WriteLine("----------------------\n");
        }
    }
    internal class Program
    {   
        // ---- Свап ----
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        

        static void Main(string[] args)
        {
            Console.WriteLine("Завдання 1");
            int x = 10, y = 20;
            Console.WriteLine($"До Swap: x={x}, y={y}");
            Swap(ref x, ref y);
            Console.WriteLine($"Після Swap: x={x}, y={y}");

            Console.WriteLine(new string('-', 20));

            string s1 = "Hello", s2 = "World";
            Console.WriteLine($"До Swap: s1={s1}, s2={s2}");
            Swap(ref s1, ref s2);
            Console.WriteLine($"Після Swap: s1={s1}, s2={s2}");

            Console.ReadKey();

            Console.WriteLine("Завдання 2");
            Console.OutputEncoding = Encoding.UTF8;
            EngUkrDictionary dict = new EngUkrDictionary();

            // Початкове наповнення для тесту
            dict.AddTranslation("run", "бігти");
            dict.AddTranslation("run", "керувати");
            dict.AddTranslation("class", "клас");

            bool running = true;
            while (running)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Знайти переклад");
                Console.WriteLine("2. Додати слово/переклад");
                Console.WriteLine("3. Змінити слово");
                Console.WriteLine("4. Змінити варіант перекладу");
                Console.WriteLine("5. Видалити слово");
                Console.WriteLine("6. Видалити варіант перекладу");
                Console.WriteLine("7. Показати весь словник");
                Console.WriteLine("0. Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Введіть слово англійською: ");
                        dict.FindTranslation(Console.ReadLine());
                        break;
                    case "2":
                        Console.Write("Введіть слово англійською: ");
                        string enAdd = Console.ReadLine();
                        Console.Write("Введіть переклад українською: ");
                        string uaAdd = Console.ReadLine();
                        dict.AddTranslation(enAdd, uaAdd);
                        break;
                    case "3":
                        Console.Write("Введіть старе слово: ");
                        string oldEn = Console.ReadLine();
                        Console.Write("Введіть нове слово: ");
                        string newEn = Console.ReadLine();
                        dict.ChangeWord(oldEn, newEn);
                        break;
                    case "4":
                        Console.Write("Введіть слово англійською: ");
                        string enCh = Console.ReadLine();
                        Console.Write("Старий переклад: ");
                        string oldUa = Console.ReadLine();
                        Console.Write("Новий переклад: ");
                        string newUa = Console.ReadLine();
                        dict.ChangeTranslationVariant(enCh, oldUa, newUa);
                        break;
                    case "5":
                        Console.Write("Введіть слово для видалення: ");
                        dict.RemoveWord(Console.ReadLine());
                        break;
                    case "6":
                        Console.Write("Введіть слово англійською: ");
                        string enRem = Console.ReadLine();
                        Console.Write("Введіть варіант перекладу для видалення: ");
                        string uaRem = Console.ReadLine();
                        dict.RemoveTranslationVariant(enRem, uaRem);
                        break;
                    case "7":
                        dict.ShowAll();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        break;
                }

            }
        }
    }
}
