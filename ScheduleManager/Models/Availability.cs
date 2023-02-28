namespace ScheduleManager.Models
{
    public class Availability
    {
        private SqlConnection theConnection = new(ConnectionStrings.local);
        public int ID { get; private set; }
        public int EmployeeID { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime MondayStart { get; set; }
        public DateTime MondayEnd { get; set; }
        public DateTime TuesdayStart { get; set; }
        public DateTime TuesdayEnd { get; set; }
        public DateTime WednesdayStart { get; set; }
        public DateTime WednesdayEnd { get; set; }
        public DateTime ThursdayStart { get; set; }
        public DateTime ThursdayEnd { get; set; }
        public DateTime FridayStart { get; set; }
        public DateTime FridayEnd { get; set; }
        public DateTime SaturdayStart { get; set; }
        public DateTime SaturdayEnd { get; set; }
        public DateTime SundayStart { get; set; }
        public DateTime SundayEnd { get; set; }
        public Employee GetEmployee()
        {
            return new Employee(EmployeeID);
        }
        public Availability (int employeeID, DateTime effectiveDate, DateTime mondayStart, DateTime mondayEnd, DateTime tuesdayStart, DateTime tuesdayEnd, DateTime wednesdayStart, DateTime wednesdayEnd, DateTime thursdayStart, DateTime thursdayEnd, DateTime fridayStart, DateTime fridayEnd, DateTime saturdayStart, DateTime saturdayEnd, DateTime sundayStart, DateTime sundayEnd)
        {
            EmployeeID = employeeID;
            EffectiveDate = effectiveDate;
            MondayStart = mondayStart;
            MondayEnd = mondayEnd;
            TuesdayStart = tuesdayStart;
            TuesdayEnd = tuesdayEnd;
            WednesdayStart = wednesdayStart;
            WednesdayEnd = wednesdayEnd;
            ThursdayStart = thursdayStart;
            ThursdayEnd = thursdayEnd;
            FridayStart = fridayStart;
            FridayEnd = fridayEnd;
            SaturdayStart = saturdayStart;
            SaturdayEnd = saturdayEnd;
            SundayStart = sundayStart;
            SundayEnd = sundayEnd;
        }   
        public Availability (int theID)
        {
            ID = theID;
            SqlConnection theConnection = new(ConnectionStrings.local);
            theConnection.Open();
            SqlCommand theCommand = new("SELECT * FROM Availability WHERE ID = " + theID, theConnection);
            SqlDataReader theReader = theCommand.ExecuteReader();
            theReader.Read();
            EmployeeID = theReader.GetInt32(1);
            EffectiveDate = theReader.GetDateTime(2);
            MondayStart = theReader.GetDateTime(3);
            MondayEnd = theReader.GetDateTime(4);
            TuesdayStart = theReader.GetDateTime(5);
            TuesdayEnd = theReader.GetDateTime(6);
            WednesdayStart = theReader.GetDateTime(7);
            WednesdayEnd = theReader.GetDateTime(8);
            ThursdayStart = theReader.GetDateTime(9);
            ThursdayEnd = theReader.GetDateTime(10);
            FridayStart = theReader.GetDateTime(11);
            FridayEnd = theReader.GetDateTime(12);
            SaturdayStart = theReader.GetDateTime(13);
            SaturdayEnd = theReader.GetDateTime(14);
            SundayStart = theReader.GetDateTime(15);
            SundayEnd = theReader.GetDateTime(16);
        }
        public static List<Availability> GetList()
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            List<Availability> list = new();
            SqlCommand theCommand = new("SELECT ID From Availability;", staticConnection);
            staticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while (theReader.Read())
            {
                list.Add(new Availability(theReader.GetInt32(0)));
            }
            staticConnection.Close();
            return list;
        }
        public string Save()
        {
            SqlCommand theCommand;
            if (ID == 0)
            {
                theCommand = new SqlCommand("INSERT INTO Availability ('EmployeeID', 'EffectiveDate', 'MondayStart', 'MondayEnd', 'TuesdayStart', 'TuesdayEnd', 'WednesdayStart', 'WednesdayEnd', 'ThursdayStart', 'ThursdayEnd', 'FridayStart', 'FridayEnd', 'SaturdayStart', 'SaturdayEnd', 'SundayStart', 'SundayEnd') OUTPUT INSERTED.ID VALUES (" + EmployeeID + ", '" + EffectiveDate + "', '" + MondayStart + "', '" + MondayEnd + "', '" + TuesdayStart + "', '" + TuesdayEnd + "', '" + WednesdayStart + "', '" + WednesdayEnd + "', '" + ThursdayStart + "', '" + ThursdayEnd + "', '" + FridayStart + "', '" + FridayEnd + "', '" + SaturdayStart + "', '" + SaturdayEnd + "', '" + SundayStart + "', '" + SundayEnd + "');", theConnection);
                theConnection.Open();
                ID = Convert.ToInt32(theCommand.ExecuteScalar());
                theConnection.Close();
                return "Success, the ID of the new record is " + ID;
            }
            else
            {
                theCommand = new SqlCommand("UPDATE Availability SET EmployeeID=" + EmployeeID + ", EffectiveDate='" + EffectiveDate + "', MondayStart='" + MondayStart + "', MondayEnd='" + MondayEnd + "', TuesdayStart='" + TuesdayStart + "', TuesdayEnd='" + TuesdayEnd + "', WednesdayStart='" + WednesdayStart + "', WednesdayEnd='" + WednesdayEnd + "', ThursdayStart='" + ThursdayStart + "', ThursdayEnd='" + ThursdayEnd + "', FridayStart='" + FridayStart + "', FridayEnd='" + FridayEnd + "', SaturdayStart='" + SaturdayStart + "', SaturdayEnd='" + SaturdayEnd + "', SundayStart='" + SundayStart + "', SundayEnd='" + SundayEnd + "' WHERE ID=" + ID + ";", theConnection);
                String message;
                try
                {
                    theConnection.Open();
                    theCommand.ExecuteNonQuery();
                    message= "The row was successfully updated.";
                } catch (Exception ex)
                {
                    message= "The row was not successfully updated. Error: " + ex.Message;
                } finally
                {
                    theConnection.Close();
                }
                return message;
            }
        }
    }
}
