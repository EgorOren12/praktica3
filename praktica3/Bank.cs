using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace praktica3
{
    /*Создать статичный класс банка со статичными методами для:
        1. Создания клиента 
        2. Создания счета для уже существующего клиента
        3. Вывода всех клиентов с данными по их счетам
        4. Вывода всех счетов для конкретного клиента
        5. Вывод суммы на всех счетах для конкретного клиента
        6. Вывод суммы на всех счетах всех клиентов
        7. Вывод отдельно только кредитных счетов
        8. Вывод отдельно только дебетовых счетов
*/
    internal static class Bank
    {
        public static void CreateClient(Dictionary<string, List<BankAccounts>> dictionary)//работает
        {
            var accounts = new List<BankAccounts>();//создаем новый список счетов
            Console.WriteLine("Введите ФИО");
            string FIO = Console.ReadLine();
            Console.WriteLine("Выберите тип счета:\n1)Дебетовый(0)\n2)Кредитный - 200000"); //через свич выбираем какой счет создать
            switch (int.Parse(Console.ReadLine()))
            {
                case 1:
                    var account = new BankAccounts(Account.Debit,0m); //через конструктор создается счет со свойством типа и баланса
                    accounts.Add(account); break; //добавляется в лист
                case 2:
                    var account1 = new BankAccounts(Account.Credit, 200000m);
                    accounts.Add(account1); break;
                default: Console.WriteLine("Ошибка ввода"); return;
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping //настройки зписи json формат русского языка
            };

            dictionary.Add(FIO, accounts); //добавляется ключ ФИО - значение Лист
            Console.WriteLine($"Клиент {FIO} создан");
            var jsonString = JsonSerializer.Serialize(dictionary,options);
            File.WriteAllText(BankAccounts.file, jsonString);
        }
        public static void CreateAccountOfClient(Dictionary<string, List<BankAccounts>> dictionary)//работает
        {
            Console.WriteLine("Введите ФИО клиента");
            string FIO = Console.ReadLine();
            if (!dictionary.ContainsKey(FIO))//проверка на существование клиента в словаре
            {
                Console.WriteLine("Клиент не найден");
                return;
            }
            List<BankAccounts> accounts = dictionary[FIO];//записываем словарный список в новый список
            Console.WriteLine("Выберите тип счета:\n1)Дебетовый(0р)\n2)Кредитный - 200000р");

            switch (int.Parse(Console.ReadLine()))//через свич выбираем какой счет создать и добавить в словарный лист
            {
                case 1:
                    var account = new BankAccounts(Account.Debit, 0);
                    accounts.Add(account); break;
                case 2:
                    var account1 = new BankAccounts(Account.Credit, 200000);
                    accounts.Add(account1); break;
                default: Console.WriteLine("Ошибка ввода"); return;
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping //настройки зписи json формат русского языка
            };
            Console.WriteLine($"Новый счет клиента {FIO} успешно создан");
            var jsonString = JsonSerializer.Serialize(dictionary, options);//обновление списка в json(новая сериализация)
            File.WriteAllText(BankAccounts.file, jsonString);
        }
        public static void PrintInfoOfClients(Dictionary<string, List<BankAccounts>> dictionary)
        {
            foreach (var key in dictionary)//перебор словаря через фореч
            {
                Console.Write($"{key.Key}; Счета:\n");
                foreach(var val in key.Value)//перебор св-в в листе
                {
                    Console.Write($"{val.Type} - {val.Summ}р\n");
                }
            }
        }//работает
        public static void PrintInfoOfAccountsOfClient(Dictionary<string, List<BankAccounts>> dictionary)
        {
            Console.WriteLine("Введите ФИО клиента");
            string FIO = Console.ReadLine();
            if (!dictionary.ContainsKey(FIO))
            {
                Console.WriteLine("Клиент не найден");
                return;
            }
            Console.WriteLine($"Счета клиента {FIO}");//перебор листа клиена по ключу
            foreach (var key in dictionary[FIO])
            {
                Console.WriteLine(key.Type);
            }
        }//работает
        public static void PrintSummOfClientAccounts(Dictionary<string, List<BankAccounts>> dictionary)
        {
            Console.WriteLine("Введите ФИО клиента");
            string FIO = Console.ReadLine();
            if (!dictionary.ContainsKey(FIO))
            {
                Console.WriteLine("Клиент не найден");
                return;
            }
            Console.WriteLine($"Сумма на каждом счету клиента {FIO}:");
            decimal count = 0;
            foreach (var key in dictionary[FIO])
            {
                count += key.Summ;
                Console.WriteLine($"{key.Type} - {key.Summ}р");
            }
            Console.WriteLine($"Общая сумма по счетам - {count}р");

        }//работает
        public static void PrintSummOfClients(Dictionary<string, List<BankAccounts>> dictionary)
        {
            decimal allCountSumm = 0;
            foreach (var key in dictionary)
            {
                Console.Write($"{key.Key}; Счета: \n");
                decimal count = 0;
                foreach (var value in key.Value)
                {
                    count += value.Summ;
                    Console.Write($"{value.Type} - {value.Summ}р ");
                }
                allCountSumm += count;
                Console.WriteLine($"\nОбщая сумма по счетам клиента - {count}р");
                Console.WriteLine();
            }
            Console.WriteLine($"Общая сумма по всем счетам клиентов - {allCountSumm}р");
        }//работает
        public static void PrintOnlyCreditAccounts(Dictionary<string, List<BankAccounts>> dictionary)
        {
            foreach (var key in dictionary)
            {
                int isCredit = 0;
                foreach (var value in key.Value)
                {
                    if (value.Type == Account.Debit)
                    {
                        isCredit = -1;
                    }
                }
                if (isCredit == 0)
                {
                    Console.WriteLine($"{key.Key}; Счета: ");
                    foreach (var value in key.Value)
                    {
                        Console.Write($"{value.Type} - {value.Summ}р \n");
                    }
                }
            }
        }//работает
        public static void PrintOnlyDebitAccounts(Dictionary<string, List<BankAccounts>> dictionary)
        {
            foreach (var key in dictionary)
            {
                int isDebit = 0;
                foreach (var value in key.Value)
                {
                    if (value.Type == Account.Credit)
                    {
                        isDebit = -1;
                    }
                }
                if (isDebit == 0)
                {
                    Console.WriteLine($"{key.Key}; Счета: ");
                    foreach (var value in key.Value)
                    {
                        Console.Write($"{value.Type} - {value.Summ}р \n");
                    }
                }
            }
        }//работает
    }
}
