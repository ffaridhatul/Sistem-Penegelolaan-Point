using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mission1.Model
{
    public class FileDB
    {
        // Memuat pelanggan dari file "Customer.txt" dan mengembalikan sebagai Dictionary
        public static Dictionary<string, Customer> LoadCustomer()
        {
            using (var reader = new StreamReader(new FileStream("Customer.txt", FileMode.OpenOrCreate), Encoding.Default))
            {
                string jsonStr = reader.ReadToEnd().Trim(); // Menghapus spasi tambahan

                // Jika file kosong, return dictionary kosong
                if (string.IsNullOrEmpty(jsonStr))
                    return new Dictionary<string, Customer>();

                var settings = new JsonSerializerSettings
                {
                    // Ini memungkinkan untuk mengabaikan tambahan teks yang tidak valid
                    Error = (sender, args) =>
                    {
                        args.ErrorContext.Handled = true;
                    }
                };

                return JsonConvert.DeserializeObject<Dictionary<string, Customer>>(jsonStr, settings);
            }
        }
        // Memuat aturan dari file "Rule.txt" atau membuat aturan default jika tidak ditemukan
        public static Rule LoadRule()
        {
            using (var reader = new StreamReader(new FileStream("Rule.txt", FileMode.OpenOrCreate), Encoding.Default))
            {
                string jsonStr = reader.ReadToEnd();

                if (!string.IsNullOrEmpty(jsonStr))
                    return JsonConvert.DeserializeObject<Rule>(jsonStr);
                else
                    return new Rule(1000); // Memberikan nilai 1000 sebagai minimumUsablePoint
            }
        }

        // Menyimpan pelanggan ke dalam file "Customer.txt"
        public static void SaveCustomer(Dictionary<string, Customer> customers)
        {
            string jsonStr = JsonConvert.SerializeObject(customers);

            using (var writer = new StreamWriter(new FileStream("Customer.txt", FileMode.Open), Encoding.Default))
            {
                writer.WriteLine(jsonStr);
            }
        }

    }
}
