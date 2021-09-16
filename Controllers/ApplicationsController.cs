using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using phoenix.Contracts;
using phoenix.Data;
using phoenix.Models;

namespace phoenix.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationsController : ControllerBase
    {
       

        private readonly ILogger<ApplicationsController> _logger;
        private readonly IApplicationRepository _repo;
        private readonly ApplicationDbContext _context;


        public ApplicationsController(ILogger<ApplicationsController> logger, ApplicationDbContext context, IApplicationRepository repository)
        {
            _logger = logger;
            _context = context;
            _repo = repository;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string search)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search))
                {
                    var dbCompany = await _repo.GetOverviewAsync();
                    if (dbCompany == null)
                    {
                        return NotFound();
                    }
                    return Ok(dbCompany);
                }
                else
                {
                    var dbCompany = await _repo.GetApplicationsAsync(search);
                    if (!dbCompany.Any())
                    {
                        return NotFound();
                    }
                    return Ok(dbCompany);
                }

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }

        }
    }
}
