using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace OneTube.UITests
{
	public class AppInitializer
	{
		public static IApp StartApp(Platform platform)
		{
			if (platform == Platform.Android)
			{
				return ConfigureApp.Android.StartApp();
			}

			return ConfigureApp.iOS.AppBundle("/Users/michaelw/GitHub/FY17Offsite/OneTube/iOS/bin/iPhoneSimulator/Debug/OneTube.iOS.app").StartApp();
		}
	}
}

