using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace WebCrawler
{
    internal class CrawlerProgram
    {
        private static Hashtable visitedUrls = new Hashtable();
        private static int pageCount = 0;
        private static readonly string outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "crawled_pages");
        private static readonly object lockObject = new object();

        static void Main(string[] args)
        {
            Directory.CreateDirectory(outputDirectory);
            string initialUrl = "https://chat.soruxgpt.com/";
            if (args.Length >= 1) initialUrl = args[0];

            lock (lockObject)
            {
                visitedUrls.Add(initialUrl, false);
            }

            new Thread(StartCrawling).Start();
        }

        private static void StartCrawling()
        {
            Console.WriteLine("Crawling has started...");
            while (true)
            {
                string urlToCrawl = null;

                lock (lockObject)
                {
                    foreach (string url in visitedUrls.Keys)
                    {
                        if ((bool)visitedUrls[url]) continue;
                        urlToCrawl = url;
                        break;
                    }
                }

                if (urlToCrawl == null || pageCount > 10) break;

                Console.WriteLine($"Crawling page: {urlToCrawl}");
                string pageContent = DownloadPage(urlToCrawl);

                lock (lockObject)
                {
                    visitedUrls[urlToCrawl] = true;
                }

                pageCount++;
                ParseLinks(pageContent, urlToCrawl); // Pass current URL for parsing relative paths
            }
            Console.WriteLine("Crawling finished");
        }

        public static string DownloadPage(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AllowAutoRedirect = true;
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64)";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string htmlContent = reader.ReadToEnd();
                    string fileName = $"{pageCount}.html";
                    string filePath = Path.Combine(outputDirectory, fileName);
                    File.WriteAllText(filePath, htmlContent, Encoding.UTF8);
                    return htmlContent;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Download failed for [{url}]: {ex.Message}");
                return string.Empty;
            }
        }

        public static void ParseLinks(string htmlContent, string currentUrl)
        {
            string linkPattern = @"(href|HREF)\s*=\s*[""'](.*?)[""']";
            MatchCollection linkMatches = Regex.Matches(htmlContent, linkPattern);
            Uri baseUri = new Uri(currentUrl);

            foreach (Match match in linkMatches)
            {
                string extractedLink = match.Groups[2].Value.Trim();
                if (string.IsNullOrEmpty(extractedLink)) continue;

                try
                {
                    Uri fullUri = new Uri(baseUri, extractedLink);
                    string absoluteUrl = fullUri.AbsoluteUri;

                    lock (lockObject)
                    {
                        if (!visitedUrls.ContainsKey(absoluteUrl))
                        {
                            visitedUrls.Add(absoluteUrl, false);
                            Console.WriteLine($"New link discovered: {absoluteUrl}");
                        }
                    }
                }
                catch (UriFormatException)
                {
                    Console.WriteLine($"Invalid URL format: {extractedLink}");
                }
            }
        }
    }
}
