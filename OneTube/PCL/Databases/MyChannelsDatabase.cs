using System;
using SQLite;

namespace PCL.Databases
{
	public class MyChannelsDatabase
	{
		SQLiteConnection database;

		public MyChannelsDatabase()
		{
			//database = DependencyService.Get<ISQLite>().GetConnection();
			//database.CreateTable<TodoItem>();
		}

	}
}

