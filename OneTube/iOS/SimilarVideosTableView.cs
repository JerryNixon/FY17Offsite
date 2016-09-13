using System;

using Foundation;
using UIKit;

using OneTube.Models;

namespace OneTube.iOS
{
	public class SimilarVideosTableView : UITableView, IUITableViewDelegate, IUITableViewDataSource
	{
		NSString CellIdentifier = new NSString("SimilarVideo");
		VideoFile SelectedVideo
		{
			get
			{
				return ((AppDelegate)UIApplication.SharedApplication.Delegate).fileController.SelectedVideo;
			}
		}

		public SimilarVideosTableView()
		{
		}

		public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = DequeueReusableCell(CellIdentifier) as FileTableViewCell;
			if (cell == null)
				cell = new FileTableViewCell(SelectedVideo.SimilarVideos[indexPath.Row], CellIdentifier);

			cell.LoadFileData(SelectedVideo.SimilarVideos[indexPath.Row]);

			return cell;
		}

		public override nint NumberOfRowsInSection(nint section)
		{
			return SelectedVideo?.SimilarVideos?.Count ?? 0;
		}

		public nint RowsInSection(UITableView tableView, nint section)
		{
			return SelectedVideo?.SimilarVideos?.Count ?? 0;
		}
	}
}