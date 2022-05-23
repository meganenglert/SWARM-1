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
    public class SectionController: BaseController<Section>, iBaseController<Section>
    {
        public SectionController(SWARMOracleContext context, IHttpContextAccessor httpContextAccessor): base(context, httpContextAccessor)
        {}

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            List<Section> lstSections = await _context.Sections.OrderBy(x => x.SectionId).ToListAsync();
            return Ok(lstSections);
        }

        [HttpGet]
        [Route("Get/{key}")]
        public async Task<IActionResult> Get(int key)
        {
            return StatusCode(StatusCodes.Status417ExpectationFailed,
                "Query must take the form of 'Get/{SectionId}/{SchoolId}'"
            );
        }

        [HttpGet]
        [Route("Get/{secId}/{sch}")]
        public async Task<IActionResult> Get(int secId, int sch)
        {
            Section sec = _context.Sections.Where(sec => sec.SectionId==secId && sec.SchoolId==sch).FirstOrDefault();
            return Ok(sec);
        }

        [HttpDelete]
        [Route("Delete/{key}")]
        public async Task<IActionResult> Delete(int key)
        {
            return StatusCode(StatusCodes.Status417ExpectationFailed,
                "Query must take the form of 'Get/{SectionId}/{SchoolId}'"
            );
        }

        [HttpDelete]
        [Route("Delete/{secId}/{sch}")]
        public async Task<IActionResult> Delete(int secId, int sch)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var sec = _context.Sections.Where(sec => sec.SectionId==secId && sec.SchoolId==sch).FirstOrDefault();
                if (sec == null) return StatusCode(StatusCodes.Status404NotFound, "Given Section not found.");

                _context.Sections.Remove(sec);
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
        public async Task<IActionResult> Post([FromBody] Section _new)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var existSection = await _context.Sections.Where(x => x.SectionId == _new.SectionId && x.SchoolId == _new.SchoolId).FirstOrDefaultAsync();

                if (existSection == null) {
                    _context.Sections.Add(_new);
                    await _context.SaveChangesAsync();
                    trans.Commit();
                    return Ok();
                }
                return StatusCode(StatusCodes.Status400BadRequest,
                        "Invalid post request. Section already exists."
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
        public async Task<IActionResult> Put([FromBody] Section _new)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var existSection = await _context.Sections.Where(x => x.SectionId == _new.SectionId && x.SchoolId == _new.SchoolId).FirstOrDefaultAsync();

                if (existSection == null) {
                    _context.Sections.Add(_new);
                    await _context.SaveChangesAsync();
                    trans.Commit();
                    return Ok();
                }
                else {
                    existSection.CourseNo = _new.CourseNo;
                    existSection.SectionNo = _new.SectionNo;
                    existSection.StartDateTime = _new.StartDateTime;
                    existSection.Location = _new.Location;
                    existSection.InstructorId = _new.InstructorId;
                    existSection.Capacity = _new.Capacity;
                    _context.Sections.Update(existSection);
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
