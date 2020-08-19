using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using webAPI.Models;

namespace webAPI.Controllers
{
    public class DataController : ApiController
    {
        
        [AllowAnonymous]
        [HttpGet]
        [Route("api/alldata")]
        public IHttpActionResult get()
        {
            return Ok("Server starts ");
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("api/getDetails")]
        public List<EmployeeList> getData()
        {

            CurrencyCalculate currency = new CurrencyCalculate();
            List<EmployeeList> list = new List<EmployeeList>();
            list = currency.CalculateEmpDetails();

            return list;


        }

        [System.Web.Http.Authorize]
        [HttpGet]
        [Route("api/authenticate")]
        public IHttpActionResult GetforAuthendicate()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("Hello " + identity.Name);
        }
        [System.Web.Http.Authorize(Roles = "admin")]
        [HttpGet]
        [Route("api/authorize")]
        public IHttpActionResult GetForAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims.Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value);
            return Ok("hai " + identity.Name + "Roles" + string.Join("", roles.ToList()));

        }
    }
}
