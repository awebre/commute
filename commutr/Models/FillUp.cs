using System;
using commutr.Services;
using SQLite;

namespace commutr.Models
{
    public class FillUp : IIdentifiable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public DateTime Date { get; set; }

        public decimal FuelAmount { get; set; }
        
        public decimal PricePerFuelAmount { get; set; }
        
        public string FuelUnit { get; set; }

        [Ignore]
        public decimal Total => FuelAmount * PricePerFuelAmount;
        
        public string Notes { get; set; }
        
        public decimal Distance { get; set; }
        
        public string DistanceUnit { get; set; }

        [Ignore]
        public decimal FuelEconomy => Distance != 0 ? FuelAmount / Distance : 0;
        
        [Indexed]
        public int VehicleId { get; set; }
    }
}