using System;
using System.Collections.Generic;
using commutr.Services;
using SQLite;

namespace commutr.Models
{
    public class MaintenanceLog : IIdentifiable
    {
        public MaintenanceLog()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        [Indexed]
        public int VehicleId { get; set; }
        
        public string MaintenanceType { get; set; }
        
        public decimal Mileage { get; set; }
        
        public string Notes { get; set; }
        
        public DateTime Date { get; set; }
    }

    public static class MaintenanceType
    {
        public static List<string> All => new List<string>{ OilChange, BrakeJob, TireRotation };
        
        public static string OilChange => "Oil Change";

        public static string BrakeJob => "Brake Job";

        public static string TireRotation => "Tire Rotation";
    }
}
