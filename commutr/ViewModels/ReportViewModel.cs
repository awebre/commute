using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using commutr.Models;
using commutr.Services;
using SkiaSharp;
using Xamarin.Forms;
using Entry = Microcharts.Entry;

namespace commutr.ViewModels
{
    public class ReportViewModel : BaseViewModel
    {
        private readonly IDataStore<FillUp> dataStore;

        public ReportViewModel(IDataStore<FillUp> dataStore)
        {
            this.dataStore = dataStore;
        }

        public int SelectedVehicleId { get; set; }

        private Dictionary<int, string> MonthColorLookup => new Dictionary<int, string>
        {
            { 1, "#c4006b"},
            { 2, "#8c00c4" },
            { 3, "#0600c4" },
            { 4, "#0082c4" },
            { 5, "#00c472" },
            { 6, "#10c400" },
            { 7, "#c4c400" },
            { 8, "#c47c00" },
            { 9, "#c44b00" },
            { 10, "#c40000" },
            { 11, "#8c2801" },
            { 12, "#848484" }
        };

        public List<Entry> GetFuelChartEntries()
        {
            var grouped = GetFillUpsByMonth();
            var entries = new List<Entry>();
            foreach (var group in grouped)
            {
                //TODO: get entries for each month starting from the CURRENT month, not the first month we have data for
                var monthlyAverage = Math.Round(group.Sum(x => x.FuelEconomy) / group.Count(), 2);
                entries.Add(new Entry((float)monthlyAverage)
                {
                    Label = DateTimeFormatInfo.CurrentInfo?.GetMonthName(group.Key),
                    ValueLabel = $"{monthlyAverage} MPG",
                    Color = SKColor.Parse(MonthColorLookup[group.Key]),
                });
            }

            return entries;
        }

        public List<Entry> GetPricePerEntries()
        {
            var grouped = GetFillUpsByMonth();

            var entries = new List<Entry>();
            foreach (var group in grouped)
            {
                var monthlyAverage = Math.Round(group.Sum(x => x.PricePerFuelAmount) / group.Count(), 3);
                entries.Add(new Entry((float)monthlyAverage)
                {
                    Label = DateTimeFormatInfo.CurrentInfo?.GetMonthName(group.Key),
                    ValueLabel = $"${monthlyAverage}",
                    Color = SKColor.Parse(MonthColorLookup[group.Key])
                });
            }

            return entries;
        }

        public List<Entry> GetTotalCostEntries()
        {
            var grouped = GetFillUpsByMonth();
            
            var entries = new List<Entry>();
            foreach (var group in grouped)
            {
                var monthlyAverage = Math.Round(group.Sum(x => x.Total) / group.Count(), 2);
                entries.Add(new Entry((float)monthlyAverage)
                {
                    Label = DateTimeFormatInfo.CurrentInfo?.GetMonthName(group.Key),
                    ValueLabel = $"${monthlyAverage}",
                    Color = SKColor.Parse(MonthColorLookup[group.Key])
                });
            }

            return entries;
        }

        public List<Entry> GetTotalMilesEntries()
        {
            var grouped = GetFillUpsByMonth();
            
            var entries = new List<Entry>();
            foreach (var group in grouped)
            {
                var monthlyMiles = group.Sum(x => x.Distance);
                entries.Add(new Entry((float)monthlyMiles)
                {
                    Label = DateTimeFormatInfo.CurrentInfo?.GetMonthName(group.Key),
                    ValueLabel = $"{monthlyMiles}",
                    Color = SKColor.Parse(MonthColorLookup[group.Key])
                });
            }

            return entries;
        }

        private IEnumerable<IGrouping<int, FillUp>> GetFillUpsByMonth()
        {
            var fillups = dataStore.GetItemsAsync().Result;

            var startDate = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month + 1, 1);
            fillups = fillups.Where(x => x.VehicleId == SelectedVehicleId && x.Date >= startDate).OrderBy(x => x.Date).ToList();

            var grouped = fillups.GroupBy(x => x.Date.Month);
            return grouped;
        }
    }
}