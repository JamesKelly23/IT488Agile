namespace ScheduleManager.Models
{
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
        public DateOnly DOB { get; set; }
        public Employee(string firstName, string lastName, int rankID, string password, string username, string email, string phone, DateOnly dOB)
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

        }
    }
}
