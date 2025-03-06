using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace WebCrawler
{
    class Program
    {
        // 可配置的参数
        private const int MaxTasks = 5;            // 最大并发数
        private const int MaxPageCount = 20;       // 最大下载页面数
        private static readonly string DirectoryPath = @"D:\crawler"; // 保存目录
        private static readonly Uri StartingUrl = new Uri("https://chat.soruxgpt.com/"); // 起始链接

        // 线程安全的数据结构
        private static readonly ConcurrentDictionary<Uri, bool> ProcessedUrls = new ConcurrentDictionary<Uri, bool>(); // 已访问的链接
        private static readonly BlockingCollection<Uri> UrlQueue = new BlockingCollection<Uri>(new ConcurrentQueue<Uri>()); // 等待抓取的链接队列

        private static int ProcessedCount = 0; // 处理过的页面计数

        static async Task Main(string[] args)
        {
            // 创建保存目录
            Directory.CreateDirectory(DirectoryPath);

            // 初始化爬虫，添加起始链接
            InitializeCrawler();

            // 启动多个爬虫任务
            var tasks = new Task[MaxTasks];
            for (int i = 0; i < MaxTasks; i++)
            {
                tasks[i] = Task.Run(ProcessUrlsAsync);
            }

            // 等待所有任务完成
            await Task.WhenAll(tasks);
            Console.WriteLine("所有任务完成");
        }

        // 初始化爬虫
        static void InitializeCrawler()
        {
            ProcessedUrls.TryAdd(StartingUrl, false); // 标记起始链接为未处理
            UrlQueue.Add(StartingUrl); // 将起始链接加入队列
            Console.WriteLine($"爬虫启动 | 起始链接: {StartingUrl}");
        }

        // 处理每个链接的任务
        static async Task ProcessUrlsAsync()
        {
            while (ProcessedCount < MaxPageCount && !UrlQueue.IsCompleted)
            {
                if (UrlQueue.TryTake(out Uri currentUri, 1000)) // 从队列中取出链接，最多等待1秒
                {
                    try
                    {
                        // 下载HTML内容
                        var html = await DownloadHtmlAsync(currentUri);
                        if (!string.IsNullOrEmpty(html))
                        {
                            // 递增已处理的页面数量
                            Interlocked.Increment(ref ProcessedCount);

                            // 解析并加入新的链接到队列
                            ExtractAndAddLinks(html, currentUri);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[错误] 处理链接失败: {currentUri}\n原因: {ex.Message}");
                    }
                }
            }
        }

        // 下载HTML内容
        static async Task<string> DownloadHtmlAsync(Uri uri)
        {
            // 替换 'using var' 为传统的 'using'
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)"); // 模拟浏览器

                try
                {
                    var response = await client.GetAsync(uri);
                    response.EnsureSuccessStatusCode(); // 确保请求成功

                    var htmlContent = await response.Content.ReadAsStringAsync();
                    SaveToFile(uri, htmlContent); // 保存到文件
                    return htmlContent;
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"[网络错误] {uri}\n错误信息: {ex.Message}");
                    return null;
                }
            }
        }


        // 解析HTML内容，提取链接
        static void ExtractAndAddLinks(string html, Uri baseUri)
        {
            var matches = Regex.Matches(html, @"<a\s+[^>]*href\s*=\s*[""']([^""'#]+)");

            foreach (Match match in matches)
            {
                var link = match.Groups[1].Value.Trim();
                AddNewLink(link, baseUri);
            }
        }

        // 添加新链接
        static void AddNewLink(string link, Uri baseUri)
        {
            try
            {
                if (Uri.TryCreate(baseUri, link, out Uri newUri))
                {
                    if (IsValidUrl(newUri) && ProcessedUrls.TryAdd(newUri, false))
                    {
                        UrlQueue.Add(newUri); // 加入队列
                        Console.WriteLine($"新链接发现: {newUri}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[链接处理错误] {link}\n原因: {ex.Message}");
            }
        }

        // 判断URL是否合法
        static bool IsValidUrl(Uri uri)
        {
            return uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps; // 只处理HTTP/HTTPS链接
        }

        // 将内容保存到文件
        static void SaveToFile(Uri uri, string content)
        {
            var fileName = $"{DateTime.Now:yyyyMMddHHmmss}_{uri.Host}";
            fileName = Regex.Replace(fileName, "[^a-zA-Z0-9]", "_") + ".html"; // 确保文件名合法

            try
            {
                var filePath = Path.Combine(DirectoryPath, fileName);
                File.WriteAllText(filePath, content, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[文件保存失败] {uri}\n原因: {ex.Message}");
            }
        }
    }
}
