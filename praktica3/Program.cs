using System.Globalization;
using System.Threading.Channels;

namespace praktica3
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var clients = BankAccounts.Clients;
            while (true)
            {
                Console.WriteLine("Выбирите действие:\n1)Операции со счетом\n" +
                    "2)Операции банка");
                int a;
                while (!int.TryParse(Console.ReadLine(), out a))
                {
                    Console.WriteLine("Ошибка ввода, попробуйте снова");
                }

                switch (a)
                {
                    case 1:
                        Console.WriteLine("Введите ФИО Клиента");
                        string FIO = Console.ReadLine();
                        if (!clients.ContainsKey(FIO))
                        {
                            Console.WriteLine("Клиент не найден");
                            return;
                        }
                        Console.WriteLine($"{FIO},Счета:");
                        foreach(var acc in clients[FIO])
                        {
                            Console.WriteLine($"{acc.Type} - {acc.Summ}р");
                        }
                        Console.WriteLine("Выбирите действие:\n1)Пополнение счета\n2)Снятие со счета");
                        int c;
                        while (!int.TryParse(Console.ReadLine(), out c))
                        {
                            Console.WriteLine("Ошибка ввода, попробуйте снова");
                        }
                        switch (c)
                        {
                            case 1: BankAccounts.AddingFundsToAccount(FIO);break;
                                case 2: BankAccounts.WithdrawalFromTheAccount(FIO);break;
                            default: Console.WriteLine("Ошибка ввода"); break;
                        }
                        break;
                    case 2:
                        Console.WriteLine("Выбирите действие:\n" +
                "1. Создания клиента \n" +
                "2. Создание счета для уже существующего клиента\n" +
                "3. Вывод всех клиентов с данными по их счетам\n" +
                "4. Вывод всех счетов для конкретного клиента\n" +
                "5. Вывод суммы на всех счетах для конкретного клиента\n" +
                "6. Вывод суммы на всех счетах всех клиентов\n" +
                "7. Вывод отдельно только кредитных счетов\n" +
                "8. Вывод отдельно только дебетовых счетов");
                        int b;
                        while (!int.TryParse(Console.ReadLine(), out b))
                        {
                            Console.WriteLine("Ошибка ввода, попробуйте снова");
                        }

                        switch (b)
                        {
                            case 1: Bank.CreateClient(clients); break;
                            case 2: Bank.CreateAccountOfClient(clients); break;
                            case 3: Bank.PrintInfoOfClients(clients); break;
                            case 4: Bank.PrintInfoOfAccountsOfClient(clients); break;
                            case 5: Bank.PrintSummOfClientAccounts(clients); break;
                            case 6: Bank.PrintSummOfClients(clients); break;
                            case 7: Bank.PrintOnlyCreditAccounts(clients); break;
                            case 8: Bank.PrintOnlyDebitAccounts(clients); break;
                            default: Console.WriteLine("Ошибка ввода"); break;
                        }
                        break;
                    default: Console.WriteLine("Ошибка ввода"); break;
                }
               
                Console.ReadKey();
                Console.Clear();
            }
            }
        }
    }

