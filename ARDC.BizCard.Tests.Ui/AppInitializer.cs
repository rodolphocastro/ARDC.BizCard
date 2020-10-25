using System;

using Xamarin.UITest;

namespace ARDC.BizCard.Tests.Ui
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .EnableLocalScreenshots()
                    .InstalledApp("com.ARDC.BizCard.Droid")
                    .StartApp();
            }

            throw new NotSupportedException();
        }
    }
}