using System;
using OneTube.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.ServiceModel;

namespace OneTube.Controllers
{
	public class FileController
	{
		public FileController(string channelUrl)
		{
			if (string.IsNullOrEmpty(channelUrl))
				throw new Exception("You must enter a channel url");

			ChannelUrls.Add(channelUrl);
			SelectedVideo = new VideoFile { ContentDownloadUrl = "https://onedrive.live.com/?authkey=%21AFjVHmxVFiwCSco&cid=BE501A3A7A1041AE&id=BE501A3A7A1041AE%21879185&parId=BE501A3A7A1041AE%2131831&o=OneUp" };
		}

		public FileController(List<string> channelUrls)
		{
			ChannelUrls = channelUrls;
		}

		HttpClient client = new HttpClient();

		public List<string> ChannelUrls { get; set; } = new List<string>();
		public Dictionary<string, List<IFile>> ChannelDictionary = new Dictionary<string, List<IFile>>();

		public VideoFile SelectedVideo { get; set; }
		public OneDriveFolder SelectedFolder { get; set; }

		public IFile test(int index)
		{
			var key = ChannelUrls[index];
			var values = new List<IFile>();

			var success = ChannelDictionary.TryGetValue(key, out values);
			if (success)
				return values[0];

			return null;
		}

		public async Task GetAllFoldersJsonContentAsync()
		{
			List<Task<KeyValuePair<string, List<IFile>>>> pendingTasks = new List<Task<KeyValuePair<string, List<IFile>>>>();
			foreach (var channel in ChannelUrls)
				pendingTasks.Add(GetFolderValuesContentAsync(channel));

			while (pendingTasks.Any())
			{
				// Wait for the first task to finish
				var completedTask = await Task.WhenAny(pendingTasks).ConfigureAwait(false);

				// Remove from our running list   
				pendingTasks.Remove(completedTask);
				// Process the completed task
				var completed = completedTask.Result;

				ChannelDictionary.Add(completed.Key, completed.Value);

				SelectedFolder = new OneDriveFolder
				{
					FolderUrl = completed.Key,
					FolderContents = completed.Value
				};
			}
		}

		public async Task<KeyValuePair<string, List<IFile>>> GetFolderValuesContentAsync(string uri)
		{
			var token = uri.Substring(uri.IndexOf("s!"));
			var url = $"https://api.onedrive.com/v1.0/shares/{token}/root/children?expand=thumbnails(select=small)";
			var result = await client.GetStringAsync(url);
			var parsedResult = JsonConvert.DeserializeObject<RootObject>(result);

			var values = new List<IFile>();
			var videos = parsedResult.Items
				.Where(x => x.video != null)
				.Select(x => new VideoFile
				{
					Name = x.name,
					Description = "None really to use",
					ThumbnailUrl = x.thumbnails?.FirstOrDefault()?.small.url ?? "",
					ContentDownloadUrl = x.downloadUrl,
					Duration = x.video.duration,
				});

			var folders = parsedResult.Items
				.Where(x => x.folder != null)
				.Select(x => new OneDriveFolder
				{
					Name = x.name,
					Description = "None really to use",
					//ThumbnailUrl = x?.thumbnails[0]?.small?.url ?? "",
					FolderUrl = x.webUrl,
				});

			values.AddRange(folders);
			values.AddRange(videos);

			return new KeyValuePair<string, List<IFile>>(uri, values);
		}

		public bool AddChannelUrl(string url)
		{
			if (string.IsNullOrEmpty(url))
				return false;
			if (ChannelUrls.Contains(url))
				return false;

			ChannelUrls.Add(url);
			return true;
		}
	}
}