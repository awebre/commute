using SimpleInjector;

namespace commutr.iOS.Configuration
{
    public static class SimpleInjectorConfiguration
    {
        public static Container Configure()
        {
            var container = new Container();
            //register ios specific implementations here
            return container;
        }
    }
}
