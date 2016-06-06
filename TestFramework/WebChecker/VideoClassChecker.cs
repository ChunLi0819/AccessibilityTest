using TestFramework.Abot.Core;
using TestFramework.Abot.Poco;
using TestFramework.Abot.Util;
using HtmlAgilityPack;
using System;

namespace TestFramework.WebChecker
{
    public class VideoClassChecker : WebChecker
    {
        public VideoClassChecker()
            : base()
        {
        }

        public VideoClassChecker(CrawlConfiguration crawlConfiguration)
            : base(crawlConfiguration, null, null, null, null, null, null, null, null)
        {
        }

        public VideoClassChecker(CrawlConfiguration crawlConfiguration, ICrawlDecisionMaker crawlDecisionMaker, IThreadManager threadManager, IScheduler scheduler, IPageRequester pageRequester, IHyperLinkParser hyperLinkParser, IMemoryManager memoryManager, IDomainRateLimiter domainRateLimiter, IRobotsDotTextFinder robotsDotTextFinder)
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
                    if (srcNode.Attributes["src"].Value.Contains("youtube"))
                    {
                        if (srcNode.Attributes["class"] != null)
                        {
                            if (string.IsNullOrEmpty(srcNode.Attributes["class"].Value) || !srcNode.ParentNode.Attributes["class"].Value.Contains("video-outline"))
                            {
                                if (srcNode.ParentNode.Attributes["class"] != null)
                                {
                                    if (string.IsNullOrEmpty(srcNode.ParentNode.Attributes["class"].Value) || !srcNode.ParentNode.Attributes["class"].Value.Contains("video-outline"))
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
                        else
                        {
                            if (srcNode.ParentNode.Attributes["class"] != null)
                            {
                                if (string.IsNullOrEmpty(srcNode.ParentNode.Attributes["class"].Value) || !srcNode.ParentNode.Attributes["class"].Value.Contains("video-outline"))
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
                }

                if (errorSource != string.Empty)
                {
                    _logger.InfoFormat("The following resources have video class issue on url {0}\r\n{1}", uri, errorSource.Substring(0, errorSource.Length - 1));
                    errorSource = string.Format("The following resources have video class issue on url {0}\r\n{1}\r\n", uri, errorSource.Substring(0, errorSource.Length - 1));
                }
            }
            catch (Exception e)
            {
                _logger.InfoFormat("Exception is thrown when checking video class issue on url {0} with message {1}", uri, e.Message);
                errorSource = string.Format("Exception is thrown when checking video class issue on url {0} with message {1}\r\n", uri, e.Message);
            }
        }
    }
}
