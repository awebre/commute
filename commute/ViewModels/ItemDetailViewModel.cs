using commutr.Models;

namespace commutr.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Vehicle Item { get; set; }
        public ItemDetailViewModel(Vehicle item = null)
        {
            Title = item?.Make;
            Item = item;
        }
    }
}
