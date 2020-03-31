using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SampleApi.Models
{
    public class BussinessLayer
    {
        public IEnumerable<Users> UsersList
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["sampledbconnection"].ConnectionString;
                List<Users> users = new List<Users>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("Select * from Users", con))
                    {
                        con.Open();
                        SqlDataReader sqlDataReader = command.ExecuteReader();
                        while (sqlDataReader.Read())
                        {
                            Users user = new Users();
                            user.Id = Convert.ToInt32(sqlDataReader["id"]);
                            user.Email = sqlDataReader["email"].ToString();
                            user.Username = sqlDataReader["username"].ToString();
                            user.Password = sqlDataReader["password"].ToString();
                            users.Add(user);
                        }
                    }
                }
                return users;
            }
        }

        public async Task<IEnumerable<Employee>> EmployeesList()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["sampledbconnection"].ConnectionString;
            List<Employee> employees = new List<Employee>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("Select * from Employee", con))
                {
                    con.Open();
                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Employee employee = new Employee();
                        employee.Id = Convert.ToInt32(sqlDataReader["id"]);
                        employee.Name = sqlDataReader["name"].ToString();
                        employee.Salary = Convert.ToDecimal(sqlDataReader["salary"]);
                        employee.Designation = sqlDataReader["designation"].ToString();
                        employees.Add(employee);
                    }
                }
            }
            return employees;

        }
    }
}