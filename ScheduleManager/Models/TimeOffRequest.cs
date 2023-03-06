namespace ScheduleManager.Models
{
    public class TimeOffRequest
    {
        private SqlConnection theConnection = new(ConnectionStrings.local);
        public int ID { get; private set; }
        public int EmployeeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsApproved { get; set; }
        public int ManagerID { get; set; }
        public string Notes { get; set; }

        public static List<TimeOffRequest> GetList()
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            List<TimeOffRequest> list = new();
            SqlCommand theCommand = new("SELECT ID FROM TimeOffRequest;", staticConnection);
            staticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while(theReader.Read())
            {
                list.Add(new TimeOffRequest(theReader.GetInt32(0)));
            }
            return list;
        }
        public static List<TimeOffRequest> GetAllByEmployee(int EmployeeID) //Returns all time off requests for a specific EmployeeID
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            List<TimeOffRequest> list = new();
            SqlCommand theCommand = new("SELECT ID FROM TimeOffRequest WHERE EmployeeID=" + EmployeeID + ";", staticConnection);
            staticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while (theReader.Read())
            {
                list.Add(new TimeOffRequest(theReader.GetInt32(0)));
            }
            return list;
        }
        public static List<TimeOffRequest> GetPending() //Returns all pending time off requests that exist in the future
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            List<TimeOffRequest> list = new();
            SqlCommand theCommand = new("SELECT ID FROM TimeOffRequest WHERE ManagerID is NULL AND StartDate > '" + DateTime.Today + "';", staticConnection);
            staticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while (theReader.Read())
            {
                list.Add(new TimeOffRequest(theReader.GetInt32(0)));
            }
            return list;
        }
        public static List<TimeOffRequest> GetApprovedByDate(DateTime theDate) //Returns all approved time off requests that affect a certain day
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            List<TimeOffRequest> list = new();
            SqlCommand theCommand = new("SELECT ID FROM TimeOffRequest WHERE IsApproved='TRUE' AND '" + theDate + "' BETWEEN StartDate AND EndDate;", staticConnection);
            staticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while (theReader.Read())
            {
                list.Add(new TimeOffRequest(theReader.GetInt32(0)));
            }
            return list;
        }
        public static List<TimeOffRequest> GetAllByDate(DateTime theDate) //Returns all time off requests that affect a certain day, whether approved or not
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            List<TimeOffRequest> list = new();
            SqlCommand theCommand = new("SELECT ID FROM TimeOffRequest WHERE '" + theDate + "' BETWEEN StartDate AND EndDate;", staticConnection);
            staticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while (theReader.Read())
            {
                list.Add(new TimeOffRequest(theReader.GetInt32(0)));
            }
            return list;
        }
        public Employee GetEmployee()
        {
            return new Employee(EmployeeID);
        }
        public Employee GetManager()
        {
            if(ManagerID == 0)
            {
                return null;
            }
            else
            {
                return new Employee(ManagerID);
            }
        }
        public TimeOffRequest(int id)
        {
            SqlCommand theCommand = new("SELECT * FROM TimeOffRequest WHERE ID=" + id + ";", theConnection);
            theConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            theReader.Read();
            ID = id;
            EmployeeID = theReader.GetInt32(1);
            StartDate = theReader.GetDateTime(2);
            EndDate= theReader.GetDateTime(3);
            IsApproved = theReader.GetBoolean(4);
            ManagerID = theReader.IsDBNull(5) ? 0 : theReader.GetInt32(5);
            Notes = theReader.IsDBNull(6) ? "" : theReader.GetString(6);
            theConnection.Close();
        }
        public TimeOffRequest(int employeeID,  DateTime startDate, DateTime endDate, string notes)
        {
            ID = 0;
            EmployeeID = employeeID;
            StartDate = startDate;
            EndDate = endDate;
            IsApproved = false;
            ManagerID = 0;
            Notes = "";
        }
        public String Save()
        {
            SqlCommand theCommand;
            if (ID == 0)
            {
                theCommand = new SqlCommand("INSERT INTO TimeOffRequest (EmployeeID, StartDate, EndDate, IsApproved, ManagerID, Notes) OUTPUT INSERTED.ID VALUES (" + EmployeeID + ", '" + StartDate + "', '" + EndDate + "', '" + IsApproved + "', '" + (ManagerID==0 ? "NULL" : ManagerID) + "', '" + Notes + "');", theConnection);
                theConnection.Open();
                ID = Convert.ToInt32(theCommand.ExecuteScalar());
                theConnection.Close();
                return "Success, the ID of the new record is " + ID;
            }
            else
            {
                theCommand = new SqlCommand("UPDATE TimeOffRequest SET EmployeeID=" + EmployeeID + ", StartDate='" + StartDate + "', EndDate='" + EndDate + "', IsApproved='" + IsApproved + "', ManagerID=" + (ManagerID == 0 ? "NULL" : ManagerID) + ", Notes='" + Notes + "'f WHERE ID=" + ID + ";", theConnection);
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
