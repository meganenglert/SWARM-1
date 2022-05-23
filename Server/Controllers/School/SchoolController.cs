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
    public class SchoolController: BaseController<School>, iBaseController<School>
    {
        public SchoolController(SWARMOracleContext context, IHttpContextAccessor httpContextAccessor): base(context, httpContextAccessor)
        {}

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            List<School> lstSchools = await _context.Schools.OrderBy(x => x.SchoolId).ToListAsync();
            return Ok(lstSchools);
        }

        [HttpGet]
        [Route("Get/{key}")]
        public async Task<IActionResult> Get(int key)
        {
            School sch = _context.Schools.Where(sch => sch.SchoolId==key).FirstOrDefault();
            return Ok(sch);
        }

        [HttpDelete]
        [Route("Delete/{key}")]
        public async Task<IActionResult> Delete(int key)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var sch = _context.Schools.Where(sch => sch.SchoolId==key).FirstOrDefault();
                if (sch == null) return StatusCode(StatusCodes.Status404NotFound, "Given School not found.");

                _context.Schools.Remove(sch);
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
        public async Task<IActionResult> Post([FromBody] School _new)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var existSchool = await _context.Schools.Where(x => x.SchoolId == _new.SchoolId).FirstOrDefaultAsync();

                if (existSchool == null) {
                    _context.Schools.Add(_new);
                    await _context.SaveChangesAsync();
                    trans.Commit();
                    return Ok();
                }
                return StatusCode(StatusCodes.Status400BadRequest,
                        "Invalid post request. School already exists."
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
        public async Task<IActionResult> Put([FromBody] School _new)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var existSchool = await _context.Schools.Where(x => x.SchoolId == _new.SchoolId).FirstOrDefaultAsync();

                if (existSchool == null) {
                    _context.Schools.Add(_new);
                    await _context.SaveChangesAsync();
                    trans.Commit();
                    return Ok();
                }
                else {
                    existSchool.SchoolName = _new.SchoolName;
                    _context.Schools.Update(existSchool);
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
