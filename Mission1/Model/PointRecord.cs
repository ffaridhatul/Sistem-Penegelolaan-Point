using System;

namespace Mission1.Model
{
    public class PointRecord
    {
        public int Amount { get; set; }            // Jumlah poin yang diperoleh atau digunakan
        public DateTime RecordDate { get; set; }   // Tanggal catatan poin dibuat
        public PointRecordTypeEnum PointRecordType { get; set; } // Jenis catatan (Earn atau Use)

        // Konstruktor untuk menginisialisasi PointRecord dengan jumlah poin dan jenis catatan
        public PointRecord(int amount, PointRecordTypeEnum recordType)
        {
            Amount = amount;
            PointRecordType = recordType;
            RecordDate = DateTime.Now; // Set tanggal saat ini sebagai RecordDate
        }
    }
}
