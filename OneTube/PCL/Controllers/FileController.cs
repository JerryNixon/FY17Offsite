using System;
using OneTube.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.ServiceModel;
using PCL.Models;

namespace OneTube.Controllers
{
	public class FileController
	{
		public FileController(Channel channelUrl)
		{
			if (channelUrl == null)
				throw new Exception("You must enter a channel url");

			ChannelUrls.Add(channelUrl);
		}

		public FileController(List<Channel> channelUrls)
		{
			if (channelUrls == null)
				throw new Exception("You must enter channel urls");

			ChannelUrls = channelUrls;
		}

		HttpClient client = new HttpClient();

		public List<Channel> ChannelUrls { get; set; } = new List<Channel>();
		public Dictionary<Channel, List<IFile>> ChannelDictionary = new Dictionary<Channel, List<IFile>>();

		public Channel GetChannelByIndex(int index)
		{
			if (index > ChannelUrls.Count)
				return null;

			return ChannelUrls[index];
		}

		public async Task GetMyAllChannelsContentAsync()
		{
			List<Task<KeyValuePair<Channel, List<IFile>>>> pendingTasks = new List<Task<KeyValuePair<Channel, List<IFile>>>>();
			foreach (var channel in ChannelUrls)
				pendingTasks.Add(GetFolderValuesContentAsync(channel.ChannelUrl));

			while (pendingTasks.Any())
			{
				// Wait for the first task to finish
				var completedTask = await Task.WhenAny(pendingTasks).ConfigureAwait(false);

				// Remove from our running list   
				pendingTasks.Remove(completedTask);
				// Process the completed task
				var completed = completedTask.Result;

				if (!ChannelDictionary.ContainsValue(completed.Value))
					if (!ChannelDictionary.ContainsKey(completed.Key))
						ChannelDictionary.Add(completed.Key, completed.Value);
			}
		}

		public async Task<Channel> GetFolderInfoAsync(string uri)
		{
			var token = uri.Substring(uri.IndexOf("s!"));
			var url = $"https://api.onedrive.com/v1.0/shares/{token}/";
			var result = await client.GetStringAsync(url);
			var parsedResult = JsonConvert.DeserializeObject<Channel>(result);

			parsedResult.ChannelUrl = uri;

			return parsedResult;
		}

		public async Task<KeyValuePair<Channel, List<IFile>>> GetFolderValuesContentAsync(string uri)
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
					ThumbnailUrl = x?.thumbnails?.FirstOrDefault().small?.url ?? "",
					FolderUrl = x.webUrl,
				});

			values.AddRange(folders);
			values.AddRange(videos);

			url = $"https://api.onedrive.com/v1.0/shares/{token}/";
			result = await client.GetStringAsync(url);

			var channel = JsonConvert.DeserializeObject<Channel>(result);
			var channelInList = ChannelUrls.Find(x => x.ChannelUrl == uri);

			if (channelInList != null)
			{
				ChannelUrls.Remove(channelInList);

				channelInList.owner = channel.owner;
				channelInList.name = channel.name;

				ChannelUrls.Add(channelInList);
			}

			return new KeyValuePair<Channel, List<IFile>>(channelInList, values);
		}

		//public bool AddChannelUrl(string url)
		//{
		//	if (string.IsNullOrEmpty(url))
		//		return false;
		//	if (ChannelUrls.Contains(url))
		//		return false;
		//
		//	ChannelUrls.Add(url);
		//	return true;
		//}
	}
}