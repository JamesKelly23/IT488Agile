using Microsoft.VisualBasic;

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
            while (theReader.Read())
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
            staticConnection.Close();
            return list;
        }
        public static List<TimeOffRequest> GetPending() //Returns all pending time off requests that exist in the future
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            List<TimeOffRequest> list = new();
            SqlCommand theCommand = new("SELECT ID FROM TimeOffRequest WHERE ManagerID is NULL AND StartDate >= '" + DateTime.Today + "';", staticConnection);
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
            staticConnection.Close();
            return list;
        }
        public static bool IsDayApprovedOffForEmployee(int empid, DateTime theDate)
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            List<TimeOffRequest> list = new();
            SqlCommand theCommand = new("SELECT ID FROM TimeOffRequest WHERE EmployeeID=" + empid + " AND IsApproved='TRUE' AND '" + theDate + "' BETWEEN StartDate AND EndDate;", staticConnection);
            staticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            if (theReader.Read())
            {
                staticConnection.Close();
                return true;
            }
            else
            {
                staticConnection.Close();
                return false;
            }
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
            staticConnection.Close();
            return list;
        }
        public void Delete()
        {
            SqlCommand theCommand = new("DELETE FROM TimeOffRequest WHERE ID=" + ID + ";", theConnection);
            theConnection.Open();
            theCommand.ExecuteNonQuery();
            theConnection.Close();
            ID = 0;
        }
        public Employee GetEmployee()
        {
            return new Employee(EmployeeID);
        }
        public Employee GetManager()
        {
            if (ManagerID == 0)
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
            EndDate = theReader.GetDateTime(3);
            IsApproved = theReader.GetBoolean(4);
            ManagerID = theReader.IsDBNull(5) ? 0 : theReader.GetInt32(5);
            Notes = theReader.IsDBNull(6) ? "" : theReader.GetString(6);
            theConnection.Close();
        }
        public TimeOffRequest(int employeeID, DateTime startDate, DateTime endDate, string notes)
        {
            ID = 0;
            EmployeeID = employeeID;
            StartDate = startDate;
            EndDate = endDate;
            IsApproved = false;
            ManagerID = 0;
            Notes = notes;
        }
        public String Save()
        {
            SqlCommand theCommand = new("SP_Update_TimeOffRequest", theConnection);
            theCommand.CommandType = System.Data.CommandType.StoredProcedure;
            theCommand.Parameters.AddWithValue("@ID", ID);
            theCommand.Parameters.AddWithValue("@EmployeeID", EmployeeID);
            theCommand.Parameters.AddWithValue("@StartDate", StartDate);
            theCommand.Parameters.AddWithValue("@EndDate", EndDate);
            theCommand.Parameters.AddWithValue("@IsApproved", IsApproved);
            theCommand.Parameters.AddWithValue("@ManagerID", (ManagerID==0?DBNull.Value:ManagerID));
            theCommand.Parameters.AddWithValue("@Notes", Notes);
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
