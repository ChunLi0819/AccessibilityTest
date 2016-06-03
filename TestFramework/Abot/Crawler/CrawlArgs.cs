using TestFramework.Abot.Poco;
using System;

namespace TestFramework.Abot.Crawler
{
    [Serializable]
    public class CrawlArgs : EventArgs
    {
        public CrawlContext CrawlContext { get; set; }

        public CrawlArgs(CrawlContext crawlContext)
        {
            if (crawlContext == null)
                throw new ArgumentNullException("crawlContext");

            CrawlContext = crawlContext;
        }
    }
}
