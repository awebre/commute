using System.Collections.Generic;

namespace commutr.Models
{
    public static class FuelUnits
    {
        public static List<string> All => new List<string> {Gallons, Litre};

        public static string Gallons => nameof(Gallons);

        public static string Litre => nameof(Litre);
    }
}