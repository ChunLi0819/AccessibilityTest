using TestFramework.Abot.Core;
using TestFramework.Abot.Poco;
using TestFramework.Abot.Util;
using HtmlAgilityPack;
using System;

namespace TestFramework.WebChecker
{
    public class VideoTabIndexChecker : WebChecker
    {
        public VideoTabIndexChecker()
            : base()
        {
        }

        public VideoTabIndexChecker(CrawlConfiguration crawlConfiguration)
            : base(crawlConfiguration, null, null, null, null, null, null, null, null)
        {
        }

        public VideoTabIndexChecker(CrawlConfiguration crawlConfiguration, ICrawlDecisionMaker crawlDecisionMaker, IThreadManager threadManager, IScheduler scheduler, IPageRequester pageRequester, IHyperLinkParser hyperLinkParser, IMemoryManager memoryManager, IDomainRateLimiter domainRateLimiter, IRobotsDotTextFinder robotsDotTextFinder)
            : base(crawlConfiguration, crawlDecisionMaker, threadManager, scheduler, pageRequester, hyperLinkParser, memoryManager, domainRateLimiter, robotsDotTextFinder)
        {
        }

        protected override void CheckThePage(string uri, HtmlDocument htmlDocument, out string errorSource)
        {
            errorSource = string.Empty;
            try
            {
                foreach (var srcNode in htmlDocument.DocumentNode.SelectNodes("//iframe/@src"))
                {
                    if (srcNode.Attributes["src"].Value.Contains("channel"))
                    {
                        if (srcNode.ParentNode.Attributes["tabindex"] != null)
                        {
                            if (string.IsNullOrEmpty(srcNode.ParentNode.Attributes["tabindex"].Value) || srcNode.ParentNode.Attributes["tabindex"].Value != "0")
                            {
                                errorSource += srcNode.Attributes["src"].Value + ";";
                            }
                        }
                        else
                        {
                            errorSource = srcNode.Attributes["src"].Value + ";";
                        }
                    }
                }

                if (errorSource != string.Empty)
                {
                    _logger.InfoFormat("The following resources have video tab index issue on url {0}\r\n{1}", uri, errorSource.Substring(0, errorSource.Length - 1));
                    errorSource = string.Format("The following resources have video tab index issue on url {0}\r\n{1}\r\n", uri, errorSource.Substring(0, errorSource.Length - 1));
                }
            }
            catch (Exception e)
            {
                _logger.InfoFormat("Exception is thrown when checking video tab index issue on url {0} with message {1}", uri, e.Message);
                errorSource = string.Format("Exception is thrown when checking video tab index issue on url {0} with message {1}\r\n", uri, e.Message);
            }
        }
    }
}
