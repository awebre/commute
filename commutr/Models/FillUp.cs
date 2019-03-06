using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using commutr.Services;
using PropertyChanged;
using SQLite;

namespace commutr.Models
{
    public class FillUp : IIdentifiable, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal FuelAmount { get; set; }

        public decimal PricePerFuelAmount { get; set; }

        [Ignore]
        public decimal Total => Math.Round(FuelAmount * PricePerFuelAmount, 2);

        public string Notes { get; set; }

        public decimal Distance { get; set; }

        [Ignore]
        public decimal FuelEconomy => Math.Round(FuelAmount != 0 ? Distance / FuelAmount : 0, 2);

        [Ignore]
        public string MPG => $"{FuelEconomy} MPG";

        [Indexed]
        public int VehicleId { get; set; }
    }
}