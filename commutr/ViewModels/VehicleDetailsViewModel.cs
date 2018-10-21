using commutr.Models;

namespace commutr.ViewModels
{
    public class VehicleDetailsViewModel : BaseViewModel
    {
        public Vehicle Item { get; set; }
        public VehicleDetailsViewModel(Vehicle item = null)
        {
            Title = $"{item?.Year} {item?.Make} {item?.Model}";
            Item = item;
        }
    }
}
