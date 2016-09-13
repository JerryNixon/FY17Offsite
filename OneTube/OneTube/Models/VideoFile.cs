using System;
using System.Collections.Generic;
namespace OneTube.Models
{
	public class VideoFile : IFile
	{
		public List<VideoFile> SimilarVideos { get; set; }

		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ContentDownloadUrl { get; set; }
		public int Duration { get; set; }

		public string ThumbnailUrl { get; set; }
	}
}