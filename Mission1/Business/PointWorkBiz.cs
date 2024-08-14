using System.Collections.Generic;
using System.Linq;
using Mission1.Model;
using System;
using System.Data;


namespace Mission1.Business
{
    public class PointWorkBiz : IPointWorkBiz
    {
        private static PointWorkBiz pointWorkBiz;
        private static Dictionary<string, Customer> Customers { get; set; }
        private static Mission1.Model.Rule PointWorkRule { get; set; }

        private PointWorkBiz()
        {
            Customers = FileDB.LoadCustomer();

            if (Customers == null)
                Customers = new Dictionary<string, Customer>();
        }

        ~PointWorkBiz()
        {
            FileDB.SaveCustomer(Customers);
        }

        public static PointWorkBiz GetInstance()
        {
            if (pointWorkBiz == null)
                pointWorkBiz = new PointWorkBiz();

            return pointWorkBiz;
        }

        public List<Customer> GetCustomersSortedByLastVisitDate()
        {
            var customerValueList = Customers.Values.ToList();

            // Bubble Sort - Urutkan berdasarkan tanggal kunjungan terakhir secara menurun
            for (int i = 0; i < customerValueList.Count - 1; i++)
            {
                for (int j = 0; j < customerValueList.Count - i - 1; j++)
                {
                    if (customerValueList[j].LastVisitDate < customerValueList[j + 1].LastVisitDate)
                    {
                        // Tukar posisi
                        var temp = customerValueList[j];
                        customerValueList[j] = customerValueList[j + 1];
                        customerValueList[j + 1] = temp;
                    }
                }
            }

            return customerValueList;
        }

        public List<Customer> GetCustomersSortedByVisitCount()
        {
            var customerValueList = Customers.Values.ToList();

            // Bubble Sort - Urutkan berdasarkan jumlah kunjungan secara menurun
            for (int i = 0; i < customerValueList.Count - 1; i++)
            {
                for (int j = 0; j < customerValueList.Count - i - 1; j++)
                {
                    if (customerValueList[j].VisitCount < customerValueList[j + 1].VisitCount)
                    {
                        // Tukar posisi
                        var temp = customerValueList[j];
                        customerValueList[j] = customerValueList[j + 1];
                        customerValueList[j + 1] = temp;
                    }
                }
            }

            return customerValueList;
        }

        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer.PhoneNo, customer);
            FileDB.SaveCustomer(Customers);
        }

        public Customer GetCustomer(string phoneNo)
        {
            if (Customers.ContainsKey(phoneNo))
                return Customers[phoneNo];
            else
                return null;
        }
    }
}
