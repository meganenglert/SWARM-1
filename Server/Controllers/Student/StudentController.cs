using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SWARM.EF.Data;
using SWARM.EF.Models;
using SWARM.Server.Models;
using SWARM.Shared;
using SWARM.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Telerik.DataSource;
using Telerik.DataSource.Extensions;

namespace SWARM.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController: BaseController<Student>, iBaseController<Student>
    {
        public StudentController(SWARMOracleContext context, IHttpContextAccessor httpContextAccessor): base(context, httpContextAccessor)
        {}

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            List<Student> lstStudents = await _context.Students.OrderBy(x => x.StudentId).ToListAsync();
            return Ok(lstStudents);
        }

        [HttpGet]
        [Route("Get/{key}")]
        public async Task<IActionResult> Get(int key)
        {
            return StatusCode(StatusCodes.Status417ExpectationFailed,
                "Query must take the form of 'Get/{StudentId}/{SchoolId}'"
            );
        }

        [HttpGet]
        [Route("Get/{stuId}/{sch}")]
        public async Task<IActionResult> Get(int stuId, int sch)
        {
            Student stu = _context.Students.Where(stu => stu.StudentId==stuId && stu.SchoolId==sch).FirstOrDefault();
            return Ok(stu);
        }

        [HttpDelete]
        [Route("Delete/{key}")]
        public async Task<IActionResult> Delete(int key)
        {
            return StatusCode(StatusCodes.Status417ExpectationFailed,
                "Query must take the form of 'Get/{StudentId}/{SchoolId}'"
            );
        }

        [HttpDelete]
        [Route("Delete/{stuId}/{sch}")]
        public async Task<IActionResult> Delete(int stuId, int sch)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var stu = _context.Students.Where(stu => stu.StudentId==stuId && stu.SchoolId==sch).FirstOrDefault();
                if (stu == null) return StatusCode(StatusCodes.Status404NotFound, "Given Student not found.");

                _context.Students.Remove(stu);
                await _context.SaveChangesAsync();
                trans.Commit();
                return Ok();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody] Student _new)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var existStudent = await _context.Students.Where(x => x.StudentId == _new.StudentId && x.SchoolId == _new.SchoolId).FirstOrDefaultAsync();

                if (existStudent == null) {
                    _context.Students.Add(_new);
                    await _context.SaveChangesAsync();
                    trans.Commit();
                    return Ok();
                }
                return StatusCode(StatusCodes.Status400BadRequest,
                        "Invalid post request. Student already exists."
                );
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        [HttpPut]
        [Route("Put")]
        public async Task<IActionResult> Put([FromBody] Student _new)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var existStudent = await _context.Students.Where(x => x.StudentId == _new.StudentId && x.SchoolId == _new.SchoolId).FirstOrDefaultAsync();

                if (existStudent == null) {
                    _context.Students.Add(_new);
                    await _context.SaveChangesAsync();
                    trans.Commit();
                    return Ok();
                }
                else {
                    existStudent.Salutation = _new.Salutation;
                    existStudent.FirstName = _new.FirstName;
                    existStudent.LastName = _new.LastName;
                    existStudent.StreetAddress = _new.StreetAddress;
                    existStudent.Zip = _new.Zip;
                    existStudent.Phone = _new.Phone;
                    existStudent.Employer = _new.Employer;
                    existStudent.RegistrationDate = _new.RegistrationDate;
                    _context.Students.Update(existStudent);
                    await _context.SaveChangesAsync();
                    trans.Commit();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
