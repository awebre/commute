using Xamarin.Forms;

namespace commutr.Helpers
{
    public static class ColorHelper
    {
        public static string ToHexString(this Color color)
        {
            var r = color.R.GetIntHexValue();
            var g = color.G.GetIntHexValue();
            var b = color.B.GetIntHexValue();
            var a = color.A.GetIntHexValue();

            return $"#{r:X2}{g:X2}{b:X2}{a:X2}";
        }

        private static int GetIntHexValue(this double value)
        {
            return (int)value * 255;
        }
    }
}