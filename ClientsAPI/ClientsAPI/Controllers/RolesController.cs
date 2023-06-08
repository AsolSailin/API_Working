using ClientsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClientsAPI.Controllers
{
    public class RolesController : ApiController
    {
        DailyCRUDEntities db = new DailyCRUDEntities();

        [HttpGet]
        [Route("api/Roles/GetAllRoles")]
        public IHttpActionResult GetAllRole()
        {
            return Ok(db.Role.Select(r => new
            {
                r.Name
            }));
        }

        [HttpGet]
        [Route("api/Roles/{RoleName}")]
        public IHttpActionResult GetRole(string RoleName)
        {
            var role = db.Role.FirstOrDefault(x => x.Name == RoleName);

            if (role == null)
            {
                return BadRequest("Такой роли не существует!");
            }

            return Ok(new
            {
                role.Id,
                role.Name
            });
        }
    }
}
