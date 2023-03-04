namespace ScheduleManager.Models
{
    public class Shift
    {
        private SqlConnection theConnection = new(ConnectionStrings.local);
        public int ID { get; private set; }
        public bool IsOpen { get; set; }
        public int EmployeeID { get; set; }
        public DateTime ShiftDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public String Role { get; set; }
        public String Notes { get; set; }
        public Shift(int theID)
        {
            ID = theID;
            SqlCommand theCommand = new("SELECT * FROM Shift WHERE ID=" + theID + ";", theConnection);
            theConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            theReader.Read();
            IsOpen = theReader.GetBoolean(1);
            EmployeeID = theReader.GetInt32(2);
            ShiftDate = theReader.GetDateTime(3);
            StartTime = theReader.GetDateTime(4);
            EndTime = theReader.GetDateTime(5);
            Role = theReader.GetString(6);
            Notes = theReader.IsDBNull(7) ? "" : theReader.GetString(7);
            theConnection.Close();
        }
        public Shift(bool isOpen, int employeeID, DateTime shiftDate, DateTime startTime, DateTime endTime, string role, string notes)
        {
            IsOpen = isOpen;
            EmployeeID = employeeID;
            ShiftDate = shiftDate;
            StartTime = startTime;
            EndTime = endTime;
            Role = role;
            Notes = notes;
        }
        public string Save()
        {
            SqlCommand theCommand;
            if (ID == 0)
            {
                theCommand = new SqlCommand("INSERT INTO Shift (IsOpen, EmployeeID, Date, StartTime, EndTime, Role, Notes)" + " OUTPUT INSERTED.ID VALUES ('" + IsOpen.ToString() + "', " + EmployeeID + ", '" + ShiftDate + "', '" + StartTime + "', '" + EndTime + "', '" + Role + "', '" + Notes + "');", theConnection);
                theConnection.Open();
                ID = Convert.ToInt32(theCommand.ExecuteScalar());
                theConnection.Close();
                return "Success, the ID of the new record is " + ID;
            }
            else
            {
                theCommand = new SqlCommand("UPDATE Shift SET IsOpen='" + IsOpen + "', EmployeeID=" + EmployeeID + ", Date='" + ShiftDate + "', StartTime='" + StartTime + "', EndTime='" + EndTime + "', Role='" + Role + "', Notes='" + Notes + "' WHERE ID=" + ID + ";", theConnection);
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
        public Employee GetEmployee()
        {
            return new Employee(EmployeeID);
        }
        public static List<Shift> GetList()
        {
            SqlConnection StaticConnection = new(ConnectionStrings.local);
            List<Shift> list = new();
            SqlCommand theCommand = new("SELECT ID FROM Shift", StaticConnection);
            StaticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while (theReader.Read())
            {
                list.Add(new Shift(theReader.GetInt32(0)));
            }
            StaticConnection.Close();
            return list;
        }
        public static List<Shift> GetScheduleByDate(DateTime beginDate, DateTime endDate)
        {
            SqlConnection StaticConnection = new(ConnectionStrings.local);
            List<Shift> list = new();
            SqlCommand theCommand = new("SELECT ID FROM Shift WHERE ShiftDate BETWEEN '" + beginDate + "' AND '" + endDate + "';", StaticConnection);
            StaticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while (theReader.Read())
            {
                list.Add(new Shift(theReader.GetInt32(0)));
            }
            StaticConnection.Close();
            return list;
        }
        public static List<Shift> GetScheduleByEmployee(DateTime beginDate, DateTime endDate, int EmployeeID)
        {
            SqlConnection StaticConnection = new(ConnectionStrings.local);
            List<Shift> list = new();
            SqlCommand theCommand = new("SELECT ID FROM Shift WHERE Date BETWEEN '" + beginDate + "' AND '" + endDate + "' AND EmployeeID=" + EmployeeID + ";", StaticConnection);
            StaticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while (theReader.Read())
            {
                list.Add(new Shift(theReader.GetInt32(0)));
            }
            StaticConnection.Close();
            return list;
        }
        public static List<Shift> GetScheduleByEmployee(DateTime WeekStartDate, int EmployeeID)
        {
            SqlConnection StaticConnection = new(ConnectionStrings.local);
            List<Shift> list = new();
            SqlCommand theCommand = new("SELECT ID FROM Shift WHERE Date BETWEEN '" + WeekStartDate + "' AND '" + WeekStartDate.AddDays(7) + "' AND EmployeeID=" + EmployeeID + ";", StaticConnection);
            StaticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while (theReader.Read())
            {
                list.Add(new Shift(theReader.GetInt32(0)));
            }
            StaticConnection.Close();
            return list;
        }
        public static List<Shift> GetScheduleByEmployee(int EmployeeID)
        {
            DateTime WeekStartDate = DateTime.Today;
            while(WeekStartDate.DayOfWeek != DayOfWeek.Monday)
            {
                WeekStartDate.AddDays(-1);
            }
            SqlConnection StaticConnection = new(ConnectionStrings.local);
            List<Shift> list = new();
            SqlCommand theCommand = new("SELECT ID FROM Shift WHERE Date BETWEEN '" + WeekStartDate + "' AND '" + WeekStartDate.AddDays(7) + "' AND EmployeeID=" + EmployeeID + ";", StaticConnection);
            StaticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while (theReader.Read())
            {
                list.Add(new Shift(theReader.GetInt32(0)));
            }
            StaticConnection.Close();
            return list;
        }
        public static Shift GetShift(int EmployeeID, DateTime shiftDate)
        {
            SqlConnection StaticConnection = new(ConnectionStrings.local);
            SqlCommand theCommand = new("SELECT ID FROM Shift WHERE ShiftDate='" + shiftDate + "' AND EmployeeID = '" + EmployeeID + ";", StaticConnection);
            StaticConnection.Open();
            try
            {
                int theID = Convert.ToInt32(theCommand.ExecuteScalar());
                StaticConnection.Close();
                return new Shift(theID);
            } catch (Exception ex)
            {
                StaticConnection.Close();
                return null;
            }

        }
    }
}
