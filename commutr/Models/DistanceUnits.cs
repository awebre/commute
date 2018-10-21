using System.Collections.Generic;

namespace commutr.Models
{
    public static class DistanceUnits
    {
        public static List<string> All => new List<string> {Miles, Kilometer};

        public static string Miles => nameof(Miles);

        public static string Kilometer => nameof(Kilometer);
    }
}