using TestFramework.Abot.Core;
using TestFramework.Abot.Poco;
using TestFramework.Abot.Util;
using HtmlAgilityPack;
using System;
using System.Xml;

namespace TestFramework.WebChecker
{
    public class ImageAltTxtChecker : WebChecker
    {
        public ImageAltTxtChecker()
            : base()
        {
        }

        public ImageAltTxtChecker(CrawlConfiguration crawlConfiguration)
            : base(crawlConfiguration, null, null, null, null, null, null, null, null)
        {
        }

        public ImageAltTxtChecker(CrawlConfiguration crawlConfiguration, ICrawlDecisionMaker crawlDecisionMaker, IThreadManager threadManager, IScheduler scheduler, IPageRequester pageRequester, IHyperLinkParser hyperLinkParser, IMemoryManager memoryManager, IDomainRateLimiter domainRateLimiter, IRobotsDotTextFinder robotsDotTextFinder)
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
                    if (!CheckResourceBlocked(srcNode.Attributes["src"].Value, "Image"))
                    {
                        if (srcNode.ParentNode.Name == "a")
                        {
                            if (srcNode.ParentNode.Attributes["aria-label"] != null)
                            {
                                if (string.IsNullOrEmpty(srcNode.ParentNode.Attributes["aria-label"].Value))
                                {
                                    errorSource += srcNode.Attributes["src"].Value + ";";
                                }
                            }
                            else
                            {
                                errorSource += srcNode.Attributes["src"].Value + ";";
                            }
                        }
                        else
                        {
                            if (srcNode.Attributes["alt"] != null)
                            {
                                if (string.IsNullOrEmpty(srcNode.Attributes["alt"].Value))
                                {
                                    errorSource += srcNode.Attributes["src"].Value + ";";
                                }
                            }
                            else
                            {
                                errorSource += srcNode.Attributes["src"].Value + ";";
                            }
                        }
                    }
                }

                if (errorSource != string.Empty)
                {
                    _logger.InfoFormat("The following resources have alt txt of image issue on url {0}\r\n{1}", uri, errorSource.Substring(0, errorSource.Length - 1));
                    errorSource = string.Format("The following resources have alt txt of image issue on url {0}\r\n{1}\r\n", uri, errorSource.Substring(0, errorSource.Length - 1));
                }
            }
            catch (Exception e)
            {
                _logger.InfoFormat("Exception is thrown when checking alt txt of image issue on url {0} with message {1}", uri, e.Message);
                errorSource = string.Format("Exception is thrown when checking alt txt of image issue on url {0} with message {1}\r\n", uri, e.Message);
            }
        }

        private bool CheckResourceBlocked(string url, string category)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Contents\\BlockedResource.xml");
            XmlNodeList urlItemList = doc.DocumentElement.GetElementsByTagName("Item");
            foreach (XmlNode node in urlItemList)
            {
                if (category.Equals(((XmlElement)node).GetAttribute("category")))
                {
                    if (url.Equals(((XmlElement)node).GetAttribute("url")))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
