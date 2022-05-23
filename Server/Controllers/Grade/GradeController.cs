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
    public class GradeController: BaseController<Grade>, iBaseController<Grade>
    {
        public GradeController(SWARMOracleContext context, IHttpContextAccessor httpContextAccessor): base(context, httpContextAccessor)
        {}

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            List<Grade> lstGrades = await _context.Grades.OrderBy(x => x.GradeTypeCode).ToListAsync();
            return Ok(lstGrades);
        }

        [HttpGet]
        [Route("Get/{key}")]
        public async Task<IActionResult> Get(int key)
        {
            return StatusCode(StatusCodes.Status417ExpectationFailed,
                "Query must take the form of 'Get/{GradeTypeCode}/{GradeCodeOccurence}/{StudentId}/{SectionId}/{SchoolId}"
            );
        }

        [HttpGet]
        [Route("Get/{code}/{sec}/{sch}")]
        public async Task<IActionResult> Get(String code, int occ, int stu, int sec, int sch)
        {
            Grade gra = _context.Grades.Where(gra => gra.GradeTypeCode==code && gra.GradeCodeOccurrence==occ && gra.StudentId==stu && gra.SectionId==sec && gra.SchoolId==sch).FirstOrDefault();
            return Ok(gra);
        }

        [HttpDelete]
        [Route("Delete/{key}")]
        public async Task<IActionResult> Delete(int key)
        {
            return StatusCode(StatusCodes.Status417ExpectationFailed,
                "Query must take the form of 'Delete/{GradeTypeCode}/{GradeCodeOccurence}/{StudentId}/{SectionId}/{SchoolId}"
            );
        }

        [HttpDelete]
        [Route("Delete/{code}/{sec}/{sch}")]
        public async Task<IActionResult> Delete(String code, int occ, int stu, int sec, int sch)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var gra = _context.Grades.Where(gra => gra.GradeTypeCode==code && gra.GradeCodeOccurrence==occ && gra.StudentId==stu && gra.SectionId==sec && gra.SchoolId==sch).FirstOrDefault();
                if (gra == null) return StatusCode(StatusCodes.Status404NotFound, "Given enrollment not found.");

                _context.Grades.Remove(gra);
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
        public async Task<IActionResult> Post([FromBody] Grade _new)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var existGrade = await _context.Grades.Where(x => x.GradeTypeCode == _new.GradeTypeCode && x.GradeCodeOccurrence == _new.GradeCodeOccurrence && x.StudentId==_new.StudentId && x.SectionId == _new.SectionId && x.SchoolId == _new.SchoolId).FirstOrDefaultAsync();

                if (existGrade == null) {
                    _context.Grades.Add(_new);
                    await _context.SaveChangesAsync();
                    trans.Commit();
                    return Ok();
                }
                return StatusCode(StatusCodes.Status400BadRequest,
                        "Invalid post request. Grade already exists."
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
        public async Task<IActionResult> Put([FromBody] Grade _new)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var existGrade = await _context.Grades.Where(x => x.GradeTypeCode == _new.GradeTypeCode && x.GradeCodeOccurrence == _new.GradeCodeOccurrence && x.StudentId==_new.StudentId && x.SectionId == _new.SectionId && x.SchoolId == _new.SchoolId).FirstOrDefaultAsync();

                if (existGrade == null) {
                    _context.Grades.Add(_new);
                    await _context.SaveChangesAsync();
                    trans.Commit();
                    return Ok();
                }
                else {
                    existGrade.NumericGrade = _new.NumericGrade;
                    existGrade.Comments = _new.Comments;
                    _context.Grades.Update(existGrade);
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
