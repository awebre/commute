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

        public decimal Gallons { get; set; }
        
        public decimal PricePerGallon { get; set; }
        
        public decimal Total { get; set; }
        
        public string Notes { get; set; }
        
        public decimal Distance { get; set; }
        
        [Indexed]
        public int VehicleId { get; set; }
    }
}