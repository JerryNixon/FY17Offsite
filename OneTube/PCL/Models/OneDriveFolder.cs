using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace OneTube.Models
{
	public class OneDriveFolder : IFile
	{
		public List<IFile> FolderContents { get; set; }

		public string Id { get; set; }
		public string Name { get; set; }
		public string FolderUrl { get; set; }
		public string Description { get; set; }

		public string ThumbnailUrl { get; set; }
	}
}