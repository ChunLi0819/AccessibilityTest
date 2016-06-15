using HtmlAgilityPack;
using System;
using TestFramework.Abot.Core;
using TestFramework.Abot.Poco;
using TestFramework.Abot.Util;

namespace TestFramework.WebChecker
{
    public class AriaLabelChecker : WebChecker
    {
        public AriaLabelChecker()
            : base()
        {
        }

        public AriaLabelChecker(CrawlConfiguration crawlConfiguration)
            : base(crawlConfiguration, null, null, null, null, null, null, null, null)
        {
        }

        public AriaLabelChecker(CrawlConfiguration crawlConfiguration, ICrawlDecisionMaker crawlDecisionMaker, IThreadManager threadManager, IScheduler scheduler, IPageRequester pageRequester, IHyperLinkParser hyperLinkParser, IMemoryManager memoryManager, IDomainRateLimiter domainRateLimiter, IRobotsDotTextFinder robotsDotTextFinder)
            : base(crawlConfiguration, crawlDecisionMaker, threadManager, scheduler, pageRequester, hyperLinkParser, memoryManager, domainRateLimiter, robotsDotTextFinder)
        {
        }

        protected override void CheckThePage(string uri, HtmlDocument htmlDocument, out string errorSource)
        {
            errorSource = string.Empty;
            try
            {
                foreach (var srcNode in htmlDocument.DocumentNode.SelectNodes("//img/@src"))
                {
                    if (!Utility.CheckResourceBlocked(srcNode.Attributes["src"].Value, "Image"))
                    {
                        if (srcNode.ParentNode.Name == "a")
                        {
                            if (srcNode.ParentNode.Attributes["aria-label"] != null)
                            {
                                if (string.IsNullOrEmpty(srcNode.ParentNode.Attributes["aria-label"].Value))
                                {
                                    if (!string.IsNullOrEmpty(srcNode.ParentNode.Attributes["href"].Value) && srcNode.ParentNode.Attributes["href"] != null)
                                        errorSource += srcNode.ParentNode.Attributes["href"].Value + ";";
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(srcNode.Attributes["src"].Value))
                                            errorSource += srcNode.Attributes["src"].Value + ";";
                                        else
                                            errorSource += srcNode.XPath + ";";
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(srcNode.ParentNode.Attributes["href"].Value) && srcNode.ParentNode.Attributes["href"] != null)
                                    errorSource += srcNode.ParentNode.Attributes["href"].Value + ";";
                                else
                                {
                                    if (!string.IsNullOrEmpty(srcNode.Attributes["src"].Value))
                                        errorSource += srcNode.Attributes["src"].Value + ";";
                                    else
                                        errorSource += srcNode.XPath + ";";
                                }
                            }
                        }
                    }
                }

                if (errorSource != string.Empty)
                {
                    _logger.InfoFormat("The following resources have aria-label issue on url {0}\r\n{1}", uri, errorSource.Substring(0, errorSource.Length - 1));
                    errorSource = string.Format("The following resources have aria-label issue on url {0}\r\n{1}\r\n", uri, errorSource.Substring(0, errorSource.Length - 1));
                }
            }
            catch (Exception e)
            {
                _logger.InfoFormat("Exception is thrown when checking aria-label issue on url {0} with message {1}", uri, e.Message);
                errorSource = string.Format("Exception is thrown when checking aria-label issue on url {0} with message {1}\r\n", uri, e.Message);
            }
        }
    }
}
