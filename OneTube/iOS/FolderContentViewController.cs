using System;
using Foundation;
using OneTube.Models;
using UIKit;
using OneTube.Controllers;
using PCL.Models;

namespace OneTube.iOS
{
	public class FolderContentViewController : UITableViewController, IUITableViewDelegate, IUITableViewDataSource
	{
		NSString CellIdentifier = new NSString("MyChannels");
		OneDriveFolder selectedFolder
		{
			get
			{
				return ((AppDelegate)UIApplication.SharedApplication.Delegate).SelectedFolder;
			}
			set
			{
				((AppDelegate)UIApplication.SharedApplication.Delegate).SelectedFolder = value;
			}
		}

		OneDriveFolder parentFolder;

		public FolderContentViewController()
		{
		}

		public FolderContentViewController(OneDriveFolder parent)
		{
			parentFolder = parent;
		}

		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();

			TableView.AccessibilityIdentifier = "FolderContentTable";
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return 60;
		}

		public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = TableView.DequeueReusableCell(CellIdentifier) as FileTableViewCell;
			if (cell == null)
				cell = new FileTableViewCell(selectedFolder.FolderContents[indexPath.Row], CellIdentifier);

			cell.LoadFileData(selectedFolder.FolderContents[indexPath.Row]);

			return cell;
		}

		public async override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var fileSelected = selectedFolder.FolderContents[indexPath.Row];
			var folderSelected = fileSelected as OneDriveFolder;
			var videoFileSelected = fileSelected as VideoFile;

			if (folderSelected != null)
			{
				var nextFolderController = new FolderContentViewController(selectedFolder);
				selectedFolder = folderSelected;

				//Need to display loading indicator
				var folderContentResponse = await ((AppDelegate)UIApplication.SharedApplication.Delegate).fileController.GetFolderValuesContentAsync(folderSelected.FolderUrl);
				selectedFolder.FolderContents = folderContentResponse.Value;

				NavigationController.PushViewController(nextFolderController, true);
				return;
			}

			if (videoFileSelected != null)
			{
				var viewVideoController = new UIVideoViewController();
				((AppDelegate)UIApplication.SharedApplication.Delegate).SelectedVideo = videoFileSelected;

				NavigationController.PushViewController(viewVideoController, true);
			}
		}

		public nint RowsInSection(UITableView tableView, nint section)
		{
			if (selectedFolder == null)
				return 0;

			return selectedFolder.FolderContents?.Count ?? 0;
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}