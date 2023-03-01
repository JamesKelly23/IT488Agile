namespace ScheduleManager.Models
{
    public class PickupRequest
    {
        private SqlConnection theConnection = new(ConnectionStrings.local); 
        public int ShiftID { get; private set; }
        public int EmployeeID { get; private set; }
        public bool IsApproved { get; set; }
        public int ManagerID { get; set; }
        public Boolean IsNew { get; set; }
        public PickupRequest(int shiftID, int employeeID) 
        {
            SqlCommand theCommand = new("SELECT * FROM PickupRequest WHERE ShiftID=" + shiftID + " AND EmployeeID=" + employeeID + ";", theConnection);
            theConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            theReader.Read();
            ShiftID = shiftID;
            EmployeeID = employeeID;
            IsApproved = theReader.GetBoolean(2);
            ManagerID = theReader.IsDBNull(3) ? 0 : theReader.GetInt32(3);
            theConnection.Close();
            IsNew = false;
        }
        public static List<PickupRequest> GetList()
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            SqlCommand theCommand = new("SELECT ShiftID, EmployeeID FROM PickupRequest;", staticConnection);
            List<PickupRequest> list = new();
            staticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while(theReader.Read())
            {
                list.Add(new PickupRequest(theReader.GetInt32(0), theReader.GetInt32(1)));
            }
            staticConnection.Close();
            return list;
        }
        public Shift GetShift()
        {
            return new Shift(ShiftID);
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
            return new Employee(ManagerID);
        }
        public PickupRequest(int shiftID, int employeeID, bool isApproved, int managerID)
        {
            IsNew = true;
            ShiftID = shiftID;
            EmployeeID = employeeID;
            IsApproved = isApproved;
            ManagerID = managerID;
        }
        public String Save()
        {
            SqlCommand theCommand;
            if(IsNew) 
            {
                theCommand = new SqlCommand("INSERT INTO PickupRequest ('ShiftID', 'EmployeeID', 'IsApproved', 'ManagerID') VALUES (" + ShiftID + ", " + EmployeeID + ", '" + IsApproved + "', " + (ManagerID == 0 ? "NULL" : ManagerID) + ");", theConnection);
                theConnection.Open();
                theCommand.ExecuteNonQuery();
                theConnection.Close();
                return "Success. Record added.";
            }
            else
            {
                theCommand = new SqlCommand("UPDATE PickupRequest SET IsApproved='" + IsApproved + "', ManagerID=" + (ManagerID==0 ? "NULL" : ManagerID) + " WHERE ShiftID=" + ShiftID + " AND EmployeeID=" + EmployeeID +";", theConnection);
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
