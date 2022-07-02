using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Globalization;

namespace ConsoleApplication1
{
    class Program
    {
        HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            Program program = new Program();
            await program.GetTodoItems();
        }
        
        NumberFormatInfo numberFormatInfo = new NumberFormatInfo()
        {
            NumberDecimalSeparator = ".",

        };
        private async Task GetTodoItems()
        {
            string responce = await client.GetStringAsync(
               "https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5"
                );

            List<Todo> todo = JsonConvert.DeserializeObject<List<Todo>>(responce);

            // double a, b;
            
            // a = Convert.ToDouble(todo[1].buy, numberFormatInfo);
            // b = Convert.ToDouble(todo[1].sale, numberFormatInfo);
            // Console.WriteLine(a / b);
            // 0.028
            // 0.027
            // 0.001
            
            foreach (var item in todo)
            {
                Console.WriteLine(item.ccy + " - " + item.base_ccy + " " + item.buy + " - " + item.sale);
            }
        }
    }

    class Todo
    {
        public string ccy { get; set; }
        public string base_ccy { get; set; }
        public string buy { get; set; }     
        public string sale { get; set; }

    }
}