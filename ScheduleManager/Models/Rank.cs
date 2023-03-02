global using Microsoft.Data.SqlClient;

namespace ScheduleManager.Models
{
    public class Rank
    {
        private SqlConnection theConnection = new(ConnectionStrings.local);
        public int ID { get; private set; }
        public string Title { get; set; }

        public Rank(int theID)
        {
            if(theID !=0)
            {
                ID = theID;
                theConnection.Open();
                SqlCommand theCommand = new("SELECT * FROM Rank WHERE ID=" + ID, theConnection);
                SqlDataReader theReader = theCommand.ExecuteReader();
                theReader.Read();
                Title = theReader.GetString(1);
                theConnection.Close();
            } else
            {
                ID = 0;
                Title = "";
            }
        }
        public Rank(string theTitle)
        {
            Title= theTitle;
        }
        public string Save()
        {
            SqlCommand theCommand;
            if(ID==0)
            {
                theCommand = new SqlCommand("INSERT INTO Rank (Title) OUTPUT INSERTED.ID VALUES ('" + Title + "');", theConnection);
                theConnection.Open();
                ID = Convert.ToInt32(theCommand.ExecuteScalar());
                theConnection.Close();
                return "Success, the ID of the new record is " + ID;
            } else
            {
                theCommand = new SqlCommand("UPDATE Rank SET Title='" + Title + "' WHERE ID=" +ID +";", theConnection);
                String message;
                try
                {
                    theConnection.Open();
                    theCommand.ExecuteNonQuery();
                    message = "The row was successfully updated.";
                }
                catch (Exception ex)
                {
                    message = "The row was not successfully updated. Error: " + ex.Message;
                }
                finally
                {
                    theConnection.Close();
                }
                return message;
            }
        }
        public static List<Rank> GetList()
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            List<Rank> list = new();
            SqlCommand theCommand = new("SELECT ID From Rank;", staticConnection);
            staticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while(theReader.Read())
            {
                list.Add(new Rank(theReader.GetInt32(0)));
            }
            staticConnection.Close();
            return list;
        }
    }
}
