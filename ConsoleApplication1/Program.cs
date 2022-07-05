using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Globalization;

namespace ConsoleApplication1
{
    enum Currency
    {
        USD = 0,
        EUR,
        BIT
    };
    NumberFormatInfo numberFormatInfo = new NumberFormatInfo()
    {
        NumberDecimalSeparator = ".",
    };
    
     class Money 
     {
         static HttpClient client = new HttpClient();
         static string responce = client.GetStringAsync(
                "https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5"
            ).ToString();

         static List<Todo> todo = JsonConvert.DeserializeObject<List<Todo>>(responce);
            
         private double item;
         private double usd = Convert.ToDouble(todo[(int)Currency.USD].sale, NumberFormatInfo);
         private double uah = 1;
         private double eur = Convert.ToDouble(todo[(int)Currency.EUR].sale, NumberFormatInfo);
         private double bit = Convert.ToDouble(todo[(int)Currency.BIT].sale, NumberFormatInfo);
         private double k;
         private string currency;
         private string currency1;
         public Money(string currency,string currency1,double item)
         {
             this.currency = currency; 
             this.currency1 = currency1;
             this.item = item;
         } 
         public void Transfer()
         {
             double res = 0 ;
             if(currency == "USD" && currency1 == "UAH")
             {
                 k = usd;
             }
             else if (currency == "USD" && currency1 == "EUR")
             { 
                 k = usd / eur;
             }
             else if (currency == "USD" && currency1 == "BIT")
                {
                    k = usd / bit;
                }
                else if (currency == "UAH" && currency1 == "USD")
                {
                     k = uah / usd;
                }
                else if (currency == "UAH" && currency1 == "EUR")
                {
                    k = uah / eur;
                }
                else if (currency == "UAH" && currency1 == "BIT")
                {
                    double dollar;
                    dollar = uah / usd;
                    k = dollar / bit;                                                                                                                               
                }
                else if (currency == "EUR" && currency1 == "UAH")
                {
                    k = eur;
                }
                else if (currency == "EUR" && currency1 == "USD")
                {   
                    k = eur / usd;
                } 
                else if (currency == "EUR" && currency1 == "BIT")
                {
                    double dollar;
                    dollar = eur / usd;
                    k = dollar / bit;
                }
                else if (currency == "BIT" && currency1 == "USD")
                {
                    k = bit;
                }
                else if (currency == "BIT" && currency1 == "EUR")
                {
                    double dollar;
                    dollar = eur / usd;
                    k = bit / dollar;
                }
                else if (currency == "BIT" && currency1 == "UAH")
                {
                    double dollar;
                    dollar = uah / usd;
                    k = dollar / bit;
                }
                res = item * k;
                Console.WriteLine(res);
            }
            public void Print()
            {
                Console.Write(currency + " = ");
                Console.WriteLine(item);
                Console.Write(currency1 + " = ");
                Transfer();
            }
        }
    class Todo
    {
        public string ccy { get; set; }
        public string base_ccy { get; set; }
        public string buy { get; set; }     
        public string sale { get; set; }
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            double item;
            string cur, cur1;
            Console.WriteLine("Enter the value of first currensy: ");
            cur = Console.ReadLine();
            Console.WriteLine("Enter the value of second currensy: ");
            cur1 = Console.ReadLine();
            Console.WriteLine("Enter the value of money: ");
            item = Convert.ToDouble(Console.ReadLine());
            Money mon = new Money(cur, cur1, item);
            mon.Print();
        }
    }
}   