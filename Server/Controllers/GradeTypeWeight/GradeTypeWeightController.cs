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
    public class GradeTypeWeightController: BaseController<GradeTypeWeight>, iBaseController<GradeTypeWeight>
    {
        public GradeTypeWeightController(SWARMOracleContext context, IHttpContextAccessor httpContextAccessor): base(context, httpContextAccessor)
        {}

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            List<GradeTypeWeight> lstGradeTypeWeights = await _context.GradeTypeWeights.OrderBy(x => x.GradeTypeCode).ToListAsync();
            return Ok(lstGradeTypeWeights);
        }

        [HttpGet]
        [Route("Get/{key}")]
        public async Task<IActionResult> Get(int key)
        {
            return StatusCode(StatusCodes.Status417ExpectationFailed,
                "Query must take the form of 'Get/{GradeTypeCode}/{SectionId}/{SchoolId}"
            );
        }

        [HttpGet]
        [Route("Get/{code}/{sec}/{sch}")]
        public async Task<IActionResult> Get(String code, int sec, int sch)
        {
            GradeTypeWeight gtw = _context.GradeTypeWeights.Where(gtw => gtw.GradeTypeCode==code && gtw.SectionId==sec && gtw.SchoolId==sch).FirstOrDefault();
            return Ok(gtw);
        }

        [HttpDelete]
        [Route("Delete/{key}")]
        public async Task<IActionResult> Delete(int key)
        {
            return StatusCode(StatusCodes.Status417ExpectationFailed,
                "Query must take the form of 'Get/{GradeTypeCode}/{SectionId}/{SchoolId}"
            );
        }

        [HttpDelete]
        [Route("Delete/{code}/{sec}/{sch}")]
        public async Task<IActionResult> Delete(String code, int sec, int sch)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var gtw = _context.GradeTypeWeights.Where(gtw => gtw.GradeTypeCode==code && gtw.SectionId==sec && gtw.SchoolId==sch).FirstOrDefault();
                if (gtw == null) return StatusCode(StatusCodes.Status404NotFound, "Given enrollment not found.");

                _context.GradeTypeWeights.Remove(gtw);
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
        public async Task<IActionResult> Post([FromBody] GradeTypeWeight _new)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var existGradeTypeWeight = await _context.GradeTypeWeights.Where(x => x.GradeTypeCode == _new.GradeTypeCode && x.SectionId == _new.SectionId && x.SchoolId == _new.SchoolId).FirstOrDefaultAsync();

                if (existGradeTypeWeight == null) {
                    _context.GradeTypeWeights.Add(_new);
                    await _context.SaveChangesAsync();
                    trans.Commit();
                    return Ok();
                }
                return StatusCode(StatusCodes.Status400BadRequest,
                        "Invalid post request. GradeTypeWeight already exists."
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
        public async Task<IActionResult> Put([FromBody] GradeTypeWeight _new)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var existGradeTypeWeight = await _context.GradeTypeWeights.Where(x => x.GradeTypeCode == _new.GradeTypeCode && x.SectionId == _new.SectionId && x.SchoolId == _new.SchoolId).FirstOrDefaultAsync();

                if (existGradeTypeWeight == null) {
                    _context.GradeTypeWeights.Add(_new);
                    await _context.SaveChangesAsync();
                    trans.Commit();
                    return Ok();
                }
                else {
                    existGradeTypeWeight.NumberPerSection = _new.NumberPerSection;
                    existGradeTypeWeight.PercentOfFinalGrade = _new.PercentOfFinalGrade;
                    existGradeTypeWeight.DropLowest = _new.DropLowest;
                    _context.GradeTypeWeights.Update(existGradeTypeWeight);
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
