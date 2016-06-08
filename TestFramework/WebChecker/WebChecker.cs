using TestFramework.Abot.Core;
using TestFramework.Abot.Crawler;
using TestFramework.Abot.Poco;
using TestFramework.Abot.Util;
using HtmlAgilityPack;
using log4net;
using System;

namespace TestFramework.WebChecker
{
    public class WebChecker : PoliteWebCrawler
    {
        private static string errorInfo = string.Empty;
        protected static ILog _logger = LogManager.GetLogger("WebCheckerLogger");

        public WebChecker()
            : base()
        {
        }

        public WebChecker(CrawlConfiguration crawlConfiguration)
            : base(crawlConfiguration, null, null, null, null, null, null, null, null)
        {
        }

        public WebChecker(CrawlConfiguration crawlConfiguration, ICrawlDecisionMaker crawlDecisionMaker, IThreadManager threadManager, IScheduler scheduler, IPageRequester pageRequester, IHyperLinkParser hyperLinkParser, IMemoryManager memoryManager, IDomainRateLimiter domainRateLimiter, IRobotsDotTextFinder robotsDotTextFinder)
            : base(crawlConfiguration, crawlDecisionMaker, threadManager, scheduler, pageRequester, hyperLinkParser, memoryManager, domainRateLimiter, robotsDotTextFinder)
        {
        }

        private string DefaultAddress
        {
            get
            {
                string address = string.Empty;
                Utility.GetConfigurationValue(_crawlContext.CrawlConfiguration, "BaseAddress", out address);
                return address.EndsWith("/") ? address.Substring(0, address.Length - 1) : address;
            }
        }

        public string ProccessCheck(string url = null)
        {
            errorInfo = string.Empty;
            string address = url ?? DefaultAddress;
            Uri uriToCrawl = new Uri(address);

            //Subscribe to any of these asynchronous events, there are also sychronous versions of each.
            //This is where you process data about specific events of the crawl
            PageCrawlStartingAsync += crawler_ProcessPageCrawlStarting;
            PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted;
            PageCrawlDisallowedAsync += crawler_PageCrawlDisallowed;
            PageLinksCrawlDisallowedAsync += crawler_PageLinksCrawlDisallowed;

            //Start the crawl
            //This is a synchronous call
            Crawl(uriToCrawl);
            return errorInfo;
        }

        protected override CrawledPage CrawlThePage(PageToCrawl pageToCrawl)
        {
            string errorSource = string.Empty;
            CrawledPage crawledPage = base.CrawlThePage(pageToCrawl);
            if (crawledPage.ToString().EndsWith("[200]"))
            {
                CheckThePage(crawledPage.Uri.AbsoluteUri, crawledPage.HtmlDocument, out errorSource);
                if (errorSource != string.Empty)
                {
                    errorInfo += errorSource;
                }
            }

            return crawledPage;
        }

        protected virtual void CheckThePage(string uri, HtmlDocument htmlDocument, out string errorSource)
        {
            errorSource = string.Empty;
        }

        static void crawler_ProcessPageCrawlStarting(object sender, PageCrawlStartingArgs e)
        {
            //Process data
        }

        static void crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            //Process data
        }

        static void crawler_PageLinksCrawlDisallowed(object sender, PageLinksCrawlDisallowedArgs e)
        {
            //Process data
        }

        static void crawler_PageCrawlDisallowed(object sender, PageCrawlDisallowedArgs e)
        {
            //Process data
        }
    }
}
