using System;
using SimpleInjector;
using commute.Services;

namespace commute.iOS.Configuration
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
