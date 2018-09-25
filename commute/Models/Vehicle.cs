using System;
using commute.Services;
namespace commute.Models
{
    public class Vehicle : IIdentifiable
    {
        public Vehicle()
        {
        }

        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public DateTime Year { get; set; }
    }
}
