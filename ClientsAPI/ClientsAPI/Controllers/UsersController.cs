using ClientsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClientsAPI.Controllers
{
    public class UsersController : ApiController
    {
        readonly DailyCRUDEntities db = new DailyCRUDEntities();

        [HttpGet]
        [Route("api/Users/GetAllUsers")]
        public IHttpActionResult GetAllUsers()
        {
            return Ok(db.User.Select(u => new
            {
                u.Id,
                u.Name,
                u.Age,
                u.RoleID,
                Role = u.Role.Name
            }));
        }

        [HttpGet]
        [Route("api/Users/{name}")]
        public IHttpActionResult GetUsers(string name)
        {
            var foundUser = db.User.FirstOrDefault(u => u.Name == name);

            if (foundUser == null)
                return BadRequest("Пользователя с таким именем не существует");
            else
            {
                return Ok(new
                {
                    foundUser.Name,
                    Role = foundUser.Role.Name
                });
            }
        }

        [HttpPost]
        [Route("api/Users/Add")]
        public IHttpActionResult PostUser(User user)
        {
            if (user != null)
            {
                db.User.Add(user);
                db.SaveChanges();
            }

            return Ok();
        }

        [HttpPut]
        [Route("api/Users/Edit/{id}")]
        public IHttpActionResult PutUser(int id, User userEdit)
        {
            var user = db.User.FirstOrDefault(u => u.Id == id);

            try {
                user.Age = userEdit.Age;
                user.RoleID = userEdit.RoleID;
                user.Name = userEdit.Name;

                db.SaveChangesAsync();
            }
            catch
            {
                return BadRequest("Пользоваетель не найден");
            }
            
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;

            return Ok($"Пользователь {user.Name} изменен");
        }

        [HttpDelete]
        [Route("api/Users/Delete/{name}")]
        public IHttpActionResult DeleteUser(string name)
        {
            var user = db.User.FirstOrDefault(u => u.Name == name);

            if (user == null)
                return BadRequest("Пользователь не найден");

            db.User.Remove(user);
            db.SaveChanges();

            return Ok($"Пользователь {name} удален");
        }
    }
}
