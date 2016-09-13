using System;
using Android.App;
using Android.Views;
using Android.Widget;
using OneTube.Models;
using System.Collections;
using System.Collections.Generic;

namespace OneTube.Droid
{
	public class SimilarVideosAdapter : BaseAdapter
	{
		List<VideoFile> items;
		Activity context;

		public SimilarVideosAdapter(Activity context, IEnumerable<VideoFile> items) : base()
		{
			this.context = context;
			this.items = new List<VideoFile>(items);
		}
		public override long GetItemId(int position)
		{
			return position;
		}

		public override Java.Lang.Object GetItem(int position)
		{
			throw new NotImplementedException();
		}
		public override int Count
		{
			get { return items.Count; }
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is available
			if (view == null) // otherwise create a new one
				view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
			view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = items[position].Name;
			return view;
		}
	}
}