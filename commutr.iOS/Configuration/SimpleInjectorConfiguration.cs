using commutr.Services;
using SimpleInjector;

namespace commutr.iOS.Configuration
{
    public static class SimpleInjectorConfiguration
    {
        public static Container Configure()
        {
            var container = new Container();
            //register ios specific implementations here
            container.Register<IPlacesService>(() => new GooglePlacesService(GoogleApiHelper.iOSApiKey));
            return container;
        }
    }
}
