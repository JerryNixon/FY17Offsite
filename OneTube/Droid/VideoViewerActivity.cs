using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using OneTube.Controllers;
using System.Collections.Generic;
using OneTube.Models;
using System;
using Android.Content.Res;
using PCL.Models;

namespace OneTube.Droid
{
	[Activity(Label = "OneTube", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/Theme.AppCompat", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize | Android.Content.PM.ConfigChanges.ScreenLayout)]
	public class MainActivity : AppCompatActivity
	{
		int count = 1;
		FileController fileController = new FileController(new Channel { ChannelUrl = "https://1drv.ms/f/s!Aq5BEHo6GlC-gfhXWNUebFUWLAJJyg" });
		VideoView videoPlayer;
		int videoPlayerPosition = 0;
		bool isInitialized;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Main);

			videoPlayer = FindViewById<VideoView>(Resource.Id.videoPlayer);
			var similarVideosList = FindViewById<ListView>(Resource.Id.similarVideos);

			if (similarVideosList != null)
				similarVideosList.Adapter = new SimilarVideosAdapter(this, new List<VideoFile>());//fileController.SelectedVideo.SimilarVideos);å

			if (!isInitialized)
			{
				var uri = Android.Net.Uri.Parse("https://so7ona.bn1302.livefilestore.com/y3mpDt20dWYBs2fSOQnxNbUzM3paqogmLhqENQIQOynmtEAByW8l6Tt4ue7VGmEductTabKPIeG3YkWHZPsEmdBQEvSFresoNotGXE2II0sjAvv9ZqF4cJc6jB7pDOFHOKn3ckjQxWQKl1TsQCEsCN2IDlCdvCMPQUqJ_D6QVEsqiE");

				videoPlayer.SetVideoURI(uri);
				videoPlayer.Start();

				isInitialized = true;
			}
		}

		public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
		{
			base.OnConfigurationChanged(newConfig);

			if (newConfig.Orientation == Android.Content.Res.Orientation.Landscape)
			{
				SupportActionBar.Hide();

			}
			else if (newConfig.Orientation == Android.Content.Res.Orientation.Portrait)
			{
				SupportActionBar.Show();
			}
		}
	}
}