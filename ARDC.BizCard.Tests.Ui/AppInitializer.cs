using System;

using Xamarin.UITest;
using Xamarin.UITest.Utils;

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
                    .WaitTimes(new WaitTimes())
                    .StartApp();
            }

            throw new NotSupportedException();
        }

        class WaitTimes : IWaitTimes
        {
            private static readonly TimeSpan _defaultWait = TimeSpan.FromMinutes(2);
            public TimeSpan WaitForTimeout => _defaultWait;

            public TimeSpan GestureWaitTimeout => _defaultWait;

            public TimeSpan GestureCompletionTimeout => _defaultWait;
        }
    }
}