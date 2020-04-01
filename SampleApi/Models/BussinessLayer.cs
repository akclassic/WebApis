using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Models
{
    public class BussinessLayer
    {
        public async Task<IEnumerable<Users>> UsersList()
        {
                //string connectionString = ConfigurationManager.ConnectionStrings["sampledbconnection"].ConnectionString;
                List<Users> users = new List<Users>();
                
                using(var context = new sampledbcontext())
                {
                    users = await context.Users.ToListAsync();
                
                }
                return users;
        }

        public async Task<IEnumerable<Employee>> EmployeesList()
        {

            //string connectionString = ConfigurationManager.ConnectionStrings["sampledbconnection"].ConnectionString;
            List<Employee> employees = new List<Employee>();
            
            using (var context = new sampledbcontext())
            {
                employees = await context.Employee.ToListAsync();
            }
            return employees;

        }
    }
}