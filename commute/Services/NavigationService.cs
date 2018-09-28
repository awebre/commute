using System.Threading.Tasks;
using Xamarin.Forms;

namespace commutr.Services
{
    public class NavigationService
    {
        private readonly NavigationPage rootPage;
        public NavigationService(NavigationPage rootPage)
        {
            this.rootPage = rootPage;
        }

        public async Task PushAsync<T>(T page) where T : Page => await rootPage.PushAsync(page);

        public async Task PopAsync() => await rootPage.PopAsync();
    }
}