using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace OneTube.Models
{
	public interface IFile
	{
		string Id { get; set; }
		string Name { get; set; }
		string Description { get; set; }

		string ThumbnailUrl { get; set; }
	}
}