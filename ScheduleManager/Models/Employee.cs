namespace ScheduleManager.Models
{
    public class ConnectionStrings
    {
        public static string local = "Data Source=localhost\\SQLEXORESS; Integrated Security=true; Initial Catalog=ScheduleManager;";
    }
    public class Employee
    {
        public int ID { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }  
        public int RankID { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public String Phone { get; set; }
        public DateTime DOB { get; set; }
        public Employee(string firstName, string lastName, int rankID, string password, string username, string email, string phone, DateTime dOB)
        {
            FirstName = firstName;
            LastName = lastName;
            RankID = rankID;
            Password = password;
            Username = username;
            Email = email;
            Phone = phone;
            DOB = dOB;
        }
        public Employee(int theID)
        {
            ID = theID;
            SqlConnection theConnection = new SqlConnection(ConnectionStrings.local);
            theConnection.Open();
            SqlCommand theCommand = new SqlCommand("SELECT * FROM Employee WHERE ID = " + theID, theConnection);
            SqlDataReader theReader = theCommand.ExecuteReader();
            theReader.Read();
            FirstName = theReader.GetString(1);
            LastName = theReader.GetString(2);
            RankID = theReader.GetInt32(3);
            Password = theReader.GetString(4);
            Username = theReader.GetString(5);
            Email = theReader.GetString(6);
            Phone = theReader.GetString(7);
            DOB = theReader.GetDateTime(8);
            theConnection.Close();
        }

    }
}
