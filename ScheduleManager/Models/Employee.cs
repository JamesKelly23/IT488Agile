namespace ScheduleManager.Models
{
    public class ConnectionStrings
    {
        public static string local = "Data Source=localhost\\SQLEXPRESS; Integrated Security=true; Initial Catalog=ScheduleManager;";
    }
    public class Employee
    {
        private SqlConnection theConnection = new SqlConnection(ConnectionStrings.local);
        public int ID { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }  
        public int RankID { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public String Phone { get; set; }
        public DateTime DOB { get; set; }
        public String FullName()
        {
            return FirstName + " " + LastName;
        }
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
        public string Save()
        {
            SqlCommand theCommand;
            if (ID == 0)
            {
                theCommand = new SqlCommand("INSERT INTO Rank ('FirstName', 'LastName', 'RankID', 'Password', 'UserName', 'Phone', 'Email', 'DateOfBirth') OUTPUT INSERTED.ID VALUES ('" + FirstName + "', '" + LastName + "', " + RankID + "', '" + Password + "', '" + Username + "', '" + Phone + "', '" + Email + "', '" + DOB.ToString() + "');", theConnection);
                theConnection.Open();
                ID = Convert.ToInt32(theCommand.ExecuteScalar());
                theConnection.Close();
                return "Success, the ID of the new record is " + ID;
            }
            else
            {
                theCommand = new SqlCommand("UPDATE Rank SET FirstName='" + FirstName + ", LastName='" + LastName + ", RankID=" + RankID + ", Password='" + Password + "', Username='" + Username + "', Phone='" +Phone + "', Email='" + Email + "', DateOfBirth='" +DOB.ToString() + "' WHERE ID=" + ID + ";", theConnection);
                theConnection.Open();
                theCommand.ExecuteNonQuery();
                theConnection.Close();
                return "The row was successfully updated.";
            }
        }
        public static List<Employee> GetList()
        {
            SqlConnection staticConnection = new SqlConnection(ConnectionStrings.local);
            List<Employee> list = new List<Employee>();
            SqlCommand theCommand = new SqlCommand("SELECT ID From Employee;", staticConnection);
            staticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while (theReader.Read())
            {
                list.Add(new Employee(theReader.GetInt32(0)));
            }
            staticConnection.Close();
            return list;
        }
    }
}
