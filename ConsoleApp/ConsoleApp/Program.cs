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
            var url = $"https://api.onedrive.com/v1.0/shares/{token}/root/children";
            var http = new HttpClient();
            var result = await http.GetStringAsync(url);
            var obj = JsonConvert.DeserializeObject<RootObject>(result);
            foreach (var item in obj.Items.Where(x => x.video != null))
            {

                // /drive/items/{item-id}/thumbnails/{thumb-id}/{size}/content

                Console.WriteLine(item.name);
            }
        }
    }

    public class Application
    {
        public string displayName { get; set; }
        public string id { get; set; }
    }

    public class Device
    {
        public string id { get; set; }
    }

    public class User
    {
        public string displayName { get; set; }
        public string id { get; set; }
    }

    public class OneDriveSync
    {
        [JsonProperty("@odata.type")]
        public string __type { get; set; }
        public string id { get; set; }
    }

    public class CreatedBy
    {
        public Application application { get; set; }
        public Device device { get; set; }
        public User user { get; set; }
        public OneDriveSync oneDriveSync { get; set; }
    }

    public class Application2
    {
        public string displayName { get; set; }
        public string id { get; set; }
    }

    public class Device2
    {
        public string id { get; set; }
    }

    public class User2
    {
        public string displayName { get; set; }
        public string id { get; set; }
    }

    public class OneDriveSync2
    {
        [JsonProperty("@odata.type")]
        public string __type { get; set; }
        public string id { get; set; }
    }

    public class LastModifiedBy
    {
        public Application2 application { get; set; }
        public Device2 device { get; set; }
        public User2 user { get; set; }
        public OneDriveSync2 oneDriveSync { get; set; }
    }

    public class ParentReference
    {
        public string driveId { get; set; }
        public string id { get; set; }
        public string path { get; set; }
    }

    public class FileSystemInfo
    {
        public string createdDateTime { get; set; }
        public string lastModifiedDateTime { get; set; }
    }

    public class Folder
    {
        public int childCount { get; set; }
    }

    public class User3
    {
        public string displayName { get; set; }
        public string id { get; set; }
    }

    public class Owner
    {
        public User3 user { get; set; }
    }

    public class Shared
    {
        public List<string> effectiveRoles { get; set; }
        public Owner owner { get; set; }
        public string scope { get; set; }
    }

    public class Hashes
    {
        public string crc32Hash { get; set; }
        public string sha1Hash { get; set; }
    }

    public class File
    {
        public Hashes hashes { get; set; }
        public string mimeType { get; set; }
    }

    public class Photo
    {
        public string takenDateTime { get; set; }
        public string cameraMake { get; set; }
        public string cameraModel { get; set; }
        public double? exposureDenominator { get; set; }
        public double? exposureNumerator { get; set; }
        public double? fNumber { get; set; }
        public int? iso { get; set; }
    }

    public class Video
    {
        public int bitrate { get; set; }
        public int duration { get; set; }
        public int height { get; set; }
        public int width { get; set; }
    }

    public class Image
    {
        public int height { get; set; }
        public int width { get; set; }
    }

    public class Value
    {
        public CreatedBy createdBy { get; set; }
        public string createdDateTime { get; set; }
        public string cTag { get; set; }
        public string eTag { get; set; }
        public string id { get; set; }
        public LastModifiedBy lastModifiedBy { get; set; }
        public string lastModifiedDateTime { get; set; }
        public string name { get; set; }
        public ParentReference parentReference { get; set; }
        public int size { get; set; }
        public string webUrl { get; set; }
        public FileSystemInfo fileSystemInfo { get; set; }
        public Folder folder { get; set; }
        public Shared shared { get; set; }
        [JsonProperty("@content.downloadUrl")]
        public string __downloadUrl { get; set; }
        public File file { get; set; }
        public Photo photo { get; set; }
        public Video video { get; set; }
        public Image image { get; set; }
    }

    public class RootObject
    {
        [JsonProperty("@odata.context")]
        public string __context { get; set; }
        [JsonProperty("value")]
        public List<Value> Items { get; set; }
    }
}
