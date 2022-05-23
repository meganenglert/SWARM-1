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
    public interface iBaseController<T>
    {
        [HttpGet]
        [Route("Get")]
        public Task<IActionResult> Get();

        [HttpGet]
        [Route("Get/{key}")]
        public Task<IActionResult> Get(int key);

        [HttpDelete]
        [Route("Delete/{key}")]
        public Task<IActionResult> Delete(int key);

        [HttpPost]
        [Route("Post")]
        public Task<IActionResult> Post([FromBody] T _new);

        [HttpPut]
        [Route("Put")]
        public Task<IActionResult> Put([FromBody] T _new);
    }
}
