using System;
using Newtonsoft.Json.Serialization;
namespace PCL.Models
{
	public class Channel
	{
		public user owner { get; set; }
		public string ChannelUrl { get; set; }
		public string name { get; set; }
		public bool IsValid { get; set; } = true;
	}

	public class user
	{
		public string displayName { get; set; }
		public string id { get; set; }
	}
}