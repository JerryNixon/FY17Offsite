using System;
using UIKit;
using AVFoundation;
using Foundation;
using CoreGraphics;
using OneTube.Models;

namespace OneTube.iOS
{
	public class UIVideoViewController : UIViewController
	{
		AVPlayer _player;
		AVPlayerLayer _playerLayer;
		AVAsset _asset;
		AVPlayerItem _playerItem;
		SimilarVideosTableView videoTable = new SimilarVideosTableView();
		UIActivityIndicatorView activityIndicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);

		AppDelegate appDelegate { get { return (AppDelegate)UIApplication.SharedApplication.Delegate; } }

		public UIVideoViewController()
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			View.BackgroundColor = UIColor.White;

			activityIndicator.HidesWhenStopped = true;
			activityIndicator.Frame = new CGRect(0, 20, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Width * (9.0 / 16.0));
			videoTable.Frame = new CGRect(0, UIScreen.MainScreen.Bounds.Width * (9.0 / 16.0) + 20, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height - UIScreen.MainScreen.Bounds.Width * (9.0 / 16.0));

			View.AddSubview(activityIndicator);
			View.AddSubview(videoTable);

			activityIndicator.TranslatesAutoresizingMaskIntoConstraints = false;
			videoTable.TranslatesAutoresizingMaskIntoConstraints = false;

			activityIndicator.StartAnimating();

			var url = "https://so7ona.bn1302.livefilestore.com/y3mpDt20dWYBs2fSOQnxNbUzM3paqogmLhqENQIQOynmtEAByW8l6Tt4ue7VGmEductTabKPIeG3YkWHZPsEmdBQEvSFresoNotGXE2II0sjAvv9ZqF4cJc6jB7pDOFHOKn3ckjQxWQKl1TsQCEsCN2IDlCdvCMPQUqJ_D6QVEsqiE";
			_asset = AVAsset.FromUrl(NSUrl.FromString(url));

			var width = appDelegate.ScreenWidth;
			var height = (nfloat)(Math.Floor(appDelegate.ScreenWidth * (9.0 / 16.0)));

			_playerItem = new AVPlayerItem(_asset);
			_player = new AVPlayer(_playerItem);
			_playerLayer = AVPlayerLayer.FromPlayer(_player);
			_playerLayer.Frame = new CGRect(0, 0, width, height);

			var containerView = new UIView(new CGRect(0, 20, width, height));
			containerView.BackgroundColor = UIColor.Black;
			containerView.Layer.AddSublayer(_playerLayer);
			View.AddSubview(containerView);

			activityIndicator.StopAnimating();
			_player.Play();
		}
	}
}