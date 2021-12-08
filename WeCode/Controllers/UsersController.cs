using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeCode;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WeCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly APPZWeCodeContext _context;

        public UsersController(APPZWeCodeContext context)
        {
            _context = context;
        }

        /// <summary>
        /// блять впадлу робити ДТОшки, тому з ріквесту непотрібні параметри просто видаляйте
        /// </summary>
        /// <remarks>
        /// "ХОРОШИЙ РІКВЕСТ":
        ///     POST /signup
        ///     {
        ///         "roleId": 2, 
        ///         "name": "Ivan",
        ///         "surname": "Boichuk",
        ///         "email": "name@email",
        ///         "dateOfBirth": "2021-12-04T08:15:16.601Z",
        ///         "password": "string",
        ///     }
        /// </remarks>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("signup")]
        public async Task<ActionResult<User>> SignUpUser(User user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                return Conflict();
            }
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }
        [HttpPost("signin")]
        public async Task<ActionResult<User>> SignInUser(string email, string password)
        {

            
            try
            {
                if (_context.Users.Any(u => u.Email == email))
                {
                    var user = _context.Users.FirstOrDefault(u => u.Email == email);
                    if (user.Password == password)
                    {
                        return CreatedAtAction("GetUser", new { id = user.UserId }, user);
                    }
                    else
                    {
                        return Unauthorized("Wrong password");
                    }
                }
            }
            catch (DbUpdateException)
            {
                    throw;
            }
            return NotFound();

        }
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.
                ToListAsync();
        }


        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
 
        /// <summary>
        /// Return Tasks created by User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Tasks/{id}")]
        public async Task<ActionResult<IEnumerable<Task>>> GetUserTasks(int id)
        {
            return await _context.Tasks.
                Where(u => u.CreatedBy == id)
                .ToListAsync();
        }
        // GET: api/Users/5
        /// <summary>
        /// Return Tasks created by User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("TaskResults/{id}")]
        public async Task<ActionResult<IEnumerable<TaskResult>>> GetUserTaskResults(int id)
        {
            return await _context.TaskResults.
                Where(u => u.SubmittedBy == id)
                .ToListAsync();
        }
        // GET: api/Users/5
        /// <summary>
        /// Return Tasks created by User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Stat/{id}")]
        public async Task<Statistic> GetUserStat(int id)
        {
            var taskRes =  await _context.TaskResults.
                Where(u => u.SubmittedBy == id)
                .ToListAsync();

            int? numberOfCompTasks = 0;
            int? numberOfSuccessTasks = 0;
            int? averMark = 0;
            if (taskRes != null)
            {
                numberOfCompTasks = taskRes?.Count;
                numberOfSuccessTasks = taskRes.
                    Where(t => t.Score == 100).
                    Count();
                if(numberOfCompTasks != 0)
                {
                    averMark = (int)taskRes.Sum(t => t.Score) / numberOfCompTasks;
                }
            }

            return new Statistic() 
            { 
                NumberOfCompTasks  =  numberOfCompTasks ,
                NumberOfSuccessTasks = numberOfSuccessTasks,
                AverMark = averMark
            };

        }
        public class Statistic
        {
            public int? NumberOfCompTasks { get; set; }
            public int? NumberOfSuccessTasks { get; set; }
            public int? AverMark { get; set; }
        }
        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Edit User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<User>> PostUser(User user)
        //{
        //    _context.Users.Add(user);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (UserExists(user.UserId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        //}

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
