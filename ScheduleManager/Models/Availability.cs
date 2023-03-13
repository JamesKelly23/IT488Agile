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
        public string GetDayString(DayOfWeek theDay)
        {
            if (!IsAvailable(theDay))
            {
                return "Not Available";
            }
            return GetStart(theDay).ToString("t") + " - " + GetEnd(theDay).ToString("t");
        }
        public Boolean IsAvailable(DayOfWeek theDay)
        {
            return (!(GetStart(theDay).ToString("t") == GetEnd(theDay).ToString("t")));
        }
        public Availability(int employeeiD, DateTime effectiveDate)
        {
            ID = 0;
            EmployeeID= employeeiD;
            EffectiveDate = effectiveDate;
            DateTime midnight = Convert.ToDateTime("1/1/2001 00:00:00");
            foreach (DayOfWeek theDay in GetDaysOfWeek())
            {
                SetStart(theDay, midnight);
                SetEnd(theDay, midnight);
            }
        }
        public DateTime GetStart(DayOfWeek theDay)
        {
            switch(theDay)
            {
                case DayOfWeek.Monday: return MondayStart;
                case DayOfWeek.Tuesday: return TuesdayStart;
                case DayOfWeek.Wednesday: return WednesdayStart;
                case DayOfWeek.Thursday: return ThursdayStart;
                case DayOfWeek.Friday: return FridayStart;
                case DayOfWeek.Saturday: return SaturdayStart;
                case DayOfWeek.Sunday: return SundayStart;
                default: return new DateTime();
            }
        }
        public DateTime GetEnd(DayOfWeek theDay)
        {
            switch(theDay)
            {
                case DayOfWeek.Monday: return MondayEnd;
                case DayOfWeek.Tuesday: return TuesdayEnd;
                case DayOfWeek.Wednesday: return WednesdayEnd;
                case DayOfWeek.Thursday: return ThursdayEnd;
                case DayOfWeek.Friday: return FridayEnd;
                case DayOfWeek.Saturday:return SaturdayEnd;
                case DayOfWeek.Sunday: return SundayEnd;
                default: return new DateTime();
            }
        }
        public void SetStart(DayOfWeek theDay, DateTime startDate)
        {
            switch(theDay)
            {
                case DayOfWeek.Monday: MondayStart = startDate; break;
                case DayOfWeek.Tuesday: TuesdayStart = startDate; break;
                case DayOfWeek.Wednesday: WednesdayStart = startDate; break;
                case DayOfWeek.Thursday: ThursdayStart = startDate; break;
                case DayOfWeek.Friday: FridayStart = startDate; break;
                case DayOfWeek.Saturday: SaturdayStart = startDate; break;
                case DayOfWeek.Sunday: SundayStart = startDate; break;
            }
        }
        public void SetEnd(DayOfWeek theDay, DateTime endDate)
        {
            switch (theDay)
            {
                case DayOfWeek.Monday: MondayEnd = endDate; break;
                case DayOfWeek.Tuesday: TuesdayEnd = endDate; break;
                case DayOfWeek.Wednesday: WednesdayEnd = endDate; break;
                case DayOfWeek.Thursday: ThursdayEnd = endDate; break;
                case DayOfWeek.Friday: FridayEnd = endDate; break;
                case DayOfWeek.Saturday: SaturdayEnd = endDate; break;
                case DayOfWeek.Sunday: SundayEnd = endDate; break;
            }
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
            theConnection.Close();
        }
        public static String DayString(DayOfWeek dayOfWeek)
        {
            return dayOfWeek switch
            {
                DayOfWeek.Monday => "Monday",
                DayOfWeek.Tuesday => "Tuesday",
                DayOfWeek.Wednesday => "Wednesday",
                DayOfWeek.Thursday => "Thursday",
                DayOfWeek.Friday => "Friday",
                DayOfWeek.Saturday => "Saturday",
                DayOfWeek.Sunday => "Sunday",
                _ => "",
            };
        }
        public static List<DayOfWeek> GetDaysOfWeek()
        {
            List<DayOfWeek> dayList = new();
            dayList.Add(DayOfWeek.Monday);
            dayList.Add(DayOfWeek.Tuesday);
            dayList.Add(DayOfWeek.Wednesday);
            dayList.Add(DayOfWeek.Thursday);
            dayList.Add(DayOfWeek.Friday);
            dayList.Add(DayOfWeek.Saturday);
            dayList.Add(DayOfWeek.Sunday);
            return dayList;
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
                theCommand = new SqlCommand("INSERT INTO Availability (EmployeeID, EffectiveDate, MondayStart, MondayEnd, TuesdayStart, TuesdayEnd, WednesdayStart, WednesdayEnd, ThursdayStart, ThursdayEnd, FridayStart, FridayEnd, SaturdayStart, SaturdayEnd, SundayStart, SundayEnd) OUTPUT INSERTED.ID VALUES (" + EmployeeID + ", '" + EffectiveDate + "', '" + MondayStart + "', '" + MondayEnd + "', '" + TuesdayStart + "', '" + TuesdayEnd + "', '" + WednesdayStart + "', '" + WednesdayEnd + "', '" + ThursdayStart + "', '" + ThursdayEnd + "', '" + FridayStart + "', '" + FridayEnd + "', '" + SaturdayStart + "', '" + SaturdayEnd + "', '" + SundayStart + "', '" + SundayEnd + "');", theConnection);
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
