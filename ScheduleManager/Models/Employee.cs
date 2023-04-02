using MessagePack.Formatters;

namespace ScheduleManager.Models
{
    public class ConnectionStrings
    {
        public static string local = "Data Source=localhost\\SQLEXPRESS; Integrated Security=true; Initial Catalog=ScheduleManager;";
    }
    public class Employee
    {
        private SqlConnection theConnection = new(ConnectionStrings.local);
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
            if(theID !=0)
            {
                ID = theID;
                theConnection.Open();
                SqlCommand theCommand = new("SELECT * FROM Employee WHERE ID = " + theID, theConnection);
                SqlDataReader theReader = theCommand.ExecuteReader();
                theReader.Read();
                FirstName = theReader.GetString(1);
                LastName = theReader.GetString(2);
                RankID = theReader.GetInt32(3);
                Password = theReader.GetString(4);
                Username = theReader.GetString(5);
                Phone = theReader.GetString(6);
                Email = theReader.GetString(7);
                DOB = theReader.GetDateTime(8);
                theConnection.Close();
            } else
            {
                FirstName = "";
                LastName = "";
                RankID = 0;
                Password = "";
                Username = "";
                Email = "";
                Phone = "";
                DOB = DateTime.Now;
            }
        }
        public string Delete()
        {
            if(ID !=0)
            {
                    SqlCommand theCommand = new SqlCommand("DELETE FROM Employee WHERE ID=" + ID + ";", theConnection);
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
            return "There is no record to delete.";
        }
        public Rank GetRank()
        {
            return new Rank(RankID);
        }
        public string Save()
        {
            SqlCommand theCommand = new("SP_Update_Employee", theConnection);
            theCommand.CommandType = System.Data.CommandType.StoredProcedure;
            theCommand.Parameters.AddWithValue("@ID", ID);
            theCommand.Parameters.AddWithValue("@FirstName", FirstName);
            theCommand.Parameters.AddWithValue("@LastName", LastName);
            theCommand.Parameters.AddWithValue("@RankID", RankID);
            theCommand.Parameters.AddWithValue("@Password", Password);
            theCommand.Parameters.AddWithValue("@Username", Username);
            theCommand.Parameters.AddWithValue("@Phone", Phone);
            theCommand.Parameters.AddWithValue("@Email", Email);
            theCommand.Parameters.AddWithValue("@DateOfBirth", DOB);
            SqlParameter newParameter = new("@NewID", 0);
            newParameter.Direction = System.Data.ParameterDirection.Output;
            theCommand.Parameters.Add(newParameter);
            String message = "The row was successfully updated.";
            bool success = false;
            try
            {
                theConnection.Open();
                theCommand.ExecuteNonQuery();
                if (ID == 0)
                {
                    ID = Convert.ToInt32(theCommand.Parameters["@NewID"].Value);
                    success = true;
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
            if(success)
            {
                Availability newAvail = new(ID, DateTime.Now);
                newAvail.Save();
            }
            return message;
        }
        public static void Delete(int id)
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            SqlCommand theCommand = new("DELETE FROM Employee WHERE ID=" + id + ";", staticConnection);
            staticConnection.Open();
            try
            {
                theCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                String theMessage = ex.Message;
            }
            finally
            {
                staticConnection.Close();
            }
        }
        public static int ValidateLogin(string userName, string password)
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            SqlCommand theCommand = new("SELECT ID FROM Employee WHERE Username='" + userName + "' AND Password='" + password + "';", staticConnection);
            staticConnection.Open();
            int theID;
            try
            {
                theID = Convert.ToInt32(theCommand.ExecuteScalar());
            }
            catch(Exception ex)
            {
                theID = 0;
                Console.WriteLine(ex.Message);
            } 
            finally
            {
                staticConnection.Close();
            }
            return theID;
        }
        public static List<Employee> GetList()
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            List<Employee> list = new();
            SqlCommand theCommand = new("SELECT ID From Employee;", staticConnection);
            staticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while (theReader.Read())
            {
                list.Add(new Employee(theReader.GetInt32(0)));
            }
            staticConnection.Close();
            return list;
        }
        public static List<Employee> GetEmployeesOfLowerRank(int MaxRank)
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            List<Employee> list = new();
            SqlCommand theCommand = new("SELECT ID From Employee WHERE RankID <= " + MaxRank + ";", staticConnection);
            staticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while (theReader.Read())
            {
                list.Add(new Employee(theReader.GetInt32(0)));
            }
            staticConnection.Close();
            return list;
        }
        public Availability GetCurrentAvailability()
        {
            SqlCommand theCommand = new("SELECT ID FROM Availability WHERE EmployeeID=" + ID + " ORDER BY EffectiveDate DESC;", theConnection);
            theConnection.Open();
            Availability theResult = new Availability(Convert.ToInt32(theCommand.ExecuteScalar()));
            theConnection.Close();
            return theResult;
        }
    }
}
