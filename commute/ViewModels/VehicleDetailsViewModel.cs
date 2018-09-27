using commutr.Models;

namespace commutr.ViewModels
{
    public class VehicleDetailsViewModel : BaseViewModel
    {
        public Vehicle Item { get; set; }
        public VehicleDetailsViewModel(Vehicle item = null)
        {
            Title = $"{item?.Year.ToString().Substring(1, 3)} {item?.Make} {item?.Model}";
            Item = item;
        }
    }
}
