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
    public class EnrollmentController: BaseController<Enrollment>, iBaseController<Enrollment>
    {
        public EnrollmentController(SWARMOracleContext context, IHttpContextAccessor httpContextAccessor): base(context, httpContextAccessor)
        {}

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            List<Enrollment> lstEnrollments = await _context.Enrollments.OrderBy(x => x.StudentId).ToListAsync();
            return Ok(lstEnrollments);
        }

        [HttpGet]
        [Route("Get/{key}")]
        public async Task<IActionResult> Get(int key)
        {
            return StatusCode(StatusCodes.Status417ExpectationFailed,
                "Query must take the form of 'Get/{StudentId}/{SectionId}/{SchoolId}"
            );
        }

        [HttpGet]
        [Route("Get/{stu}/{sec}/{sch}")]
        public async Task<IActionResult> Get(int stu, int sec, int sch)
        {
            Enrollment enr = _context.Enrollments.Where(enr => enr.StudentId==stu && enr.SectionId==sec && enr.SchoolId==sch).FirstOrDefault();
            return Ok(enr);
        }

        [HttpDelete]
        [Route("Delete/{key}")]
        public async Task<IActionResult> Delete(int key)
        {
            return StatusCode(StatusCodes.Status417ExpectationFailed,
                "Query must take the form of 'Get/{StudentId}/{SectionId}/{SchoolId}"
            );
        }

        [HttpDelete]
        [Route("Delete/{stu}/{sec}/{sch}")]
        public async Task<IActionResult> Delete(int stu, int sec, int sch)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var enr = _context.Enrollments.Where(enr => enr.StudentId==stu && enr.SectionId==sec && enr.SchoolId==sch).FirstOrDefault();
                if (enr == null) return StatusCode(StatusCodes.Status404NotFound, "Given enrollment not found.");

                _context.Enrollments.Remove(enr);
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
        public async Task<IActionResult> Post([FromBody] Enrollment _new)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var existEnrollment = await _context.Enrollments.Where(x => x.StudentId == _new.StudentId && x.SectionId == _new.SectionId && x.SchoolId == _new.SchoolId).FirstOrDefaultAsync();

                if (existEnrollment == null) {
                    _context.Enrollments.Add(_new);
                    await _context.SaveChangesAsync();
                    trans.Commit();
                    return Ok();
                }
                return StatusCode(StatusCodes.Status400BadRequest,
                        "Invalid post request. Enrollment already exists."
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
        public async Task<IActionResult> Put([FromBody] Enrollment _new)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var existEnrollment = await _context.Enrollments.Where(x => x.StudentId == _new.StudentId && x.SectionId == _new.SectionId && x.SchoolId == _new.SchoolId).FirstOrDefaultAsync();

                if (existEnrollment == null) {
                    _context.Enrollments.Add(_new);
                    await _context.SaveChangesAsync();
                    trans.Commit();
                    return Ok();
                }
                else {
                    existEnrollment.EnrollDate = _new.EnrollDate;
                    existEnrollment.FinalGrade = _new.FinalGrade;
                    _context.Enrollments.Update(existEnrollment);
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
