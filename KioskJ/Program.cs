using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KioskJ
{
    public class Kiosk
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; } // "журнал" або "газета"

        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }

        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }
    }

    class Program
    {

        static void Main(string[] args)
        {
            string jsonFilePath = @"C:\Programming\.vs\Programming\v17\lab2\KioskJson\KioskJ\KioskJ\KioskList.json";

            // Створення та заповнення списку даних
            var kioskList = new List<Kiosk>
            {
                new Kiosk { Name = "Newspaper A", Type = "газета", Quantity = 34, Price = 17 },
                new Kiosk { Name = "Magazine A", Type = "журнал", Quantity = 14, Price = 40 },
                new Kiosk { Name = "Newspaper B", Type = "газета", Quantity = 33, Price = 15 },
                new Kiosk { Name = "Magazine B", Type = "журнал", Quantity = 10, Price = 20 },
                new Kiosk { Name = "Magazine C", Type = "журнал", Quantity = 5, Price = 30 }
            };

            // Запис даних у форматі JSON
            string jsonOutput = JsonConvert.SerializeObject(kioskList, Formatting.Indented);
            File.WriteAllText(jsonFilePath, jsonOutput);

            Console.WriteLine("Файл Kiosk.json створено.");

            // Читання та вивід даних з JSON-файлу
            var readItems = JsonConvert.DeserializeObject<List<Kiosk>>(File.ReadAllText(jsonFilePath));
            Console.WriteLine("\nВиведення даних з Kiosk.json:");
            foreach (var item in readItems)
            {
                Console.WriteLine($"Назва: {item.Name}, Тип: {item.Type}, Кількість: {item.Quantity}, Ціна: {item.Price}");
            }

            // Визначення загальної вартості усіх газет
            decimal totalCostOfNewspapers = readItems
                .Where(item => item.Type == "газета")
                .Sum(item => item.Price * item.Quantity);

            Console.WriteLine($"\nЗагальна вартість усіх газет: {totalCostOfNewspapers} грн.");

            // Визначення кількості журналів, ціна яких лежить від X грн. до Y грн.
            decimal x = 20m; // Мінімальна ціна
            decimal y = 40m; // Максимальна ціна

            int countOfMagazinesInRange = readItems
                .Count(item => item.Type == "журнал" && item.Price >= x && item.Price <= y);

            Console.WriteLine($"Кількість журналів, ціна яких від {x} грн. до {y} грн.: {countOfMagazinesInRange}");
            Console.ReadLine();
        }
        

        }
}


