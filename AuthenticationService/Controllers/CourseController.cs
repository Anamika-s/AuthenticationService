using ClaimUserService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClaimUserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class CourseController : ControllerBase
    { UserDbContext _context;
        
        public CourseController(UserDbContext context)
        {
            _context = context;

        }
        [HttpGet]
        public ActionResult<List<Course>> Get()
        {
            return _context.Courses.ToList();
        }

        [Authorize]
        [HttpPost]
        public ActionResult<Course> Post(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return Ok();
        }
    }
}
