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
    public class GradeConversionController: BaseController<GradeConversion>, iBaseController<GradeConversion>
    {
        public GradeConversionController(SWARMOracleContext context, IHttpContextAccessor httpContextAccessor): base(context, httpContextAccessor)
        {}

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            List<GradeConversion> lstGradeConversions = await _context.GradeConversions.OrderBy(x => x.LetterGrade).ToListAsync();
            return Ok(lstGradeConversions);
        }

        [HttpGet]
        [Route("Get/{key}")]
        public async Task<IActionResult> Get(int key)
        {
            return StatusCode(StatusCodes.Status417ExpectationFailed,
                "Query must take the form of 'Get/{LetterGrade}/{SchoolId}'"
            );
        }

        [HttpGet]
        [Route("Get/{letGr}/{sch}")]
        public async Task<IActionResult> Get(String letGr, int sch)
        {
            GradeConversion let = _context.GradeConversions.Where(let => let.LetterGrade==letGr && let.SchoolId==sch).FirstOrDefault();
            return Ok(let);
        }

        [HttpDelete]
        [Route("Delete/{key}")]
        public async Task<IActionResult> Delete(int key)
        {
            return StatusCode(StatusCodes.Status417ExpectationFailed,
                "Query must take the form of 'Get/{LetterGrade}/{SchoolId}'"
            );
        }

        [HttpDelete]
        [Route("Delete/{letGr}/{sch}")]
        public async Task<IActionResult> Delete(String letGr, int sch)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var let = _context.GradeConversions.Where(let => let.LetterGrade==letGr && let.SchoolId==sch).FirstOrDefault();
                if (let == null) return StatusCode(StatusCodes.Status404NotFound, "Given GradeConversion not found.");

                _context.GradeConversions.Remove(let);
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
        public async Task<IActionResult> Post([FromBody] GradeConversion _new)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var existGradeConversion = await _context.GradeConversions.Where(x => x.LetterGrade == _new.LetterGrade && x.SchoolId == _new.SchoolId).FirstOrDefaultAsync();

                if (existGradeConversion == null) {
                    _context.GradeConversions.Add(_new);
                    await _context.SaveChangesAsync();
                    trans.Commit();
                    return Ok();
                }
                return StatusCode(StatusCodes.Status400BadRequest,
                        "Invalid post request. GradeConversion already exists."
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
        public async Task<IActionResult> Put([FromBody] GradeConversion _new)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var existGradeConversion = await _context.GradeConversions.Where(x => x.LetterGrade == _new.LetterGrade && x.SchoolId == _new.SchoolId).FirstOrDefaultAsync();

                if (existGradeConversion == null) {
                    _context.GradeConversions.Add(_new);
                    await _context.SaveChangesAsync();
                    trans.Commit();
                    return Ok();
                }
                else {
                    existGradeConversion.GradePoint = _new.GradePoint;
                    existGradeConversion.MaxGrade = _new.MaxGrade;
                    existGradeConversion.MinGrade = _new.MinGrade;
                    _context.GradeConversions.Update(existGradeConversion);
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
