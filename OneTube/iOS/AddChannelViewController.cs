using System;
using UIKit;
using EventKit;
namespace OneTube.iOS
{
	public class AddChannelViewController : UIViewController
	{
		public AddChannelViewController()
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			View.BackgroundColor = UIColor.White;

			var entry = new UITextField();
			var submitButton = new UIButton(UIButtonType.RoundedRect);
			submitButton.SetTitle("Submit", UIControlState.Normal);

			entry.Frame = new CoreGraphics.CGRect(10, 10, 200, 200);
			submitButton.Frame = new CoreGraphics.CGRect(10, 210, 200, 50);

			View.Add(entry);
			View.Add(submitButton);

			submitButton.TouchUpInside += async (sender, e) =>
			{
				var channel = await ((AppDelegate)UIApplication.SharedApplication.Delegate).fileController.GetFolderInfoAsync(entry.Text);
				if (channel != null)
					((AppDelegate)UIApplication.SharedApplication.Delegate).fileController.ChannelUrls.Add(channel);

				NavigationController.PopViewController(true);
			};
		}
	}
}

