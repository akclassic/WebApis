using SampleApi.Filters;
using SampleApi.Models;
using System;
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
        //string connectionString = ConfigurationManager.ConnectionStrings["sampledbconnection"].ConnectionString;
        string token = Thread.CurrentPrincipal.Identity.Name;
        // GET: api/Employee
        public async Task<HttpResponseMessage> Get()
        {
            BussinessLayer bussinesslayer = new BussinessLayer();
            var userlist = await bussinesslayer.EmployeesList();
            return Request.CreateResponse(HttpStatusCode.OK, userlist);
        }

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
            var user = UserList.Where(u => u.id == id);
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
                using(var context = new sampledbcontext())
                {
                    var emp = new Employee()
                    { 
                        name = employee.name,
                        salary = employee.salary,
                        designation = employee.designation
                    };
                    context.Employee.Add(emp);

                    context.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.Created, employee);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }

        // PUT: api/Employee/5
        public HttpResponseMessage Put(int id, [FromBody]Employee employee)
        {
            try
            {
                using(var context = new sampledbcontext())
                {
                    var emp = context.Employee.FirstOrDefault(e => e.id == id);
                    emp.name = employee.name;
                    emp.salary = employee.salary;
                    emp.designation = employee.designation;
                    context.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, employee);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //// DELETE: api/Employee/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (var context = new sampledbcontext())
                {
                    var employee = context.Employee.FirstOrDefault(emp => emp.id == id);
                    context.Employee.Remove(employee);
                    context.SaveChanges();
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
