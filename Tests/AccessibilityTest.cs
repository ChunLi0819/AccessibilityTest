using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFramework;

namespace Tests
{
    [TestClass]
    public class AccessibilityTest
    {
        #region Additional test attributes
        [ClassInitialize]
        [AssemblyInitialize]
        public static void ClassInitialize(TestContext context)
        {
            //Browser.Initialize();
            log4net.Config.XmlConfigurator.Configure();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            //Browser.Close();
        }
        #endregion

        [TestMethod]
        public void Acceptance_S18_TC01_CheckImageAltTextForAllPages()
        {
            bool isSucceed = false;
            string errorInfo = WebCheckers.ImageAltTextChecker.ProccessCheck();
            isSucceed = errorInfo == string.Empty;
            Assert.IsTrue(isSucceed, (isSucceed ? "Check alt text of image succeed for all pages." : string.Format("Check alt text of image result:\r\n{0}", errorInfo)));
        }

        [TestMethod]
        public void Acceptance_S18_TC02_CheckImageAltTextForSinglePage()
        {
            bool isSucceed = false;
            WebCheckers.CrawlConfiguration.MaxCrawlDepth = 0;
            string errorInfo = WebCheckers.ImageAltTextChecker.ProccessCheck();
            isSucceed = errorInfo == string.Empty;
            Assert.IsTrue(isSucceed, (isSucceed ? "Check alt text of image succeed for a single page." : string.Format("Check alt text of image result:\r\n{0}", errorInfo)));
        }

        [TestMethod]
        public void Acceptance_S18_TC03_CheckVideoClassForAllPages()
        {
            bool isSucceed = false;
            string errorInfo = WebCheckers.VideoClassChecker.ProccessCheck();
            isSucceed = errorInfo == string.Empty;
            Assert.IsTrue(isSucceed, (isSucceed ? "Check video class succeed for all pages." : string.Format("Check video class result:\r\n{0}", errorInfo)));
        }

        [TestMethod]
        public void Acceptance_S18_TC04_CheckVideoClassForSinglePage()
        {
            bool isSucceed = false;
            WebCheckers.CrawlConfiguration.MaxCrawlDepth = 0;
            string errorInfo = WebCheckers.VideoClassChecker.ProccessCheck();
            isSucceed = errorInfo == string.Empty;
            Assert.IsTrue(isSucceed, (isSucceed ? "Check video class succeed for a single page." : string.Format("Check video class result:\r\n{0}", errorInfo)));
        }

        /*[TestMethod]
        public void Acceptance_S18_TC05_CheckVideoTabIndexForAllPages()
        {
            bool isSucceed = false;
            string errorInfo = WebCheckers.VideoTabIndexChecker.ProccessCheck();
            isSucceed = errorInfo == string.Empty;
            Assert.IsTrue(isSucceed, (isSucceed ? "Check video tab index succeed for all pages." : string.Format("Check video tab index result:\r\n{0}", errorInfo)));
        }

        [TestMethod]
        public void Acceptance_S18_TC06_CheckVideoTabIndexForSinglePage()
        {
            bool isSucceed = false;
            WebCheckers.CrawlConfiguration.MaxCrawlDepth = 0;
            string errorInfo = WebCheckers.VideoTabIndexChecker.ProccessCheck();
            isSucceed = errorInfo == string.Empty;
            Assert.IsTrue(isSucceed, (isSucceed ? "Check video tab index succeed for a single page." : string.Format("Check video tab index result:\r\n{0}", errorInfo)));
        }*/

        [TestMethod]
        public void Acceptance_S18_TC07_CheckEmptyLinkForAllPages()
        {
            bool isSucceed = false;
            string errorInfo = WebCheckers.EmptyLinkChecker.ProccessCheck();
            isSucceed = errorInfo == string.Empty;
            Assert.IsTrue(isSucceed, (isSucceed ? "Check empty link succeed for all pages." : string.Format("Check empty link result:\r\n{0}", errorInfo)));
        }

        [TestMethod]
        public void Acceptance_S18_TC08_CheckEmptyLinkForSinglePage()
        {
            bool isSucceed = false;
            WebCheckers.CrawlConfiguration.MaxCrawlDepth = 0;
            string errorInfo = WebCheckers.EmptyLinkChecker.ProccessCheck();
            isSucceed = errorInfo == string.Empty;
            Assert.IsTrue(isSucceed, (isSucceed ? "Check empty link succeed for a single page." : string.Format("Check empty link result:\r\n{0}", errorInfo)));
        }

        [TestMethod]
        public void Acceptance_S18_TC09_CheckImageSrcForAllPages()
        {
            bool isSucceed = false;
            string errorInfo = WebCheckers.ImageSrcChecker.ProccessCheck();
            isSucceed = errorInfo == string.Empty;
            Assert.IsTrue(isSucceed, (isSucceed ? "Check image src succeed for all pages." : string.Format("Check image src result:\r\n{0}", errorInfo)));
        }

        [TestMethod]
        public void Acceptance_S18_TC10_CheckImageSrcForSinglePage()
        {
            bool isSucceed = false;
            WebCheckers.CrawlConfiguration.MaxCrawlDepth = 0;
            string errorInfo = WebCheckers.ImageSrcChecker.ProccessCheck();
            isSucceed = errorInfo == string.Empty;
            Assert.IsTrue(isSucceed, (isSucceed ? "Check image src succeed for a single page." : string.Format("Check image src result:\r\n{0}", errorInfo)));
        }

        [TestMethod]
        public void Acceptance_S18_TC11_CheckAriaLabelForAllPages()
        {
            bool isSucceed = false;
            string errorInfo = WebCheckers.AriaLabelChecker.ProccessCheck();
            isSucceed = errorInfo == string.Empty;
            Assert.IsTrue(isSucceed, (isSucceed ? "Check aria-label for link which contains image succeed for all pages." : string.Format("Check aria-label for link which contains image result:\r\n{0}", errorInfo)));
        }

        [TestMethod]
        public void Acceptance_S18_TC12_CheckAriaLabelForSinglePage()
        {
            bool isSucceed = false;
            WebCheckers.CrawlConfiguration.MaxCrawlDepth = 0;
            string errorInfo = WebCheckers.AriaLabelChecker.ProccessCheck();
            isSucceed = errorInfo == string.Empty;
            Assert.IsTrue(isSucceed, (isSucceed ? "Check aria-label for link which contains image succeed for a single page." : string.Format("Check aria-label for link which contains image result:\r\n{0}", errorInfo)));
        }
    }
}
