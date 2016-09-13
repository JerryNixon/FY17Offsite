using System;
using Foundation;
using OneTube.Models;
using UIKit;
using OneTube.Controllers;
using System.Collections.Generic;
using System.IO;

namespace OneTube.iOS
{
	public class MyChannelsTableViewController : UITableViewController, IUITableViewDelegate, IUITableViewDataSource
	{
		NSString CellIdentifier = new NSString("MyChannels");
		AppDelegate appDelegate
		{
			get
			{
				return ((AppDelegate)UIApplication.SharedApplication.Delegate);
			}
		}

		public MyChannelsTableViewController()
		{
			addChannelNavButton.AccessibilityIdentifier = "AddChannel";
			addChannelNavButton.Title = "Add Channel";
			addChannelNavButton.Clicked += AddChannelNavButton_Clicked;
		}

		UIBarButtonItem addChannelNavButton = new UIBarButtonItem(UIBarButtonSystemItem.Add);

		void AddChannelNavButton_Clicked(object sender, EventArgs e)
		{
			NavigationController.PushViewController(new AddChannelViewController(), true);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			TableView.AccessibilityIdentifier = "ChannelsTable";

			NavigationItem.SetRightBarButtonItem(addChannelNavButton, false);
		}

		public async override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			await ((AppDelegate)UIApplication.SharedApplication.Delegate).fileController.GetMyAllChannelsContentAsync();
			TableView.ReloadData();
		}

		public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = TableView.DequeueReusableCell(CellIdentifier) as UITableViewCell;
			if (cell == null)
				cell = new UITableViewCell(UITableViewCellStyle.Value1, CellIdentifier);

			var channel = appDelegate.fileController.GetChannelByIndex(indexPath.Row);
			if (channel != null)
			{
				cell.TextLabel.Text = channel.name ?? "";
				cell.DetailTextLabel.Text = channel.owner?.displayName ?? "";
			}

			return cell;
		}

		public nint RowsInSection(UITableView tableView, nint section)
		{
			return appDelegate.fileController.ChannelUrls?.Count ?? 0;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var channelSelelcted = appDelegate.fileController.GetChannelByIndex(indexPath.Row);
			if (channelSelelcted != null)
			{
				var files = new List<IFile>();
				var areThereFilesToShow = appDelegate.fileController.ChannelDictionary.TryGetValue(channelSelelcted, out files);

				if (areThereFilesToShow)
				{
					appDelegate.SelectedFolder = new OneDriveFolder
					{
						FolderUrl = channelSelelcted.ChannelUrl,
						FolderContents = files,
						Name = channelSelelcted.name
					};
					NavigationController.PushViewController(new FolderContentViewController(), true);
				}

			}
		}
	}
}

