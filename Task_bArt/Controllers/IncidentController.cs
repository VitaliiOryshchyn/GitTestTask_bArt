using Microsoft.AspNetCore.Mvc;
using Task_bArt.Service.Dtos.Request;
using Task_bArt.Service.Services.Abstracts;

namespace Task_bArt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentService _incidentService;

        public IncidentController(IIncidentService incidentService)
        {
            _incidentService = incidentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIncedent()
        {
            var incidents = await _incidentService.GetAllIncidentsAsync();
            return incidents is null ? NotFound() : Ok(incidents);
        }

        [HttpGet("{indexName}")]
        public async Task<IActionResult> GetIncedentByIndexName(string indexName)
        {
            var incedent = await _incidentService.GetIncidentAsync(indexName);
            return incedent is null ? NotFound() : Ok(incedent);
        }

        [HttpPut("{indexName}")]
        public async Task<IActionResult> UpdateIncedent(string indexName, UpdateIncidentDto incidentDto)
        {
            var incedent = await _incidentService.UpdateIncidentAsync(indexName, incidentDto);
            return incedent ? Ok("Incident update!") : NotFound("Incident doesn't exist!");
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncedent(int idAccount, CreateIncidentDto createIncidentDto)
        {
            var incedent = await _incidentService.CreateIncidentAsync(idAccount, createIncidentDto);
            return incedent.Contains("Error") ? NotFound(incedent) : Ok(incedent);
        }
    }
}
