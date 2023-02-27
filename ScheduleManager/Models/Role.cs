global using Microsoft.Data.SqlClient;

namespace ScheduleManager.Models
{
    public class Rank
    {
        public int ID { get; }
        public string Title { get; set; }

        public Rank(int theID)
        {
            ID = theID;
            SqlConnection theConnection = new SqlConnection("Data Source=localhost\\SQLEXPRESS; Integrated Security=true; Initial Catalog='ScheduleManager'");
            theConnection.Open();
            SqlCommand theCommand = new SqlCommand("SELECT * FROM Rank WHERE ID=" + ID, theConnection);
            SqlDataReader theReader = theCommand.ExecuteReader();
            theReader.Read();
            Title = theReader.GetString(1);
            theConnection.Close();
        }
    }
}
