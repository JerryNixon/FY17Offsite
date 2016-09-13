using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneTube.Models
{
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
		public string webUrl { get; set; }
		public Video video { get; set; }
		public Folder folder { get; set; }
		public List<Thumbnail> thumbnails { get; set; }
	}

	public class Folder
	{
		public int childCount { get; set; }
	}

	public class RootObject
	{
		[JsonProperty("value")]
		public List<Value> Items { get; set; }
	}
}