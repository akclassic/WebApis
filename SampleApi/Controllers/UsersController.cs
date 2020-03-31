using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SampleApi.Models;
using System.Web.Http.Cors;

namespace SampleApi.Controllers
{
    [EnableCors(origins: "http://localhost:61577", headers:"*",methods:"*")]
    public class UsersController : ApiController
    {
        // GET: api/Users
        public HttpResponseMessage Get()
        {
            BussinessLayer bussinessLayer = new BussinessLayer();
            var UserList = bussinessLayer.UsersList;
            return Request.CreateResponse(HttpStatusCode.OK, UserList);
        }

        // GET: api/Users/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Users
        public HttpResponseMessage Post([FromBody]Users data)
        {
            BussinessLayer bussinessLayer = new BussinessLayer();
            var UserList = bussinessLayer.UsersList;
            bool isValidUser = UserList.Any(u => u.Username == data.Username && u.Password == data.Password);
            if (isValidUser)
            {
                Users user = UserList.Where(u => u.Username == data.Username && u.Password == data.Password).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, new Exception());
            }
        }

        // PUT: api/Users/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Users/5
        public void Delete(int id)
        {
        }
    }
}
