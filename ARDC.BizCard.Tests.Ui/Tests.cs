using NUnit.Framework;

using System.Linq;

using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace ARDC.BizCard.Tests.Ui
{
    [TestFixture(Platform.Android)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void LandingPage_Has_FloatingActionButton()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("fab"));            
            app.Screenshot("Landing Page has Floating Action Button");

            Assert.IsTrue(results.Any());
        }
    }
}
