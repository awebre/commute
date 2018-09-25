using SimpleInjector;

namespace commute.Droid.Configuration
{
    public static class SimpleInjectorConfiguration
    {
        public static Container Configure()
        {
            var container = new Container();
            //register android specific implementations here
            return container;
        }
    }
}