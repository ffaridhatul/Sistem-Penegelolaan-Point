using System;
using System.Collections.Generic;

namespace Mission1.Model
{
    public class Customer
    {
        public string Name {  get; set; }
        public string PhoneNo { get; set; }
        public int Balance { get; set; }
        public int VisitCount { get; set; }
        public DateTime LastVisitDate { get; set; }
        public List<PointRecord> PointRecords { get; set; }

        public Customer()
        {
            PointRecords = new List<PointRecord>();
        }

        public bool UsePoint(int point)
        {
            if (Balance >= point)
            {
                // Buat objek PointRecord dengan parameter yang sesuai
                var pointRecord = new PointRecord(point, PointRecordTypeEnum.Use);

                PointRecords.Add(pointRecord);

                // Kurangi Balance dan tambah VisitCount
                Balance -= point;
                VisitCount++;
                LastVisitDate = DateTime.Now;

                return true;
            }

            return false; // Jika poin tidak mencukupi
        }

        public void EarnPoint(int point)
        {
            // Buat objek PointRecord dengan parameter yang sesuai
            var pointRecord = new PointRecord(point, PointRecordTypeEnum.Earn);

            PointRecords.Add(pointRecord);

            // Tambah Balance dan VisitCount
            Balance += point;
            VisitCount++;
            LastVisitDate = DateTime.Now;
        }
    }
}
