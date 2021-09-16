using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using phoenix.Models;
using phoenix.Data;
using Dapper;
using phoenix.Contracts;

namespace phoenix.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {

        private readonly ILogger<ClientsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IClientRepository _repo;

        public ClientsController(ILogger<ClientsController> logger, ApplicationDbContext context, IClientRepository repository)
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
                    var dbCompany = await _repo.GetOverviewAsyc();
                    if (dbCompany == null)
                    {
                        return NotFound();
                    }
                    return Ok(dbCompany);
                }
                else
                {
                    var dbCompany = await _repo.GetClientsAsync(search);
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
