using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () => await GoGetItAsync("https://1drv.ms/f/s!Aq5BEHo6GlC-gfhXWNUebFUWLAJJyg"));
            Console.Read();
        }

        static async Task GoGetItAsync(string uri)
        {
            var token = uri.Substring(uri.IndexOf("s!"));
            var url = $"https://api.onedrive.com/v1.0/shares/{token}/root/children?select=id,name,duration,@content.downloadUrl,video&expand=thumbnails(select=small)&$filter=(video ne null)";
            var http = new HttpClient();
            var result = await http.GetStringAsync(url);
            var obj = JsonConvert.DeserializeObject<RootObject>(result);
            var items = obj.Items
                .Where(x => x.video != null)
                .Select(x => new Item
                {
                    Name = x.name,
                    Duration = x.video.duration,
                    Thumbnail = x.thumbnails[0].small.url,
                    Url = x.downloadUrl
                });
            foreach (var item in items)
            {
                Console.WriteLine(item.Name);
                Console.WriteLine(item.Duration);
                Console.WriteLine(item.Url);
                Console.WriteLine(item.Thumbnail);
            //}
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Thumbnail { get; set; }
        public int Duration { get; set; }
    }

    public class Video
    {
        public int bitrate { get; set; }
        public int duration { get; set; }
        public int height { get; set; }
        public int width { get; set; }
    }

    public class Small
    {
        public int height { get; set; }
        public string url { get; set; }
        public int width { get; set; }
    }

    public class Thumbnail
    {
        public Small small { get; set; }
    }

    public class Value
    {
        [JsonProperty("@content.downloadUrl")]
        public string downloadUrl { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public Video video { get; set; }
        public List<Thumbnail> thumbnails { get; set; }
    }

    public class RootObject
    {
        [JsonProperty("value")]
        public List<Value> Items { get; set; }
    }
}
