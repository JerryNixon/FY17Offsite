using System;
using UIKit;
using OneTube.Models;
using Foundation;
using CoreGraphics;
using System.Threading.Tasks;

namespace OneTube.iOS
{
	public class FileTableViewCell : UITableViewCell
	{
		UIImageView imageView = new UIImageView();
		UILabel headingLabel = new UILabel();
		UILabel subheadingLabel = new UILabel();

		public FileTableViewCell(IFile folder, NSString cellId) : base(UITableViewCellStyle.Default, cellId)
		{
			headingLabel.BackgroundColor = UIColor.Yellow;
			subheadingLabel.BackgroundColor = UIColor.Blue;

			Add(imageView);
			Add(headingLabel);
			Add(subheadingLabel);
		}

		//public FileTableViewCell(OneDriveFolder folder, NSString cellId) : base(UITableViewCellStyle.Default, cellId)
		//{
		//	LoadFileData(folder);
		//}

		//public FileTableViewCell(VideoFile videoFile, NSString cellId) : base(UITableViewCellStyle.Default, cellId)
		//{
		//	LoadFileData(videoFile);
		//}

		//public void UpdateVideoFile(VideoFile videoFile)
		//{
		//	LoadFileData(videoFile);
		//}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			imageView.Frame = new CGRect(5, 5, 33, 33);
			headingLabel.Frame = new CGRect(5, 4, ContentView.Bounds.Width - 63, 25);
			subheadingLabel.Frame = new CGRect(100, 18, 100, 20);
		}

		public void LoadFileData(IFile videoFile)
		{
			if (!string.IsNullOrEmpty(videoFile.ThumbnailUrl))
			{
				Task.Factory.StartNew(() =>
				{
					BeginInvokeOnMainThread(() =>
					{
						using (var url = new NSUrl(videoFile.ThumbnailUrl))
						using (var data = NSData.FromUrl(url))
							imageView.Image = UIImage.LoadFromData(data);
					});
				});
			}

			headingLabel.Text = videoFile?.Name ?? "";
			headingLabel.AccessibilityIdentifier = videoFile.Name;
			subheadingLabel.Text = videoFile?.Description ?? "";
		}
	}
}