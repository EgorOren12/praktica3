﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace praktica3
{
    internal class BankAccounts
    {
        private static Dictionary<string, List<BankAccounts>> clients;

        public static string file = Path.Combine("BankSave.json");

        public Account Type { get; set; }
        public int Summ { get; set; }
        public BankAccounts(Account type, int summ) 
        {
        this.Type = type;
        this.Summ = summ;
        }


        public static Dictionary<string, List<BankAccounts>> Clients 
        { 
            get
            {
                if (clients == null)
                {
                    if (File.Exists(file))
                    {
                        string jsonFile = File.ReadAllText(file);
                        clients = JsonSerializer.Deserialize<Dictionary<string, List<BankAccounts>>>(jsonFile);
                        return clients;
                    }
                    else
                    {
                        var options = new JsonSerializerOptions
                        {
                            WriteIndented = true,
                            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping //настройки зписи json формат русского языка
                        };
                        clients = ClientsGenerator();
                        string jsonStr = JsonSerializer.Serialize(clients, options);
                        File.WriteAllText(file, jsonStr);
                        return clients;
                    }
                }
                return clients;
            }            
        }

       public static Dictionary<string,List<BankAccounts>> ClientsGenerator() //Генератор
        {
            var clients = new Dictionary<string, List<BankAccounts>>();
            Random rnd = new Random();
            List<string> names = new List<string>
            {"Иван", "Максим","Дмитрий","Евгений","Александр","Сергей","Борис","Николай","Владимир","Руслан","Кирилл","Алексей","Матвей" };
            List<string> surnames = new List<string>
            {"Иванов","Романов","Головачев","Петров","Сергеев","Антипов","Леонов","Пантификов","Османов","Желтков","Чехов","Людинов","Простолюдинов","Графов" };
            List<string> patronymics = new List<string>
            {"Сергеевич","Николаевич","Александрович","Петрович","Ильич","Тимофеевич","Федорович","Данилович" };

            List<BankAccounts> CreateList()
            {
                var list = new List<BankAccounts>();
                if (rnd.Next(0,3) == 0)
                {           
                    var bankAccounts1 = new BankAccounts(Account.Debit, rnd.Next(0, 70000));
                    list.Add(bankAccounts1);
                }
                else if (rnd.Next(0, 3) == 1)
                {
                    var bankAccounts1 = new BankAccounts(Account.Debit, rnd.Next(0, 70000));
                    var bankAccounts2 = new BankAccounts(Account.Credit, rnd.Next(20000, 200000));
                    list.Add(bankAccounts1); list.Add(bankAccounts2);
                }
                else
                {
                    var bankAccounts2 = new BankAccounts(Account.Credit, rnd.Next(20000, 200000));
                    list.Add(bankAccounts2);
                }
                return list;
            }

            for (int i = 0; i < 30; i++)
            {
                List<BankAccounts> accounts = new List<BankAccounts>();
                {
                    new BankAccounts(Account.Debit, rnd.Next(1000, 200000));
                }
                clients.Add(surnames[rnd.Next(0,surnames.Count)] + " " + names[rnd.Next(0, names.Count)] + " " + patronymics[rnd.Next(0, patronymics.Count)], CreateList());
                
            }
           return clients;
        }
    }
}
