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
            SqlCommand theCommand = new("SP_Update_Rank", theConnection);
            theCommand.CommandType = System.Data.CommandType.StoredProcedure;
            theCommand.Parameters.AddWithValue("@ID", ID);
            theCommand.Parameters.AddWithValue("@Title", Title);
            SqlParameter newParameter = new("@NewID", 0);
            newParameter.Direction = System.Data.ParameterDirection.Output;
            theCommand.Parameters.Add(newParameter);
            String message = "The row was successfully updated.";
            try
            {
                theConnection.Open();
                theCommand.ExecuteNonQuery();
                if (ID == 0)
                {
                    ID = Convert.ToInt32(theCommand.Parameters["@NewID"].Value);
                    message = "Success, the ID of the new record is " + ID;
                }
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
        public static List<Rank> GetList(int maxRank)
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            List<Rank> list = new();
            SqlCommand theCommand = new("SELECT ID From Rank WHERE ID <= " + maxRank + ";", staticConnection);
            staticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while (theReader.Read())
            {
                list.Add(new Rank(theReader.GetInt32(0)));
            }
            staticConnection.Close();
            return list;
        }
    }
}
