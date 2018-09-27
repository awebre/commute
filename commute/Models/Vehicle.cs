using System;
using commutr.Services;
using SQLite;

namespace commutr.Models
{
    public class Vehicle : IIdentifiable
    {
        public Vehicle()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }
        
        public decimal Odometer { get; set; }
        
        public string Notes { get; set; }
        
        public bool IsPrimary { get; set; }
    }
}
