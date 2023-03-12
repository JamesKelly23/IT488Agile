using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using ScheduleManager.Models;

namespace ScheduleManager.Controllers
{
    public class ScheduleViewer : Controller
	{
		public class ScheduleViewerController
		{
			private SqlConnection conn;
			private string connectionString;

			public ScheduleViewerController(string connectionString)
			{
				this.connectionString = connectionString;
			}

			public List<Schedule> GetSchedules()
			{
				List<Schedule> schedules = new List<Schedule>();

				try
				{
					using (conn = new SqlConnection(connectionString))
					{
						conn.Open();

						SqlCommand cmd = new SqlCommand("SELECT * FROM Schedule", conn);

						SqlDataReader reader = cmd.ExecuteReader();

						while (reader.Read())
						{
							Schedule schedule = new Schedule();

							schedule.Id = (int)reader["Id"];
							schedule.Date = (DateTime)reader["Date"];
							schedule.StartTime = (DateTime)reader["StartTime"];
							schedule.EndTime = (DateTime)reader["EndTime"];
							schedule.Role = reader["Role"].ToString();
							schedule.Notes = reader["Notes"].ToString();

							schedules.Add(schedule);
						}

						reader.Close();
					}
				}
				catch (Exception ex)
				{
					// Handle exception
				}
				finally
				{
					conn.Close();
				}

				return schedules;
			}

			public void AddSchedule(Schedule schedule)
			{
				try
				{
					using (conn = new SqlConnection(connectionString))
					{
						conn.Open();

						SqlCommand cmd = new SqlCommand("INSERT INTO Schedule (Date, StartTime, EndTime, Role, Notes) " +
							"VALUES (@Date, @StartTime, @EndTime, @Role, @Notes)", conn);

						cmd.Parameters.AddWithValue("@Date", schedule.Date);
						cmd.Parameters.AddWithValue("@StartTime", schedule.StartTime);
						cmd.Parameters.AddWithValue("@EndTime", schedule.EndTime);
						cmd.Parameters.AddWithValue("@Role", schedule.Role);
						cmd.Parameters.AddWithValue("@Notes", schedule.Notes);

						cmd.ExecuteNonQuery();
					}
				}
				catch (Exception ex)
				{
					// Handle exception
				}
				finally
				{
					conn.Close();
				}
			}

			public void UpdateSchedule(Schedule schedule)
			{
				try
				{
					using (conn = new SqlConnection(connectionString))
					{
						conn.Open();

						SqlCommand cmd = new SqlCommand("UPDATE Schedule " +
							"SET Date = @Date, StartTime = @StartTime, EndTime = @EndTime, " +
							"Role = @Role, Notes = @Notes " +
							"WHERE Id = @Id", conn);

						cmd.Parameters.AddWithValue("@Id", schedule.Id);
						cmd.Parameters.AddWithValue("@Date", schedule.Date);
						cmd.Parameters.AddWithValue("@StartTime", schedule.StartTime);
						cmd.Parameters.AddWithValue("@EndTime", schedule.EndTime);
						cmd.Parameters.AddWithValue("@Role", schedule.Role);
						cmd.Parameters.AddWithValue("@Notes", schedule.Notes);

						cmd.ExecuteNonQuery();
					}
				}
				catch (Exception ex)
				{
					// Handle exception
				}
				finally
				{
					conn.Close();
				}
			}

			public void DeleteSchedule(int id)
			{
				try
				{
					using (conn = new SqlConnection(connectionString))
					{
						conn.Open();

						SqlCommand cmd = new SqlCommand("DELETE FROM Schedule WHERE Id = @Id", conn);

						cmd.Parameters.AddWithValue("@Id", id);

						cmd.ExecuteNonQuery();
					}
				}
				catch (Exception ex)
				{
					// Handle exception
				}
				finally
				{
					conn.Close();
				}
			}
		}

		public class Schedule
		{
			internal readonly object Notes;

			public int Id { get; set; }
			public DateTime Date { get; set; }
			public DateTime StartTime { get; set; }
			public DateTime EndTime { get; set; }
			public String Role { get; set; }
			public String Notes { get; set; }
		}
			
