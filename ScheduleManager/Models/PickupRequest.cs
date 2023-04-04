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
        public static List<PickupRequest> GetPending()
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            SqlCommand theCommand = new("SELECT ShiftID, EmployeeID FROM PickupRequest WHERE ManagerID is NULL;", staticConnection);
            List<PickupRequest> list = new();
            staticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while (theReader.Read())
            {
                list.Add(new PickupRequest(theReader.GetInt32(0), theReader.GetInt32(1)));
            }
            staticConnection.Close();
            return list;
        }
        public static bool Exists(int employeeID, int shiftID)
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            SqlCommand theCommand = new("SELECT * FROM PickupRequest WHERE EmployeeID=" + employeeID + " AND ShiftID=" + shiftID + ";", staticConnection);
            staticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            if( theReader.Read())
            {
                staticConnection.Close();
                return true;
            } else
            {
                staticConnection.Close();
                return false;
            }
        }
        public List<PickupRequest> GetConflictingRequests()
        {
            List<PickupRequest> theList = new();
            SqlCommand theCommand = new("SELECT ShiftID, EmployeeID FROM PickupRequest WHERE ShiftID =" + ShiftID + " AND EmployeeID != " + EmployeeID, theConnection);
            theConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while(theReader.Read())
            {
                theList.Add(new PickupRequest(theReader.GetInt32(0), theReader.GetInt32(1)));
            }
            theConnection.Close();
            return theList;
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
        public String Delete()
        {
            SqlCommand theCommand = new("DELETE FROM PickupRequest WHERE EmployeeID=" + EmployeeID + " AND ShiftID = " + ShiftID + ";", theConnection);
            theConnection.Open();
            theCommand.ExecuteNonQuery();
            theConnection.Close();
            return "Success";
        }
        public String Save()
        {
            SqlCommand theCommand = new("SP_Update_PickupRequest", theConnection);
            theCommand.CommandType = System.Data.CommandType.StoredProcedure;
            theCommand.Parameters.AddWithValue("@ShiftID", ShiftID);
            theCommand.Parameters.AddWithValue("@EmployeeID", EmployeeID);
            theCommand.Parameters.AddWithValue("@IsApproved", IsApproved);
            theCommand.Parameters.AddWithValue("@ManagerID", (ManagerID==0?DBNull.Value:ManagerID));
            String message = "The row was successfully updated.";
            try
            {
                theConnection.Open();
                theCommand.ExecuteNonQuery();
                if (IsNew)
                {
                    IsNew = false;
                    message = "The row was successfully inserted.";
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
