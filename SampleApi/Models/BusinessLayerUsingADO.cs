using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SampleApi.Models
{
    public class BusinessLayerUsingADO
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["sampledbconnection"].ConnectionString;

        public List<Users> GetAllUsers()
        {
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
                        user.id = Convert.ToInt32(sqlDataReader["id"]);
                        user.email = sqlDataReader["email"].ToString();
                        user.username = sqlDataReader["username"].ToString();
                        user.password = sqlDataReader["password"].ToString();
                        users.Add(user);
                    }
                }
            }
            return users;
        }

        public List<Employee> GetAllEmployess()
        {
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
                        employee.id = Convert.ToInt32(sqlDataReader["id"]);
                        employee.name = sqlDataReader["name"].ToString();
                        employee.salary = Convert.ToDecimal(sqlDataReader["salary"]);
                        employee.designation = sqlDataReader["designation"].ToString();
                        employees.Add(employee);
                    }
                }
            }
            return employees;
        }
        public void InsertEmployeeRecord(Employee employee) {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Employee Values( @name, @salary, @designation)";
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("name", employee.name);
                    command.Parameters.AddWithValue("salary", employee.salary);
                    command.Parameters.AddWithValue("designation", employee.designation);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateEmployeeRecord(int id, Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Employee SET name = @name, salary = @salary, designation = @designation WHERE id=@id";
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    command.Parameters.AddWithValue("name", employee.name);
                    command.Parameters.AddWithValue("salary", employee.salary);
                    command.Parameters.AddWithValue("designation", employee.designation);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteEmployeeRecord(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM EMPLOYEE WHERE id=@id";
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}