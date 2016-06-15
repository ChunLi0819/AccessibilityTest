using System;
using System.Net;
using System.Xml;
using TestFramework.Abot.Poco;

namespace TestFramework.Abot.Util
{
    public static class Utility
    {
        /// <summary>
        /// Verify whether a url refer to a valid image
        /// </summary>
        /// <param name="Url">The image url</param>
        /// <returns>True if yes, else no</returns>
        public static bool FileExist(string Url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Timeout = 15000;
                request.Method = "HEAD";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NotModified);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string ConvertRelativeUrl(string url, string relativeUrl)
        {
            string absoluteUrl = string.Empty;
            string prefix = url.Substring(0, url.IndexOf("//") + 2);
            string other = url.Substring(url.IndexOf("//") + 2);
            absoluteUrl = relativeUrl.StartsWith("/") ? prefix + other.Substring(0, other.IndexOf("/")) + relativeUrl :
                prefix + other.Substring(0, other.IndexOf("/") + 1) + relativeUrl;
            return absoluteUrl;
        }

        public static bool CheckResourceBlocked(string url, string category)
        {
            if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(category))
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
            }

            return false;
        }

        /// <summary>
        /// Get a property's value from App.config
        /// </summary>
        /// <param name="propertyName">The property's key</param>
        /// <returns>The property's value</returns>
        public static bool GetConfigurationValue(CrawlConfiguration config, string propertyName, out string propertyValue)
        {
            propertyValue = string.Empty;
            return config.ConfigurationExtensions.TryGetValue(propertyName, out propertyValue);
        }
    }
}
