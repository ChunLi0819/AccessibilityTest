using log4net;
using System;
using TestFramework.Abot.Core;
using TestFramework.Abot.Poco;
using TestFramework.WebChecker;

namespace TestFramework
{
    public static class WebCheckers
    {
        static ILog _logger = LogManager.GetLogger("AbotLogger");
        private static CrawlConfiguration _config;

        static WebCheckers()
        {
            //Create a config object manually
            CrawlConfiguration config = new CrawlConfiguration();
            config.CrawlTimeoutSeconds = 0;
            config.DownloadableContentTypes = "text/html, text/plain";
            config.IsExternalPageCrawlingEnabled = false;
            config.IsExternalPageLinksCrawlingEnabled = false;
            config.IsRespectRobotsDotTextEnabled = false;
            config.IsUriRecrawlingEnabled = false;
            config.MaxConcurrentThreads = 1;
            config.MaxPagesToCrawl = 3000;
            config.MaxPagesToCrawlPerDomain = 0;
            config.MinCrawlDelayPerDomainMilliSeconds = 1000;
            config.HttpRequestTimeoutInSeconds = 60;

            //Add you own values without modifying Abot's source code.
            //These are accessible in CrawlContext.CrawlConfuration.ConfigurationException object throughout the crawl
            config.ConfigurationExtensions.Add("KeywordExternalLink", "ExternalLink");
            config.ConfigurationExtensions.Add("KeywordID", "ID");
            config.ConfigurationExtensions.Add("BaseAddress", "http://officedevcentersite-devx.azurewebsites.net/");
            //config.ConfigurationExtensions.Add("IngoreUrlType", "htm");
            _config = GetCrawlConfigurationFromConfigFile() ?? config;
        }

        public static CrawlConfiguration CrawlConfiguration
        {
            get
            {
                return _config;
            }

            set
            {
                _config = value;
            }
        }

        public static ImageAltTxtChecker ImageAltTxtChecker
        {
            get
            {
                return new ImageAltTxtChecker(_config);
            }
        }

        public static VideoClassChecker VideoClassChecker
        {
            get
            {
                return new VideoClassChecker(_config);
            }
        }

        public static VideoTabIndexChecker VideoTabIndexChecker
        {
            get
            {
                return new VideoTabIndexChecker(_config);
            }
        }

        public static EmptyLinkChecker EmptyLinkChecker
        {
            get
            {
                return new EmptyLinkChecker(_config);
            }
        }

        public static ImageSrcChecker ImageSrcChecker
        {
            get
            {
                return new ImageSrcChecker(_config);
            }
        }

        private static CrawlConfiguration GetCrawlConfigurationFromConfigFile()
        {
            AbotConfigurationSectionHandler configFromFile = AbotConfigurationSectionHandler.LoadFromXml();

            if (configFromFile == null)
                throw new InvalidOperationException("abot config section was NOT found");

            _logger.DebugFormat("abot config section was found");
            return configFromFile.Convert();
        }
    }
}
