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
    public class InstructorController: BaseController<Instructor>, iBaseController<Instructor>
    {
        public InstructorController(SWARMOracleContext context, IHttpContextAccessor httpContextAccessor): base(context, httpContextAccessor)
        {}

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            List<Instructor> lstInstructors = await _context.Instructors.OrderBy(x => x.InstructorId).ToListAsync();
            return Ok(lstInstructors);
        }

        [HttpGet]
        [Route("Get/{key}")]
        public async Task<IActionResult> Get(int key)
        {
            return StatusCode(StatusCodes.Status417ExpectationFailed,
                "Query must take the form of 'Get/{InstructorId}/{SchoolId}'"
            );
        }

        [HttpGet]
        [Route("Get/{insId}/{sch}")]
        public async Task<IActionResult> Get(int insId, int sch)
        {
            Instructor ins = _context.Instructors.Where(ins => ins.InstructorId==insId && ins.SchoolId==sch).FirstOrDefault();
            return Ok(ins);
        }

        [HttpDelete]
        [Route("Delete/{key}")]
        public async Task<IActionResult> Delete(int key)
        {
            return StatusCode(StatusCodes.Status417ExpectationFailed,
                "Query must take the form of 'Get/{InstructorId}/{SchoolId}'"
            );
        }

        [HttpDelete]
        [Route("Delete/{insId}/{sch}")]
        public async Task<IActionResult> Delete(int insId, int sch)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var ins = _context.Instructors.Where(ins => ins.InstructorId==insId && ins.SchoolId==sch).FirstOrDefault();
                if (ins == null) return StatusCode(StatusCodes.Status404NotFound, "Given Instructor not found.");

                _context.Instructors.Remove(ins);
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
        public async Task<IActionResult> Post([FromBody] Instructor _new)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var existInstructor = await _context.Instructors.Where(x => x.InstructorId == _new.InstructorId && x.SchoolId == _new.SchoolId).FirstOrDefaultAsync();

                if (existInstructor == null) {
                    _context.Instructors.Add(_new);
                    await _context.SaveChangesAsync();
                    trans.Commit();
                    return Ok();
                }
                return StatusCode(StatusCodes.Status400BadRequest,
                        "Invalid post request. Instructor already exists."
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
        public async Task<IActionResult> Put([FromBody] Instructor _new)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var existInstructor = await _context.Instructors.Where(x => x.InstructorId == _new.InstructorId && x.SchoolId == _new.SchoolId).FirstOrDefaultAsync();

                if (existInstructor == null) {
                    _context.Instructors.Add(_new);
                    await _context.SaveChangesAsync();
                    trans.Commit();
                    return Ok();
                }
                else {
                    existInstructor.Salutation = _new.Salutation;
                    existInstructor.FirstName = _new.FirstName;
                    existInstructor.LastName = _new.LastName;
                    existInstructor.StreetAddress = _new.StreetAddress;
                    existInstructor.Zip = _new.Zip;
                    existInstructor.Phone = _new.Phone;
                    _context.Instructors.Update(existInstructor);
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
