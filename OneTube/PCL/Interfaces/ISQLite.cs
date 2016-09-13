using SQLite;

namespace PCL.Interfaces
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}