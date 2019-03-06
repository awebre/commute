using commutr.Services;
using SimpleInjector;

namespace commutr.Droid.Configuration
{
    public static class SimpleInjectorConfiguration
    {
        public static Container Configure()
        {
            var container = new Container();
            //register android specific implementations here
            
            container.Register<IPlacesService>(() => new GooglePlacesService(GoogleApiHelper.AndroidApiKey));
            
            return container;
        }
    }
}