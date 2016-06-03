using HtmlAgilityPack;
using System;
using TestFramework.Abot.Core;
using TestFramework.Abot.Poco;
using TestFramework.Abot.Util;

namespace TestFramework.WebChecker
{
    public class EmptyLinkChecker : WebChecker
    {
        public EmptyLinkChecker()
            : base()
        {
        }

        public EmptyLinkChecker(CrawlConfiguration crawlConfiguration)
            : base(crawlConfiguration, null, null, null, null, null, null, null, null)
        {
        }

        public EmptyLinkChecker(CrawlConfiguration crawlConfiguration, ICrawlDecisionMaker crawlDecisionMaker, IThreadManager threadManager, IScheduler scheduler, IPageRequester pageRequester, IHyperLinkParser hyperLinkParser, IMemoryManager memoryManager, IDomainRateLimiter domainRateLimiter, IRobotsDotTextFinder robotsDotTextFinder)
            : base(crawlConfiguration, crawlDecisionMaker, threadManager, scheduler, pageRequester, hyperLinkParser, memoryManager, domainRateLimiter, robotsDotTextFinder)
        {
        }

        protected override void CheckThePage(string uri, HtmlDocument htmlDocument, out string errorSource)
        {
            errorSource = string.Empty;
            try
            {
                foreach (var srcNode in htmlDocument.DocumentNode.SelectNodes("//a/@href"))
                {
                    if (srcNode.HasChildNodes)
                    {
                        bool hasChildNotEmpty = false;
                        foreach (var node in srcNode.ChildNodes)
                        {
                            if(node.Name == "span" || node.Name == "svg" || node.Name == "div" || node.Name == "p")
                            {
                                if (!string.IsNullOrEmpty(node.InnerText))
                                {
                                    hasChildNotEmpty = true;
                                    break;
                                }
                            }
                            else if (node.Name == "img")
                            {
                                hasChildNotEmpty = true;
                                break;
                            }
                        }

                        if (!hasChildNotEmpty && string.IsNullOrEmpty(srcNode.InnerText))
                        {
                            errorSource += srcNode.Attributes["href"].Value + ";";
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(srcNode.InnerText))
                        {
                            errorSource += srcNode.Attributes["href"].Value + ";";
                        }
                    }
                }

                if (errorSource != string.Empty)
                {
                    _logger.InfoFormat("The following resources have empty link issue on url {0}\r\n{1}", uri, errorSource.Substring(0, errorSource.Length - 1));
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
