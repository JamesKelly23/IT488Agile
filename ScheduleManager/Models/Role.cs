using System.Security.Cryptography.X509Certificates;

namespace ScheduleManager.Models
{
    public class Role
    {
        private SqlConnection theConnection = new(ConnectionStrings.local);
        public int ID { get; private set; }
        public string Name { get; set; }
        public Role(int id)
        {
            SqlCommand theCommand = new("SELECT * FROM Role Where ID=" + id + ";",theConnection);
            theConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            theReader.Read();
            ID = id;
            Name = theReader.GetString(1);
            theConnection.Close();
        }
        public Role(string name)
        {
            ID = 0;
            Name = name;
        }
        public static List<Role> GetList()
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            List<Role> list = new();
            SqlCommand theCommand = new("SELECT ID FROM Role", staticConnection);
            staticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while(theReader.Read())
            {
                list.Add(new Role(theReader.GetInt32(0)));
            }
            return list;
        }
        public static void Delete(int id)
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            SqlCommand theCommand = new("DELETE FROM Role WHERE ID=" + id + ";",staticConnection);
            staticConnection.Open();
            try
            {
                theCommand.ExecuteNonQuery();
            } catch (Exception ex)
            {
                String theMessage = ex.Message;
            }
            finally
            {
                staticConnection.Close();
            }
        }
        public String Save()
        {
            SqlCommand theCommand;
            if (ID == 0)
            {
                theCommand = new SqlCommand("INSERT INTO Role (Name) OUTPUT INSERTED.ID VALUES ('" + Name + "');", theConnection);
                theConnection.Open();
                ID = Convert.ToInt32(theCommand.ExecuteScalar());
                theConnection.Close();
                return "Success, the ID of the new record is " + ID;
            }
            else
            {
                theCommand = new SqlCommand("UPDATE Role SET Name='" + Name + "' WHERE ID=" + ID + ";", theConnection);
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
    }
}
