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
    public class ZipcodeController: BaseController<Zipcode>, iBaseController<Zipcode>
    {
        public ZipcodeController(SWARMOracleContext context, IHttpContextAccessor httpContextAccessor): base(context, httpContextAccessor)
        {}

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            List<Zipcode> lstZipcodes = await _context.Zipcodes.OrderBy(x => x.Zip).ToListAsync();
            return Ok(lstZipcodes);
        }

        [HttpGet]
        [Route("Get/{key}")]
        public async Task<IActionResult> Get(int key)
        {
            Zipcode zip = _context.Zipcodes.Where(zip => zip.Zip==key.ToString()).FirstOrDefault();
            return Ok(zip);
        }


        [HttpGet]
        [Route("Get/{key}")]
        public async Task<IActionResult> Get(String key)
        {
            Zipcode zip = _context.Zipcodes.Where(zip => zip.Zip==key).FirstOrDefault();
            return Ok(zip);
        }

        [HttpDelete]
        [Route("Delete/{key}")]
        public async Task<IActionResult> Delete(String key)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var zip = _context.Zipcodes.Where(zip => zip.Zip==key).FirstOrDefault();
                if (zip == null) return StatusCode(StatusCodes.Status404NotFound, "Given Zipcode not found.");

                _context.Zipcodes.Remove(zip);
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

        [HttpDelete]
        [Route("Delete/{key}")]
        public async Task<IActionResult> Delete(int key)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var zip = _context.Zipcodes.Where(zip => zip.Zip==key.ToString()).FirstOrDefault();
                if (zip == null) return StatusCode(StatusCodes.Status404NotFound, "Given Zipcode not found.");

                _context.Zipcodes.Remove(zip);
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
        public async Task<IActionResult> Post([FromBody] Zipcode _new)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var existZipcode = await _context.Zipcodes.Where(x => x.Zip == _new.Zip).FirstOrDefaultAsync();

                if (existZipcode == null) {
                    _context.Zipcodes.Add(_new);
                    await _context.SaveChangesAsync();
                    trans.Commit();
                    return Ok();
                }
                return StatusCode(StatusCodes.Status400BadRequest,
                        "Invalid post request. Zipcode already exists."
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
        public async Task<IActionResult> Put([FromBody] Zipcode _new)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var existZipcode = await _context.Zipcodes.Where(x => x.Zip == _new.Zip).FirstOrDefaultAsync();

                if (existZipcode == null) {
                    _context.Zipcodes.Add(_new);
                    await _context.SaveChangesAsync();
                    trans.Commit();
                    return Ok();
                }
                else {
                    existZipcode.City = _new.City;
                    existZipcode.State = _new.State;
                    _context.Zipcodes.Update(existZipcode);
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
