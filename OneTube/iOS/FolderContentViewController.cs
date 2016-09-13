using System;
using Foundation;
using OneTube.Models;
using UIKit;
using OneTube.Controllers;

namespace OneTube.iOS
{
	public class FolderContentViewController : UITableViewController, IUITableViewDelegate, IUITableViewDataSource
	{
		NSString CellIdentifier = new NSString("MyChannels");
		OneDriveFolder selectedFolder
		{
			get
			{
				return ((AppDelegate)UIApplication.SharedApplication.Delegate).fileController.SelectedFolder;
			}
		}

		public FolderContentViewController()
		{
		}

		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();

			await ((AppDelegate)UIApplication.SharedApplication.Delegate).fileController.GetAllFoldersJsonContentAsync();
			TableView.ReloadData();
		}

		public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = TableView.DequeueReusableCell(CellIdentifier) as FileTableViewCell;
			if (cell == null)
				cell = new FileTableViewCell(selectedFolder.FolderContents[indexPath.Row], CellIdentifier);

			cell.LoadFileData(selectedFolder.FolderContents[indexPath.Row]);

			return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			base.RowSelected(tableView, indexPath);


		}

		public nint RowsInSection(UITableView tableView, nint section)
		{
			if (selectedFolder == null)
				return 0;

			return selectedFolder.FolderContents?.Count ?? 0;
		}
	}
}