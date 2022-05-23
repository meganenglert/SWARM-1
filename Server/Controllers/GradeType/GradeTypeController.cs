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
    public class GradeTypeController: BaseController<GradeType>, iBaseController<GradeType>
    {
        public GradeTypeController(SWARMOracleContext context, IHttpContextAccessor httpContextAccessor): base(context, httpContextAccessor)
        {}

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            List<GradeType> lstGradeTypes = await _context.GradeTypes.OrderBy(x => x.GradeTypeCode).ToListAsync();
            return Ok(lstGradeTypes);
        }

        [HttpGet]
        [Route("Get/{key}")]
        public async Task<IActionResult> Get(int key)
        {
            return StatusCode(StatusCodes.Status417ExpectationFailed,
                "Query must take the form of 'Get/{GradeTypeCode}/{SchoolId}'"
            );
        }

        [HttpGet]
        [Route("Get/{grTyp}/{sch}")]
        public async Task<IActionResult> Get(String grTyp, int sch)
        {
            GradeType typ = _context.GradeTypes.Where(typ => typ.GradeTypeCode==grTyp && typ.SchoolId==sch).FirstOrDefault();
            return Ok(typ);
        }

        [HttpDelete]
        [Route("Delete/{key}")]
        public async Task<IActionResult> Delete(int key)
        {
            return StatusCode(StatusCodes.Status417ExpectationFailed,
                "Query must take the form of 'Get/{GradeTypeCode}/{SchoolId}'"
            );
        }

        [HttpDelete]
        [Route("Delete/{grTyp}/{sch}")]
        public async Task<IActionResult> Delete(String grTyp, int sch)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var typ = _context.GradeTypes.Where(typ => typ.GradeTypeCode==grTyp && typ.SchoolId==sch).FirstOrDefault();
                if (typ == null) return StatusCode(StatusCodes.Status404NotFound, "Given GradeType not found.");

                _context.GradeTypes.Remove(typ);
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
        public async Task<IActionResult> Post([FromBody] GradeType _new)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var existGradeType = await _context.GradeTypes.Where(x => x.GradeTypeCode == _new.GradeTypeCode && x.SchoolId == _new.SchoolId).FirstOrDefaultAsync();

                if (existGradeType == null) {
                    _context.GradeTypes.Add(_new);
                    await _context.SaveChangesAsync();
                    trans.Commit();
                    return Ok();
                }
                return StatusCode(StatusCodes.Status400BadRequest,
                        "Invalid post request. GradeType already exists."
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
        public async Task<IActionResult> Put([FromBody] GradeType _new)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var existGradeType = await _context.GradeTypes.Where(x => x.GradeTypeCode == _new.GradeTypeCode && x.SchoolId == _new.SchoolId).FirstOrDefaultAsync();

                if (existGradeType == null) {
                    _context.GradeTypes.Add(_new);
                    await _context.SaveChangesAsync();
                    trans.Commit();
                    return Ok();
                }
                else {
                    existGradeType.Description = _new.Description;
                    _context.GradeTypes.Update(existGradeType);
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
