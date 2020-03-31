using SampleApi.Filters;
using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SampleApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [BasicAuthentication]
    public class EmployeeController : ApiController
    {
        string connectionString = ConfigurationManager.ConnectionStrings["sampledbconnection"].ConnectionString;
        string token = Thread.CurrentPrincipal.Identity.Name;
        // GET: api/Employee
        public async Task<HttpResponseMessage> Get()
        {
            BussinessLayer bussinessLayer = new BussinessLayer();
            var UserList = await bussinessLayer.EmployeesList();
            return Request.CreateResponse(HttpStatusCode.OK, UserList);
        }

        //public HttpResponseMessage Get()
        //{
        //    BussinessLayer bussinessLayer = new BussinessLayer();
        //    var UserList = bussinessLayer.EmployeesList;
        //    return Request.CreateResponse(HttpStatusCode.OK, UserList);
        //}

        //public HttpResponseMessage GetEmployees()
        //{
        //    BussinessLayer bussinessLayer = new BussinessLayer();
        //    var UserList = bussinessLayer.EmployeesList;
        //    return Request.CreateResponse(HttpStatusCode.OK, UserList);
        //}


        // GET: api/Employee/5
        public async Task<HttpResponseMessage> Get(int id)
        {
            BussinessLayer bussinessLayer = new BussinessLayer();
            var UserList = await bussinessLayer.EmployeesList();
            var user = UserList.Where(u => u.Id == id);
            if (user != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "User not found");
            }
        }

        // POST: api/Employee
        public HttpResponseMessage Post([FromBody]Employee employee)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Employee Values( @name, @salary, @designation)";
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("name", employee.Name);
                        command.Parameters.AddWithValue("salary", employee.Salary);
                        command.Parameters.AddWithValue("designation", employee.Designation);
                        command.ExecuteNonQuery();
                    }
                }
                return Request.CreateResponse(HttpStatusCode.Created, employee);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }

        // PUT: api/Employee/5
        public HttpResponseMessage Put(int id, [FromBody]Employee emp)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Employee SET name = @name, salary = @salary, designation = @designation WHERE id=@id";
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        command.Parameters.AddWithValue("name", emp.Name);
                        command.Parameters.AddWithValue("salary", emp.Salary);
                        command.Parameters.AddWithValue("designation", emp.Designation);
                        command.ExecuteNonQuery();
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, emp);
            }
            catch (Exception ex)
            { 
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE: api/Employee/5
        public HttpResponseMessage Delete(int id)
        {
            try
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
                return Request.CreateResponse(HttpStatusCode.OK, "User with id= " + id + " deleted successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
