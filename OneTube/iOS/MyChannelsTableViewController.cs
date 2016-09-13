using System;
using Foundation;
using OneTube.Models;
using UIKit;
using OneTube.Controllers;
namespace OneTube.iOS
{
	//public class MyChannelsTableViewController : UITableViewController, IUITableViewDelegate, IUITableViewDataSource
	//{
	//	NSString CellIdentifier = new NSString("MyChannels");
	//	FileController fileController
	//	{
	//		get
	//		{
	//			return ((AppDelegate)UIApplication.SharedApplication.Delegate).fileController;
	//		}
	//	}

	//	public MyChannelsTableViewController()
	//	{
	//	}

	//	public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
	//	{
	//		var cell = TableView.DequeueReusableCell(CellIdentifier) as FileTableViewCell;
	//		if (cell == null)
	//			cell = new FileTableViewCell(fileController.SelectedFolder.[indexPath.Row], CellIdentifier);

	//		cell.UpdateVideoFile(SelectedVideo.SimilarVideos[indexPath.Row]);

	//		return cell;
	//	}

	//	public nint RowsInSection(UITableView tableView, nint section)
	//	{
	//		return SelectedVideo?.SimilarVideos?.Count ?? 0;
	//	}
	//}
}

