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
            staticConnection.Close();
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
            SqlCommand theCommand = new("SP_Update_Role", theConnection);
            theCommand.CommandType = System.Data.CommandType.StoredProcedure;
            theCommand.Parameters.AddWithValue("@ID", ID);
            theCommand.Parameters.AddWithValue("@Name", Name);
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
    }
}
